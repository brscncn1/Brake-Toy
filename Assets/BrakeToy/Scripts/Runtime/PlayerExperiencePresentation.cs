using UnityEngine;
using UnityEngine.UI;

namespace BrakeToy
{
    // Adventure-only presentation. Does not change the driving camera or spend powers.
    [DefaultExecutionOrder(400)]
    public sealed class PlayerExperiencePresentation : MonoBehaviour
    {
        private GameManager game;
        private ReviewFrontend front;
        private Transform more;
        private Image noticeBacking, warningBacking;
        private Text tutorial;
        private bool tutorialShown;
        private Transform lessonBacking;
        private Text powerState;
        private PlaygroundRun run;

        private static RectTransform Area(string name, Transform parent, float a, float b, float c, float d)
        {
            var r = new GameObject(name, typeof(RectTransform)).GetComponent<RectTransform>();
            r.SetParent(parent, false); Place(r, a, b, c, d); return r;
        }
        private static void Place(RectTransform r, float a, float b, float c, float d)
        { r.anchorMin = new Vector2(a,b); r.anchorMax = new Vector2(c,d); r.offsetMin = r.offsetMax = Vector2.zero; }
        private static Text Label(Transform parent, string name, string value, int size, float a,float b,float c,float d)
        {
            var t = Area(name,parent,a,b,c,d).gameObject.AddComponent<Text>();
            t.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); t.text=value;
            t.fontSize=size; t.resizeTextForBestFit=true; t.resizeTextMinSize=18; t.resizeTextMaxSize=size;
            t.color=new Color(.04f,.12f,.18f); t.alignment=TextAnchor.MiddleCenter; t.raycastTarget=false; return t;
        }
        private static void Button(Transform parent, string name, UnityEngine.Events.UnityAction action, float a,float b,float c,float d)
        {
            var r=Area(name,parent,a,b,c,d); var image=r.gameObject.AddComponent<Image>();
            image.color=new Color(.04f,.38f,.43f); var button=r.gameObject.AddComponent<Button>();
            button.targetGraphic=image; button.onClick.AddListener(action);
            Label(r,"Label",name,28,.03f,.02f,.97f,.98f).color=Color.white;
        }
        private static Image Back(Text label, string name, Color color)
        {
            var r=Area(name,label.transform.parent,0,0,1,1);
            r.anchorMin=label.rectTransform.anchorMin; r.anchorMax=label.rectTransform.anchorMax;
            r.SetSiblingIndex(label.transform.GetSiblingIndex());
            var image=r.gameObject.AddComponent<Image>(); image.color=color; image.raycastTarget=false; return image;
        }
        private static void Move(Transform root,string name,float a,float b,float c,float d)
        { var t=root.Find(name); if(t)Place((RectTransform)t,a,b,c,d); }
        private void Start()
        {
            game=GetComponent<GameManager>(); front=GetComponent<ReviewFrontend>(); run=GetComponent<PlaygroundRun>(); var safe=game.UI.SafeArea;
            var main=safe.Find("Ana menü");
            Move(main,"ARCADE",.06f,.63f,.94f,.84f);
            Move(main,"GARAJ",.06f,.40f,.49f,.59f); Move(main,"GÖREVLER",.51f,.40f,.94f,.59f);
            Move(main,"AYARLAR",.51f,.17f,.94f,.36f);
            more=Area("Diğer seçenekler",safe,.045f,.035f,.955f,.80f);
            more.gameObject.AddComponent<Image>().color=new Color(.98f,.95f,.87f,.98f);
            Label(more,"Title","DAHA FAZLA MACERA",42,.06f,.85f,.94f,.96f);
            string[] names={"RELAX · 3 HAK","GÜNLÜK ANTRENMAN","SIRALAMA","GÜÇ HAKLARI"};
            for(int i=0;i<names.Length;i++)
            {
                var t=main.Find(names[i]); t.SetParent(more,false);
                Place((RectTransform)t,.07f,.65f-i*.16f,.93f,.78f-i*.16f);
                if(names[i]=="SIRALAMA")t.GetComponentInChildren<Text>().text="YEREL REKORLAR";
                t.GetComponent<Button>().onClick.AddListener(()=>more.gameObject.SetActive(false));
            }
            Button(more,"GERİ",()=>more.gameObject.SetActive(false),.07f,.035f,.93f,.12f);
            Button(main,"DAHA FAZLA",()=>{more.gameObject.SetActive(true);more.SetAsLastSibling();},.06f,.17f,.49f,.36f);
            more.gameObject.SetActive(false);
            var board=safe.Find("Sıralama"); board.Find("Title").GetComponent<Text>().text="YEREL REKORLAR";
            // Adventure has no connected leaderboard. Do not advertise unavailable online actions.
            if(!GetComponent<ReviewOnline>().Configured)
                foreach(var button in board.GetComponentsInChildren<Button>(true))
                    if(button.name!="GERİ")button.gameObject.SetActive(false);
            safe.Find("Görevler/Title").GetComponent<Text>().text="KARİYER GÖREVLERİ";

            var settings=safe.Find("Ayarlar");
            Move(settings,"Settings state",.07f,.68f,.93f,.81f);
            Move(settings,"GHOST AÇ / KAPAT",.07f,.56f,.93f,.65f);
            Move(settings,"YAZI BOYUTUNU DEĞİŞTİR",.07f,.44f,.93f,.53f);
            Move(settings,"GİZLİLİK VE BİLGİ",.07f,.32f,.93f,.41f);
            var audioPage=Area("Ses paneli",safe,.045f,.035f,.955f,.91f);
            audioPage.gameObject.AddComponent<Image>().color=new Color(.98f,.95f,.87f,.99f);
            Label(audioPage,"Title","SES AYARLARI",42,.07f,.83f,.93f,.96f);
            var audio=GetComponent<ToyAudioSettings>().Panel.GetComponent<RectTransform>();
            audio.SetParent(audioPage,false);Place(audio,.07f,.35f,.93f,.75f);
            var sound=game.Arcade.HUD.SoundButton.GetComponent<RectTransform>();
            sound.SetParent(audioPage,false);Place(sound,.07f,.20f,.93f,.30f);
            Button(audioPage,"GERİ",()=>audioPage.gameObject.SetActive(false),.07f,.03f,.93f,.12f);
            Button(settings,"SES",()=>{audioPage.gameObject.SetActive(true);audioPage.SetAsLastSibling();},.07f,.20f,.49f,.29f);
            audioPage.gameObject.SetActive(false);
            var graphicsPage=Area("Görüntü paneli",safe,.045f,.035f,.955f,.91f);
            graphicsPage.gameObject.AddComponent<Image>().color=new Color(.98f,.95f,.87f,.99f);
            Label(graphicsPage,"Title","GÖRÜNTÜ AYARLARI",42,.07f,.83f,.93f,.96f);
            var graphics=GetComponent<GraphicsProfileController>().ToggleButton;
            graphics.transform.SetParent(graphicsPage,false);Place(graphics.GetComponent<RectTransform>(),.07f,.63f,.93f,.75f);
            Move(settings,"GERİ",.07f,.02f,.93f,.09f);
            var zones=settings.Find("MASA BÖLGELERİ");zones.SetParent(graphicsPage,false);Place((RectTransform)zones,.07f,.47f,.93f,.59f);
            var finish=GetComponent<TableFinishSwitch>();
            if(finish)Button(graphicsPage,"MASA KAPLAMASINI DEĞİŞTİR",finish.Toggle,.07f,.31f,.93f,.43f);
            Button(graphicsPage,"GERİ",()=>graphicsPage.gameObject.SetActive(false),.07f,.03f,.93f,.12f);
            Button(settings,"GÖRÜNTÜ",()=>{graphicsPage.gameObject.SetActive(true);graphicsPage.SetAsLastSibling();},.51f,.20f,.93f,.29f);
            graphicsPage.gameObject.SetActive(false);
            var diagnostic=Area("Ölçüm paneli",safe,.045f,.035f,.955f,.91f);
            diagnostic.gameObject.AddComponent<Image>().color=new Color(.98f,.95f,.87f,.99f);
            Label(diagnostic,"Title","PERFORMANS ÖLÇÜMÜ",38,.06f,.82f,.94f,.94f);
            Label(diagnostic,"Info","Ölçümü başlat, geri dön ve 60 saniye sür.\nSonuç yalnız bu cihazda kaydedilir.\nDokunma ve ısınma ayrıca değerlendirilir.",28,.07f,.40f,.93f,.77f);
            var probe=GetComponent<PerformanceProbe>(); probe.CaptureButton.transform.SetParent(diagnostic,false);
            Place(probe.CaptureButton.GetComponent<RectTransform>(),.07f,.25f,.93f,.36f);
            Button(diagnostic,"GERİ",()=>diagnostic.gameObject.SetActive(false),.07f,.03f,.93f,.12f);
            Button(settings,"ÖLÇÜM",()=>{diagnostic.gameObject.SetActive(true);diagnostic.SetAsLastSibling();},.07f,.105f,.93f,.18f);
            diagnostic.gameObject.SetActive(false);
            // Text remains in stable screen bands; backing never intercepts steering.
            var hud=game.Arcade.HUD;
            Place(game.Arcade.Challenges.GoalLabel.rectTransform,.05f,.775f,.95f,.835f);
            Back(game.Arcade.Challenges.GoalLabel,"Goal backing",new Color(1,.98f,.89f,.94f));
            Place(hud.NoticeLabel.rectTransform,.07f,.69f,.93f,.75f);
            noticeBacking=Back(hud.NoticeLabel,"Notice backing",new Color(1,.98f,.89f,.94f));
            Place(hud.WarningLabel.rectTransform,.12f,.62f,.88f,.68f);
            warningBacking=Back(hud.WarningLabel,"Warning backing",new Color(.035f,.10f,.16f,.94f));
            foreach(var t in new[]{hud.NoticeLabel,hud.WarningLabel,game.Arcade.Challenges.GoalLabel})
            {t.resizeTextForBestFit=true;t.resizeTextMinSize=18;t.resizeTextMaxSize=27;}
            tutorial=Label(hud.Driving.transform,"Driving lesson","SÜRÜKLE: YÖN VER\nYeşil ok: rampa · Havada ek TP",26,.07f,.22f,.93f,.29f);
            lessonBacking=Back(tutorial,"Lesson backing",new Color(1,.98f,.89f,.92f)).transform;
            powerState=hud.Driving.transform.Find("Active power").GetComponent<Text>();
            powerState.resizeTextForBestFit=true;powerState.resizeTextMinSize=18;powerState.resizeTextMaxSize=25;
        }
        private void LateUpdate()
        {
            if(!game)return;
            noticeBacking.enabled=game.Arcade.HUD.NoticeLabel.enabled;
            warningBacking.enabled=!string.IsNullOrEmpty(game.Arcade.HUD.WarningLabel.text);
            bool lesson=game.State==RunState.Running&&!tutorialShown&&game.Arcade.Elapsed<6;
            tutorial.gameObject.SetActive(lesson);lessonBacking.gameObject.SetActive(lesson);
            if(game.State==RunState.Running&&game.Arcade.Elapsed>=6)tutorialShown=true;
            if(game.State==RunState.Running)more.gameObject.SetActive(false);
            if(game.State==RunState.Running&&game.Arcade.Elapsed<10)
            {
                if(!run.ActivePower.HasValue&&!run.Assisted&&run.Mastery<2)powerState.text="";
            }
        }
    }
}
