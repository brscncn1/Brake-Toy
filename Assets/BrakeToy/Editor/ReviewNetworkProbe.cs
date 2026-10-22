using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace BrakeToy.Editor
{
    // Explicit local integration probe; never enables networking in the saved scene.
    public static class ReviewNetworkProbe
    {
        private static double started;private static bool running;
        public static void Run(){EditorSceneManager.OpenScene(ReviewSceneBuilder.ScenePath);SessionState.SetBool("ReviewNetworkProbe",true);EditorApplication.EnterPlaymode();}
        [InitializeOnLoadMethod]private static void Resume(){if(!SessionState.GetBool("ReviewNetworkProbe",false))return;started=EditorApplication.timeSinceStartup;running=false;EditorApplication.update+=Update;}
        private static void Update()
        {
            if(EditorApplication.timeSinceStartup-started>90){Complete(false,"Timed out");return;}
            if(!EditorApplication.isPlaying||running||EditorApplication.timeSinceStartup-started<3)return;
            running=true;var g=UnityEngine.Object.FindFirstObjectByType<GameManager>();g.enabled=false;g.Review.Persist=false;g.Arcade.PersistRecords=false;
            var online=g.GetComponent<ReviewOnline>();online.Configuration=ScriptableObject.CreateInstance<OnlineConfiguration>();online.Configuration.Endpoint="http://127.0.0.1:8765";online.Configuration.EligibilityAndPrivacyReviewed=true;online.StartCoroutine(Probe(g,online));
        }
        private static IEnumerator Probe(GameManager game,ReviewOnline online)
        {
            online.StartOnline(Core.PlayMode.Arcade);float deadline=Time.realtimeSinceStartup+20;
            while(!online.Recording&&Time.realtimeSinceStartup<deadline)yield return null;
            if(!online.Recording){Complete(false,online.Status);yield break;}
            game.Arcade.Step(1f/60,0,0);game.Crash();deadline=Time.realtimeSinceStartup+20;
            while(online.Board==null&&Time.realtimeSinceStartup<deadline)yield return null;
            if(online.Board==null||!online.Board.verified||online.Board.ownRank!=1){Complete(false,online.Status);yield break;}
            online.FetchBoard("daily","style");deadline=Time.realtimeSinceStartup+20;
            while(online.BoardMode!="daily"&&Time.realtimeSinceStartup<deadline)yield return null;
            if(online.BoardMode!="daily"){Complete(false,online.Status);yield break;}
            online.Configuration.EligibilityAndPrivacyReviewed=false;online.CancelCompetitive();game.Review.Mode=Core.PlayMode.Relax;game.StartRun();game.Arcade.Step(.1f,0,0);
            Complete(game.State==RunState.Running&&!online.Recording,"Unity HTTP auth → server-owned ticket → input recording → resimulated submit → own rank → daily/style fetch → gated offline Relax");
        }
        private static void Complete(bool passed,string details)
        {SessionState.SetBool("ReviewNetworkProbe",false);EditorApplication.update-=Update;Directory.CreateDirectory("Tests");File.WriteAllText("Tests/Network-review.txt",(passed?"PASS":"FAIL")+"\n"+details+"\nLocal loopback reference server only; no production deployment or eligibility acceptance.\n");Debug.Log((passed?"PASS ":"FAIL ")+details);EditorApplication.Exit(passed?0:1);}
    }
}
