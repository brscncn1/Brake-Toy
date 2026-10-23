using System;
using System.Collections.Generic;
using BrakeToy.Core;
using UnityEngine;
using UnityEngine.UI;
namespace BrakeToy
{
    [DefaultExecutionOrder(250)]
    public sealed class ReviewFrontend:MonoBehaviour
    {
        public string Page{get;private set;}="Ana menü";
        public string Status{get;private set;}="";
        private GameManager game;private ReviewSession career;private readonly Dictionary<string,GameObject> pages=new Dictionary<string,GameObject>();
        private Text wallet,garageTitle,garageInfo,missions,results,modeHud,settingsStatus,leaderboard;
        private Button buy;private int carIndex,boardPage;private string boardMode="arcade",boardMetric="distance";private RunState previous;
        private readonly Dictionary<Text,int> fontSizes=new Dictionary<Text,int>();
        private Camera garageCamera;private RenderTexture preview;private GameObject model;private float modelAngle;
        private void Start()
        {
            game=GetComponent<GameManager>();career=game.Review;previous=game.State;Build();foreach(var label in game.UI.SafeArea.GetComponentsInChildren<Text>(true))fontSizes[label]=label.fontSize;SetPage("Ana menü");
        }
        private RectTransform Rect(string name,Transform parent,float x0,float y0,float x1,float y1)
        {var r=new GameObject(name,typeof(RectTransform)).GetComponent<RectTransform>();r.SetParent(parent,false);r.anchorMin=new Vector2(x0,y0);r.anchorMax=new Vector2(x1,y1);r.offsetMin=r.offsetMax=Vector2.zero;return r;}
        private Text Text(Transform parent,string name,string value,int size,float x0,float y0,float x1,float y1,TextAnchor alignment=TextAnchor.MiddleCenter)
        {var t=Rect(name,parent,x0,y0,x1,y1).gameObject.AddComponent<Text>();t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");t.text=value;t.fontSize=size;t.color=game.Arcade.Joyful?new Color(.055f,.14f,.22f):new Color(.94f,.96f,.99f);t.alignment=alignment;t.raycastTarget=false;return t;}
        private Button Button(Transform parent,string name,Action action,float x0,float y0,float x1,float y1,bool gold=false)
        {var r=Rect(name,parent,x0,y0,x1,y1);var image=r.gameObject.AddComponent<Image>();image.color=gold?new Color(.95f,.57f,.12f):game.Arcade.Joyful?new Color(.07f,.48f,.52f):new Color(.08f,.20f,.25f,.98f);var b=r.gameObject.AddComponent<Button>();b.targetGraphic=image;b.onClick.AddListener(()=>action());var label=Text(r,"Label",name,32,.03f,0,.97f,1);if(gold)label.color=new Color(.12f,.12f,.10f);else if(game.Arcade.Joyful)label.color=Color.white;return b;}
        private Transform Panel(string name)
        {
            var r=Rect(name,game.UI.SafeArea,.045f,.035f,.955f,.96f);var bg=r.gameObject.AddComponent<Image>();bg.color=game.Arcade.Joyful?new Color(.99f,.95f,.84f,.96f):new Color(.025f,.055f,.075f,.97f);pages[name]=r.gameObject;
            Text(r,"Title",name=="Ana menü"?"BRAKE TOY":name.ToUpperInvariant(),58,.04f,.865f,.96f,.96f);
            if(name!="Ana menü")Button(r,"GERİ",()=>SetPage(game.State==RunState.Paused?"Mola":"Ana menü"),.08f,.035f,.92f,.095f);
            return r;
        }
        private void Build()
        {
            var main=Panel("Ana menü");Text(main,"Tagline","NO BRAKES. BIG TROUBLE.",27,.04f,.79f,.96f,.85f);
            wallet=Text(game.UI.SafeArea,"Wallet","",27,.07f,.955f,.93f,.99f);
            Button(main,"ARCADE",()=>StartMode(PlayMode.Arcade),.08f,.66f,.92f,.745f,true);
            Button(main,"RELAX · 3 HAK",()=>StartMode(PlayMode.Relax),.08f,.55f,.92f,.635f);
            Button(main,"GÜNLÜK ANTRENMAN",()=>StartMode(PlayMode.Daily),.08f,.44f,.92f,.525f);
            Button(main,"GARAJ",()=>SetPage("Garaj"),.08f,.33f,.48f,.41f);Button(main,"GÖREVLER",()=>SetPage("Görevler"),.52f,.33f,.92f,.41f);
            Button(main,"SIRALAMA",()=>SetPage("Sıralama"),.08f,.22f,.48f,.30f);Button(main,"AYARLAR",()=>SetPage("Ayarlar"),.52f,.22f,.92f,.30f);
            Text(main,"Version",game.Arcade.Joyful?"v0.7  •  JOYFUL ROOM\nYeşil ok → kitap rampası → havada TP ×2":"REVIEW 0.6  •  TOY ROOM\nSürükle; parmağını bırakınca yön korunur.",25,.05f,.10f,.95f,.19f);
            var garage=Panel("Garaj");garageTitle=Text(garage,"Car name","",38,.06f,.77f,.94f,.85f);
            var raw=Rect("3D preview",garage,.04f,.43f,.96f,.76f).gameObject.AddComponent<RawImage>();preview=new RenderTexture(640,520,24);preview.Create();raw.texture=preview;raw.raycastTarget=false;
            Button(garage,"İZ RENGİ · 100 TP",()=>{Status=career.Cosmetic(false)?"İz rengi seçildi":"Yeterli TP yok";RefreshGarage(false);},.07f,.37f,.49f,.425f);Button(garage,"KORNA · 100 TP",()=>{Status=career.Cosmetic(true)?"Korna seçildi":"Yeterli TP yok";game.Arcade.Sound.Horn(career.Data.horn);RefreshGarage(false);},.51f,.37f,.93f,.425f);
            garageCamera=new GameObject("Garage preview camera").AddComponent<Camera>();garageCamera.transform.position=new Vector3(1001.5f,1001,1001.8f);garageCamera.transform.LookAt(new Vector3(1000,1000.28f,1000));garageCamera.fieldOfView=38;garageCamera.clearFlags=CameraClearFlags.SolidColor;garageCamera.backgroundColor=game.Arcade.Joyful?new Color(.52f,.82f,.82f):new Color(.025f,.055f,.075f);garageCamera.cullingMask=1<<30;garageCamera.targetTexture=preview;garageCamera.enabled=false;
            Button(garage,"◀",()=>Browse(-1),.07f,.30f,.25f,.365f);Button(garage,"DÖNDÜR",()=>modelAngle+=45,.29f,.30f,.71f,.365f);Button(garage,"▶",()=>Browse(1),.75f,.30f,.93f,.365f);
            garageInfo=Text(garage,"Car info","",26,.06f,.185f,.94f,.29f);buy=Button(garage,"SEÇ",()=>{Status=career.BuyOrSelect(carIndex)?"Seçildi":"Yeterli TP yok";RefreshGarage(false);},.08f,.115f,.92f,.18f,true);
            var missionPanel=Panel("Görevler");missions=Text(missionPanel,"Mission list","",30,.08f,.115f,.92f,.85f,TextAnchor.UpperLeft);
            var settings=Panel("Ayarlar");settingsStatus=Text(settings,"Settings state","",27,.07f,.68f,.93f,.84f);
            Button(settings,"GHOST AÇ / KAPAT",()=>{var ghost=GetComponent<GhostReplay>();ghost.Visible=!ghost.Visible;Refresh();},.08f,.58f,.92f,.655f);
            Button(settings,"YAZI BOYUTUNU DEĞİŞTİR",()=>{largeText=!largeText;Refresh();},.08f,.48f,.92f,.555f);
            Button(settings,"GİZLİLİK VE BİLGİ",()=>SetPage("Bilgi"),.08f,.38f,.92f,.455f);
            var audio=GetComponent<ToyAudioSettings>();audio.Panel.transform.SetParent(settings,false);Place(audio.Panel.GetComponent<RectTransform>(),.07f,.235f,.93f,.35f);
            var q=GetComponent<GraphicsProfileController>();q.ToggleButton.transform.SetParent(settings,false);Place(q.ToggleButton.GetComponent<RectTransform>(),.08f,.15f,.49f,.22f);
            game.Arcade.HUD.SoundButton.transform.SetParent(settings,false);Place(game.Arcade.HUD.SoundButton.GetComponent<RectTransform>(),.51f,.15f,.92f,.22f);
            var probe=GetComponent<PerformanceProbe>();probe.CaptureButton.transform.SetParent(settings,false);Place(probe.CaptureButton.GetComponent<RectTransform>(),.08f,.095f,.92f,.145f);
            var board=Panel("Sıralama");leaderboard=Text(board,"Leaderboard","",28,.07f,.33f,.93f,.83f,TextAnchor.UpperLeft);
            Button(board,"ONLINE ARCADE",()=>GetComponent<ReviewOnline>().StartOnline(PlayMode.Arcade),.08f,.12f,.48f,.20f);Button(board,"ONLINE GÜNLÜK",()=>GetComponent<ReviewOnline>().StartOnline(PlayMode.Daily),.52f,.12f,.92f,.20f);
            Button(board,"ARCADE / GÜNLÜK",()=>{boardMode=boardMode=="arcade"?"daily":"arcade";boardPage=0;GetComponent<ReviewOnline>().FetchBoard(boardMode,boardMetric);},.08f,.255f,.49f,.315f);
            Button(board,"MESAFE / STİL",()=>{boardMetric=boardMetric=="distance"?"style":"distance";boardPage=0;GetComponent<ReviewOnline>().FetchBoard(boardMode,boardMetric);},.51f,.255f,.92f,.315f);
            Button(board,"◀",()=>boardPage=Mathf.Max(0,boardPage-1),.08f,.205f,.30f,.25f);Button(board,"YENİLE",()=>GetComponent<ReviewOnline>().FetchBoard(boardMode,boardMetric),.34f,.205f,.66f,.25f);Button(board,"▶",()=>boardPage=Mathf.Min(6,boardPage+1),.70f,.205f,.92f,.25f);
            var info=Panel("Bilgi");Text(info,"Privacy","ÇEVRİMDIŞI REVİZYON ADAYI\n\nHesap zorunlu değil. Reklam, ödeme ve analiz yok. Kariyer ve ghost bu cihazda tutulur.\n\nÇevrimiçi servis henüz bağlı değil; skorlar küresel tabloya gönderilmez. Günlük antrenman cihazın UTC tarihini kullanır, doğrulanmış günlük yarış değildir.\n\nPerformans testi model/işletim sistemi/FPS bilgilerini yalnızca yerel dosyaya kaydeder.\n\nCC0 kaynakları: Poly Haven, Kenney ve qubodup. Ayrıntılı lisanslar proje dosyalarında.",30,.08f,.14f,.92f,.84f,TextAnchor.UpperLeft);
            var result=Panel("Sonuç");results=Text(result,"Results","",37,.07f,.46f,.93f,.83f);Button(result,"YENİ PARKUR",()=>StartMode(career.Mode),.08f,.35f,.92f,.435f,true);Button(result,"AYNI PARKUR + GHOST",()=>{game.StartSameRoute();},.08f,.245f,.92f,.325f);Button(result,"ANA MENÜ",()=>SetPage("Ana menü"),.08f,.14f,.92f,.22f);
            var pause=Panel("Mola");Text(pause,"Pause description","Parkur ve kombo duraklatıldı.",32,.08f,.65f,.92f,.81f);Button(pause,"DEVAM ET",()=>game.Resume(),.08f,.50f,.92f,.59f,true);Button(pause,"TURU BİTİR",()=>game.Crash(),.08f,.38f,.92f,.47f);Button(pause,"AYARLAR",()=>SetPage("Ayarlar"),.08f,.26f,.92f,.35f);
            modeHud=Text(game.Arcade.HUD.Driving.transform,"Mode lives","",26,.05f,.84f,.95f,.885f);wallet.transform.SetAsLastSibling();
            Button(game.Arcade.HUD.Driving.transform,"KORNA",()=>game.Arcade.Sound.Horn(career.Data.horn),.79f,.02f,.985f,.075f);
        }
        private bool largeText;
        private static void Place(RectTransform r,float x0,float y0,float x1,float y1){r.anchorMin=new Vector2(x0,y0);r.anchorMax=new Vector2(x1,y1);r.offsetMin=r.offsetMax=Vector2.zero;}
        public void SetPage(string page)
        {if(!pages.ContainsKey(page))return;Page=page;foreach(var pair in pages)pair.Value.SetActive(pair.Key==page);if(page=="Garaj"){carIndex=career.Data.selectedCar;RefreshGarage(true);}Refresh();}
        public void StartMode(PlayMode mode)
        {
            GetComponent<ReviewOnline>()?.CancelCompetitive();career.Mode=mode;game.Arcade.VaryRuns=mode!=PlayMode.Daily;
            if(mode==PlayMode.Daily){string date=DateTime.UtcNow.ToString("yyyy-MM-dd");if(career.Data.dailyDate!=date){career.Data.dailyDate=date;career.Data.dailyDistance=0;career.Data.dailyStyle=0;}game.Arcade.Seed=DailySeed(date);}
            game.StartRun();
        }
        public static int DailySeed(string date){unchecked{uint hash=2166136261;foreach(char c in date){hash^=c;hash*=16777619;}return (int)(hash&0x7fffffff);}}
        public void Browse(int delta){carIndex=(carIndex+delta+career.Vehicles.Length)%career.Vehicles.Length;RefreshGarage(true);}
        private void RefreshGarage(bool rebuild)
        {
            var d=career.Vehicles[carIndex];bool unlocked=(career.Data.unlocked&(1<<carIndex))!=0;garageTitle.text=d.Title;
            buy.GetComponentInChildren<Text>().text=unlocked?(career.Data.selectedCar==carIndex?"SEÇİLİ":"SEÇ"):"AÇ  ·  "+d.Cost+" TP";
            garageInfo.text=(unlocked?"AÇIK":"KİLİTLİ")+"  •  "+(carIndex+1)+" / 10\nTüm arabaların sürüşü eşit.\n"+Status;
            if(rebuild){if(model)Destroy(model);model=Instantiate(d.VisualPrefab);model.SetActive(true);model.transform.position=new Vector3(1000,1000,1000);foreach(var t in model.GetComponentsInChildren<Transform>(true))t.gameObject.layer=30;modelAngle=150;}
        }
        private void Refresh()
        {
            missions.text="BU TURUN GÖREVLERİ\n";foreach(var m in career.Missions)missions.text+="• "+m.Title+"  +"+m.Reward+" TP\n";missions.text+="\nBAŞARIMLAR\n";for(int i=0;i<CareerRules.Achievements.Length;i++)missions.text+=((career.Data.achievements&(1<<i))!=0?"✓ ":"○ ")+CareerRules.Achievements[i].Title+"\n";
            settingsStatus.text="Ghost: "+(GetComponent<GhostReplay>().Visible?"AÇIK":"KAPALI")+"\nYazı: "+(largeText?"BÜYÜK":"STANDART")+"\n"+(career.SaveFailed?"Kayıt yazılamadı; depolamayı kontrol et.":career.Recovered?"Yedek kayıt kurtarıldı.":"İlerleme cihazda saklanır.");
            foreach(var pair in fontSizes)if(pair.Key)pair.Key.fontSize=largeText?Mathf.RoundToInt(pair.Value*(pair.Value<=30?1.15f:1.08f)):pair.Value;
            leaderboard.text="BU CİHAZDAKİ REKORLAR\n\nARCADE\n"+Mathf.FloorToInt(career.Data.arcadeDistance)+" m  •  "+career.Data.arcadeStyle+" stil\n\nRELAX\n"+Mathf.FloorToInt(career.Data.relaxDistance)+" m  •  "+career.Data.relaxStyle+" stil\n\nGÜNLÜK ANTRENMAN\n"+career.Data.dailyDate+"\n"+Mathf.FloorToInt(career.Data.dailyDistance)+" m\n\nÇEVRİMİÇİ: BAĞLI DEĞİL\nKüresel skor gönderimi yok.";
        }
        private void LateUpdate()
        {
            if(!game)return;
            game.UI.Overlay.SetActive(false);bool running=game.State==RunState.Running;
            if(game.State!=previous){if(game.State==RunState.Crashed){SetPage("Sonuç");results.text=(game.Arcade.Joyful?"JOYFUL ROOM · ":"")+ModeName()+"\n"+Mathf.FloorToInt(game.Arcade.Distance)+" m  •  "+game.Arcade.Style.Total+" STİL\n\n+"+career.LastReward+" TOY POINTS\nBAKİYE  "+career.Data.points+" TP";}else if(game.State==RunState.Paused)SetPage("Mola");previous=game.State;}
            foreach(var pair in pages)pair.Value.SetActive(!running&&pair.Key==Page);
            wallet.gameObject.SetActive(!running);wallet.text=career.Data.points+" TOY POINTS"+(career.SaveFailed?"  •  KAYIT HATASI":"");
            garageCamera.enabled=!running&&Page=="Garaj";if(model){model.SetActive(garageCamera.enabled);if(model.activeSelf){modelAngle+=Time.unscaledDeltaTime*15;model.transform.rotation=Quaternion.Euler(0,modelAngle,0);}}
            if(!running&&Page=="Sıralama")
            {var online=GetComponent<ReviewOnline>();if(online.Board!=null&&online.Board.rows!=null){var rows=online.Board.rows;boardPage=Mathf.Clamp(boardPage,0,Mathf.Max(0,(rows.Length-1)/15));leaderboard.text=online.Status+"\n"+online.BoardMode.ToUpperInvariant()+" · "+online.BoardMetric.ToUpperInvariant()+" · "+(boardPage+1)+"/"+Mathf.Max(1,(rows.Length+14)/15)+"\nSıran: "+(online.Board.ownRank>0?online.Board.ownRank.ToString():"Henüz yok")+"\n\n";for(int i=boardPage*15;i<Mathf.Min(rows.Length,boardPage*15+15);i++){var row=rows[i];leaderboard.text+=(i+1)+". "+row.player+"  "+(online.BoardMetric=="style"?row.style+" stil":Mathf.FloorToInt(row.distance)+" m")+"\n";}if(online.Board.nearby!=null&&online.Board.nearby.Length>0){leaderboard.text+="\nYAKIN SIRALAR\n";foreach(var row in online.Board.nearby)leaderboard.text+=row.player+" · "+(online.BoardMetric=="style"?row.style+" stil":Mathf.FloorToInt(row.distance)+" m")+"\n";}}else{Refresh();leaderboard.text+="\n"+online.Status;}}
            if(running){modeHud.text=ModeName()+(career.Mode==PlayMode.Relax?"  •  "+career.Lives+" HAK":"")+(game.Arcade.Joyful&&GetComponent<JoyfulRun>().Airborne?"  •  TP ×2":"");if(career.Mode==PlayMode.Relax)game.Arcade.HUD.SpeedLabel.text="RELAX  "+(game.Arcade.Speed/10).ToString("0.00")+"×";}
            GetComponent<ToyAudioSettings>().Panel.SetActive(!running&&Page=="Ayarlar");
        }
        private string ModeName()=>career.Mode==PlayMode.Relax?"RELAX":career.Mode==PlayMode.Daily?"GÜNLÜK ANTRENMAN":"ARCADE";
        private void OnDestroy(){if(model)Destroy(model);if(garageCamera){garageCamera.enabled=false;garageCamera.targetTexture=null;Destroy(garageCamera.gameObject);}if(preview){preview.Release();Destroy(preview);}}
    }
}
