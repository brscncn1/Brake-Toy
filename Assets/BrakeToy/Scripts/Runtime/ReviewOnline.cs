using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BrakeToy.Core;
using UnityEngine;
using UnityEngine.Networking;
namespace BrakeToy
{
    [Serializable] public sealed class BoardRow{public string player;public float distance;public int style;}
    [Serializable] public sealed class BoardSnapshot{public BoardRow[] rows,nearby;public int ownRank;public bool verified;}
    public interface ILeaderboardService{IEnumerator Fetch(string mode,string metric,Action<BoardSnapshot> complete);}
    public sealed class LocalLeaderboardService:ILeaderboardService
    {
        private readonly CareerData data;public LocalLeaderboardService(CareerData data){this.data=data;}
        public IEnumerator Fetch(string mode,string metric,Action<BoardSnapshot> complete)
        {complete(new BoardSnapshot{verified=false,ownRank=1,rows=new[]{new BoardRow{player="BU CİHAZ",distance=mode=="daily"?data.dailyDistance:data.arcadeDistance,style=mode=="daily"?data.dailyStyle:data.arcadeStyle}},nearby=Array.Empty<BoardRow>()});yield break;}
    }
    public sealed class OnlineLeaderboardService:ILeaderboardService
    {
        private readonly ReviewOnline transport;public OnlineLeaderboardService(ReviewOnline transport){this.transport=transport;}
        public IEnumerator Fetch(string mode,string metric,Action<BoardSnapshot> complete)
        {yield return transport.Request("GET","/leaderboard?mode="+mode+"&metric="+metric,null,s=>complete(JsonUtility.FromJson<BoardSnapshot>(s)));}
    }
    public sealed class ReviewOnline:MonoBehaviour
    {
        public OnlineConfiguration Configuration;
        public string Status{get;private set;}="ÇEVRİMİÇİ: BAĞLI DEĞİL";
        public BoardSnapshot Board{get;private set;}
        public string BoardMode{get;private set;}="arcade";public string BoardMetric{get;private set;}="distance";
        public bool Recording{get;private set;}
        [Serializable] public sealed class InputStep{public float dt,x;}
        [Serializable] private sealed class Auth{public string token,player;}
        [Serializable] private sealed class Ticket{public string ticket,date,rules;public int seed;}
        [Serializable] private sealed class Submission{public string ticket,rules="review-r1";public List<InputStep> steps;}
        private readonly List<InputStep> steps=new List<InputStep>(36000);private string token,ticket,runMode;private int seed;private bool prepared,busy;
        public bool Configured
        {
            get{if(GetComponent<JoyfulRun>())return false;if(!Configuration||!Configuration.EligibilityAndPrivacyReviewed||string.IsNullOrWhiteSpace(Configuration.Endpoint))return false;if(!Uri.TryCreate(Configuration.Endpoint,UriKind.Absolute,out var u))return false;
#if UNITY_EDITOR
                if(u.IsLoopback&&u.Scheme=="http")return true;
#endif
                return u.Scheme=="https";}
        }
        public IEnumerator Request(string method,string path,string body,Action<string> complete)
        {
            if(!Configured){Status="Çevrimiçi servis yapılandırılmadı.";yield break;}
            using(var request=new UnityWebRequest(Configuration.Endpoint.TrimEnd('/')+path,method))
            {request.downloadHandler=new DownloadHandlerBuffer();request.timeout=15;if(body!=null){request.uploadHandler=new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));request.SetRequestHeader("Content-Type","application/json");}if(!string.IsNullOrEmpty(token))request.SetRequestHeader("Authorization","Bearer "+token);yield return request.SendWebRequest();if(request.result!=UnityWebRequest.Result.Success){Status="Bağlantı kurulamadı ("+request.responseCode+"). Çevrimdışı oynayabilirsin.";yield break;}complete(request.downloadHandler.text);}
        }
        public void StartOnline(PlayMode mode){if(!busy)StartCoroutine(Begin(mode));}
        private IEnumerator Begin(PlayMode mode)
        {
            if(mode==PlayMode.Relax){Status="Relax küresel sıralamaya girmez.";yield break;}if(!Configured){Status="Bu pakette çevrimiçi bağlantı hazır değil.";yield break;}
            busy=true;Status="BAĞLANIYOR…";
            try
            {
                if(string.IsNullOrEmpty(token)){Auth auth=null;yield return Request("POST","/session","{}",s=>auth=JsonUtility.FromJson<Auth>(s));if(auth==null||string.IsNullOrEmpty(auth.token))yield break;token=auth.token;}
                Ticket response=null;yield return Request("POST","/runs","{\"mode\":\""+(mode==PlayMode.Daily?"daily":"arcade")+"\"}",s=>response=JsonUtility.FromJson<Ticket>(s));
                if(response==null||response.rules!="review-r1"||string.IsNullOrEmpty(response.ticket)){Status="Parkur alınamadı; çevrimdışı sürüş açık.";yield break;}
                var g=GetComponent<GameManager>();ticket=response.ticket;runMode=mode==PlayMode.Daily?"daily":"arcade";seed=response.seed;prepared=true;g.Review.Mode=mode;
                if(mode==PlayMode.Daily&&g.Review.Data.dailyDate!=response.date){g.Review.Data.dailyDate=response.date;g.Review.Data.dailyDistance=0;g.Review.Data.dailyStyle=0;}
                g.Arcade.Seed=seed;g.Arcade.VaryRuns=false;g.StartRun();Status="ÇEVRİMİÇİ PARKUR";
            }
            finally{busy=false;}
        }
        public void BeginRecord()
        {steps.Clear();Recording=prepared&&GetComponent<GameManager>().Arcade.CurrentSeed==seed;prepared=false;if(!Recording)ticket=null;}
        public void CancelCompetitive(){prepared=Recording=false;ticket=null;}
        public void Record(float dt,float x){if(!Recording)return;if(steps.Count>=72000){CancelCompetitive();Status="Tur kayıt sınırına ulaştı; sonuç yalnız cihazda.";return;}steps.Add(new InputStep{dt=dt,x=x});}
        public void Submit()
        {if(!Recording)return;Recording=false;var submission=new Submission{ticket=ticket,steps=new List<InputStep>(steps)};ticket=null;StartCoroutine(Send(submission,runMode));}
        private IEnumerator Send(Submission submission,string mode)
        {Status="TUR DOĞRULANIYOR…";bool accepted=false;yield return Request("POST","/submit",JsonUtility.ToJson(submission),s=>accepted=true);if(accepted){Status="SUNUCUDA DOĞRULANDI";yield return Fetch(mode,"distance");}}
        public void FetchBoard(string mode,string metric){if(!busy)StartCoroutine(Fetch(mode,metric));}
        private IEnumerator Fetch(string mode,string metric)
        {if((mode!="arcade"&&mode!="daily")||(metric!="distance"&&metric!="style"))yield break;busy=true;try{if(string.IsNullOrEmpty(token)){Auth auth=null;yield return Request("POST","/session","{}",s=>auth=JsonUtility.FromJson<Auth>(s));if(auth==null)yield break;token=auth.token;}yield return new OnlineLeaderboardService(this).Fetch(mode,metric,b=>{Board=b;BoardMode=mode;BoardMetric=metric;Status="SUNUCUDA DOĞRULANMIŞ SIRALAMA";});}finally{busy=false;}}
    }
}
