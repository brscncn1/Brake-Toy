using BrakeToy.Core;
using UnityEngine;
using UnityEngine.UI;
namespace BrakeToy
{
    [DefaultExecutionOrder(300)]
    public sealed class PlaygroundUI:MonoBehaviour
    {
        private GameManager game;private PlaygroundRun run;private ReviewFrontend front;private Text[] chargeLabels,missionTitles,missionProgress;private Image[] missionFills;private Button[] powers;private Text state,zones,domino;private bool achievements;private int badgePage;
        private readonly Color ink=new Color(.055f,.13f,.20f),paper=new Color(.98f,.95f,.87f),teal=new Color(.05f,.43f,.46f);
        private RectTransform Rect(string name,Transform parent,float a,float b,float c,float d){var r=new GameObject(name,typeof(RectTransform)).GetComponent<RectTransform>();r.SetParent(parent,false);Place(r,a,b,c,d);return r;}
        private static void Place(RectTransform r,float a,float b,float c,float d){r.anchorMin=new Vector2(a,b);r.anchorMax=new Vector2(c,d);r.offsetMin=r.offsetMax=Vector2.zero;}
        private Text Label(Transform parent,string name,string value,int size,float a,float b,float c,float d){var t=Rect(name,parent,a,b,c,d).gameObject.AddComponent<Text>();t.font=Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");t.text=value;t.fontSize=size;t.color=ink;t.alignment=TextAnchor.MiddleCenter;t.raycastTarget=false;return t;}
        private Button Button(Transform parent,string name,System.Action action,float a,float b,float c,float d){var r=Rect(name,parent,a,b,c,d);var image=r.gameObject.AddComponent<Image>();image.color=teal;var button=r.gameObject.AddComponent<Button>();button.targetGraphic=image;button.onClick.AddListener(()=>action());Label(r,"Label",name,23,0,0,1,1).color=Color.white;return button;}
        private void Start()
        {
            game=GetComponent<GameManager>();run=GetComponent<PlaygroundRun>();front=GetComponent<ReviewFrontend>();var safe=game.UI.SafeArea;var main=safe.Find("Ana menü").GetComponent<RectTransform>();Place(main,.045f,.035f,.955f,.48f);main.GetComponent<Image>().color=new Color(.98f,.95f,.87f,.91f);
            foreach(Transform t in main){if(t.name=="Tagline"||t.name=="Version")t.gameObject.SetActive(false);}
            Place(main.Find("Title").GetComponent<RectTransform>(),.05f,.86f,.95f,.99f);main.Find("Title").GetComponent<Text>().fontSize=42;
            Move(main,"ARCADE",.06f,.67f,.94f,.84f);Move(main,"RELAX · 3 HAK",.06f,.49f,.49f,.64f);Move(main,"GÜNLÜK ANTRENMAN",.51f,.49f,.94f,.64f);Move(main,"GARAJ",.06f,.31f,.49f,.46f);Move(main,"GÖREVLER",.51f,.31f,.94f,.46f);Move(main,"SIRALAMA",.06f,.13f,.49f,.28f);Move(main,"AYARLAR",.51f,.13f,.94f,.28f);
            roomTitle=Label(safe,"Room title","BİR ODA DOLUSU MACERA",32,.06f,.88f,.94f,.93f);
            var missions=safe.Find("Görevler");missions.Find("Mission list").gameObject.SetActive(false);missionTitles=new Text[3];missionProgress=new Text[3];missionFills=new Image[3];
            for(int i=0;i<3;i++){float y=.63f-i*.215f;var card=Rect("Mission card "+i,missions,.07f,y,.93f,y+.19f);card.gameObject.AddComponent<Image>().color=new Color(.85f,.91f,.88f);for(int metric=0;metric<4;metric++)symbols[i,metric]=Symbol(card,metric);missionTitles[i]=Label(card,"Task","",38,.25f,.52f,.96f,.95f);missionProgress[i]=Label(card,"Progress","",29,.25f,.20f,.96f,.55f);var track=Rect("Track",card,.22f,.10f,.94f,.17f);track.gameObject.AddComponent<Image>().color=new Color(.64f,.72f,.71f);var fill=Rect("Fill",track,0,0,0,1).gameObject.AddComponent<Image>();fill.color=teal;missionFills[i]=fill;}
            Button(missions,"GÖREV / ROZET",()=>{achievements=!achievements;badgePage=0;},.07f,.105f,.64f,.165f);Button(missions,"◀",()=>badgePage=Mathf.Max(0,badgePage-1),.66f,.105f,.78f,.165f);Button(missions,"▶",()=>badgePage=Mathf.Min(4,badgePage+1),.80f,.105f,.93f,.165f);
            var driving=game.Arcade.HUD.Driving.transform;chargeLabels=new Text[3];powers=new Button[3];for(int i=0;i<3;i++){int n=i;float x=.025f+i*.325f;powers[i]=Button(driving,"GÜÇ "+i,()=>run.Activate((ToyPower)n),x,.095f,x+.30f,.16f);chargeLabels[i]=powers[i].GetComponentInChildren<Text>();}
            Place(game.Arcade.Challenges.GoalLabel.rectTransform,.05f,.785f,.95f,.82f);game.Arcade.Challenges.GoalLabel.color=ink;
            state=Label(driving,"Active power","",27,.08f,.17f,.92f,.205f);
            var settings=safe.Find("Ayarlar");var audio=settings.Find("Audio settings"); // Existing panel has its own retained layout.
            zones=Button(settings,"MASA BÖLGELERİ",()=>run.ToggleZones(),.08f,.84f,.49f,.89f).GetComponentInChildren<Text>();domino=Button(settings,"DOMİNO",()=>run.ToggleDomino(),.51f,.84f,.92f,.89f).GetComponentInChildren<Text>();
            // A separate refill drawer keeps purchases out of steering and avoids accidental instant spending.
            var drawer=Rect("Power refills",safe,.045f,.03f,.955f,.90f);drawer.gameObject.AddComponent<Image>().color=paper;Label(drawer,"Title","OYUNCAK GÜÇLERİ",44,.05f,.84f,.95f,.96f);Label(drawer,"Info","Başlangıçta üçer hak · Her kullanım kalıcıdır\nYalnız TP kullanılır · Gerçek para yok",26,.06f,.72f,.94f,.84f);
            for(int i=0;i<3;i++){int n=i;float y=.52f-i*.17f;Button(drawer,new[]{"YAVAŞLAT · 3 SN","ROKET + KORUMA · 3 SN","JETPACK · 5 SN"}[i]+"\n+1 HAK · "+PlaygroundRules.RefillCosts[i]+" TP",()=>{bool ok=game.Review.RefillPower((ToyPower)n);refillStatus.text=ok?"+1 HAK ALINDI":game.Review.SaveFailed?"Kayıt yazılamadı; ücret alınmadı":"Yeterli TP yok veya hak sınırı";},.07f,y,.93f,y+.13f);}
            refillStatus=Label(drawer,"Result","",26,.05f,.09f,.95f,.18f);Button(drawer,"GERİ",()=>drawer.gameObject.SetActive(false),.08f,.02f,.92f,.08f);drawer.gameObject.SetActive(false);
            Button(main,"GÜÇ HAKLARI",()=>{drawer.gameObject.SetActive(true);drawer.SetAsLastSibling();refillStatus.text="Bakiye: "+game.Review.Data.points+" TP";},.06f,.005f,.94f,.115f);
        }
        private GameObject[,] symbols=new GameObject[3,4];
        private Text refillStatus,roomTitle;private bool resultDecorated;

        private GameObject Symbol(Transform parent,int metric)
        {
            var root=Rect("Task symbol "+metric,parent,.035f,.22f,.22f,.85f);root.gameObject.AddComponent<Image>().color=new Color(.70f,.83f,.80f);
            void Shape(string name,Color color,float a,float b,float c,float d){Rect(name,root,a,b,c,d).gameObject.AddComponent<Image>().color=color;}
            if(metric==0){Shape("Road",ink,.28f,.04f,.72f,.96f);for(int n=0;n<4;n++)Shape("Road dash",Color.white,.47f,.10f+n*.22f,.53f,.22f+n*.22f);}
            else if(metric==2){Shape("Red toy",new Color(.88f,.20f,.15f),.08f,.20f,.38f,.80f);Shape("Blue toy",teal,.62f,.20f,.92f,.80f);Shape("Red window",Color.white,.13f,.58f,.33f,.70f);Shape("Blue window",Color.white,.67f,.58f,.87f,.70f);Label(root,"Gap","↔",32,.30f,.20f,.70f,.80f);}
            else Label(root,"Star",metric==3?"★":"✦",90,.03f,.03f,.97f,.97f).color=new Color(.88f,.46f,.055f);
            return root.gameObject;
        }

        private void Move(Transform parent,string name,float a,float b,float c,float d){var t=parent.Find(name);Place(t.GetComponent<RectTransform>(),a,b,c,d);t.GetComponentInChildren<Text>().fontSize=name=="ARCADE"?30:23;}
        private void LateUpdate()
        {
            if(!game)return;roomTitle.gameObject.SetActive(game.State!=RunState.Running&&front.Page=="Ana menü");for(int i=0;i<3;i++){chargeLabels[i].text=new[]{"YAVAŞLAT","ROKET","JETPACK"}[i]+"  ×"+PlaygroundRules.Charges(game.Review.Data,(ToyPower)i);powers[i].interactable=game.State==RunState.Running&&!run.ActivePower.HasValue&&!run.Protected&&PlaygroundRules.Charges(game.Review.Data,(ToyPower)i)>0;}
            state.text=run.ActivePower.HasValue?run.ActivePower.Value.ToString().ToUpperInvariant()+"  "+run.Remaining.ToString("0.0")+" sn":run.Mastery>=2?"USTALIK ×"+run.Mastery:run.Assisted?"GÜÇ DESTEKLİ TUR":game.Arcade.Elapsed<10?"YEŞİL: GEÇİT · ALTIN: RİSK / ÖDÜL":"";
            zones.text="MASA: "+(run.TableZones?"BÖLGELER":"KLASİK");domino.text="DOMİNO: "+(run.DominoEnabled?"AÇIK":"KAPALI");
            if(game.State==RunState.Crashed&&!resultDecorated){var label=game.UI.SafeArea.Find("Sonuç/Results").GetComponent<Text>();label.text=label.text.Replace("JOYFUL ROOM","PLAYGROUND")+"\nUSTALIK ×"+run.BestMastery+(run.Assisted?" · GÜÇ DESTEKLİ TUR":" · YARDIMSIZ TUR");resultDecorated=true;}if(game.State==RunState.Running)resultDecorated=false;
            if(front.Page!="Görevler")return;for(int i=0;i<3;i++){var m=achievements?CareerRules.Achievements[badgePage*3+i]:game.Review.Missions[i];float progress=achievements?(m.Metric==4?game.Review.Data.runs:m.Metric==0?game.Review.Data.arcadeDistance:m.Metric==1?game.Review.Data.arcadeStyle:0):m.Metric==0?game.Arcade.Distance:m.Metric==1?game.Arcade.Style.Total:m.Metric==2?game.Arcade.Style.NearMisses:game.Arcade.Challenges.Goals.Pickups;bool done=achievements?(game.Review.Data.achievements&(1<<(badgePage*3+i)))!=0:progress>=m.Target;for(int metric=0;metric<4;metric++)symbols[i,metric].SetActive(metric==Mathf.Clamp(m.Metric,0,3));missionTitles[i].text=(done?"✓ ":"")+m.Title;missionProgress[i].text=(done?"TAMAMLANDI":Mathf.Min(m.Target,Mathf.FloorToInt(progress))+" / "+m.Target)+"   ·   +"+m.Reward+" TP";missionFills[i].rectTransform.anchorMax=new Vector2(done?1:Mathf.Clamp01(progress/m.Target),1);}
        }
    }
}
