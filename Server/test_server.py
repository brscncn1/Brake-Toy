import unittest,tempfile,threading,json,urllib.request,urllib.error,time
from http.server import ThreadingHTTPServer
from app import make_handler,database,daily
from rules import Sequence,validate
from pathlib import Path
class ServerTests(unittest.TestCase):
 def setUp(self):
  self.temp=tempfile.TemporaryDirectory();self.db=self.temp.name+'/test.sqlite';database(self.db).close();self.server=ThreadingHTTPServer(('127.0.0.1',0),make_handler(self.db));threading.Thread(target=self.server.serve_forever,daemon=True).start();self.url='http://127.0.0.1:'+str(self.server.server_port);self.token=self.call('/session',{})['token']
 def tearDown(self):self.server.shutdown();self.server.server_close();self.temp.cleanup()
 def call(self,path,body=None,token=True):
  data=json.dumps(body).encode() if body is not None else None;headers={'Content-Type':'application/json'}
  if token and hasattr(self,'token'):headers['Authorization']='Bearer '+self.token
  try:
   with urllib.request.urlopen(urllib.request.Request(self.url+path,data=data,headers=headers),timeout=5) as r:return json.load(r)
  except urllib.error.HTTPError as error:
   error.read();error.close();raise
 def test_delete_removes_only_authenticated_player_and_revokes_session(self):
  ticket=self.call('/runs',{'mode':'arcade'});self.call('/submit',{'ticket':ticket['ticket'],'rules':'review-r1','steps':[{'dt':.016,'x':0}]});self.assertTrue(self.call('/delete-player',{})['deleted'])
  with self.assertRaises(urllib.error.HTTPError) as error:self.call('/leaderboard')
  self.assertEqual(error.exception.code,401);self.token=self.call('/session',{})['token'];self.assertEqual(self.call('/leaderboard')['rows'],[])
 def test_time_and_seed_are_server_owned(self):self.assertEqual(self.call('/daily'),daily());self.assertEqual(self.call('/runs',{'mode':'daily'})['seed'],daily()['seed'])
 def test_relax_cannot_request_competitive_ticket(self):
  with self.assertRaises(urllib.error.HTTPError) as e:self.call('/runs',{'mode':'relax'})
  self.assertEqual(e.exception.code,400)
 def test_expired_or_wrong_owner_ticket_rejected(self):
  ticket=self.call('/runs',{'mode':'arcade'});self.token=self.call('/session',{})['token']
  with self.assertRaises(urllib.error.HTTPError) as e:self.call('/submit',{'ticket':ticket['ticket'],'rules':'review-r1','steps':[{'dt':.016,'x':0}]})
  self.assertEqual(e.exception.code,409)
 def test_score_is_resimulated_and_ticket_is_one_time(self):
  ticket=self.call('/runs',{'mode':'arcade'});payload={'ticket':ticket['ticket'],'rules':'review-r1','steps':[{'dt':.016,'x':0}],'score':99999999};result=self.call('/submit',payload);self.assertEqual(result['style'],0);self.assertLess(result['distance'],1)
  with self.assertRaises(urllib.error.HTTPError):self.call('/submit',payload)
  board=self.call('/leaderboard?mode=arcade&metric=distance');self.assertTrue(board['verified']);self.assertEqual(len(board['rows']),1);self.assertEqual(board['ownRank'],1)
 def test_teleport_and_unbounded_time_rejected(self):
  for step in [{'dt':.016,'x':3},{'dt':10,'x':0},{'dt':.1,'x':float('nan')}]:
   ticket=self.call('/runs',{'mode':'arcade'})
   with self.assertRaises(urllib.error.HTTPError) as e:self.call('/submit',{'ticket':ticket['ticket'],'rules':'review-r1','steps':[step]})
   self.assertEqual(e.exception.code,400)
 def test_auth_required_and_fields_whitelisted(self):
  with self.assertRaises(urllib.error.HTTPError):self.call('/leaderboard',token=False)
  with self.assertRaises(urllib.error.HTTPError):self.call('/leaderboard?mode=relax')
 def test_rules_safe_routes(self):
  for seed in range(1,100):
   seq=Sequence(seed);last=1
   for _ in range(100):row=seq.next();self.assertLessEqual(abs(row['safe']-last),1);self.assertFalse(row['mask']&(1<<row['safe']));last=row['safe']
 def test_actual_unity_traces_match_server_owned_scores(self):
  files=list((Path(__file__).resolve().parent.parent/'Tests'/'ServerParity').glob('*.json'));self.assertGreaterEqual(len(files),2)
  for path in files:
   data=json.loads(path.read_text(encoding='utf-8'));result=validate(data['seed'],data['steps']);self.assertAlmostEqual(result['distance'],data['distance'],delta=.001);self.assertEqual(result['style'],data['style']);self.assertFalse(result['crashed'])
if __name__=='__main__':unittest.main(verbosity=2)
