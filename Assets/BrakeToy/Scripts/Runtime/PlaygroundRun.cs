using BrakeToy.Core;
using UnityEngine;
namespace BrakeToy
{
    public sealed class PlaygroundRun:MonoBehaviour
    {
        public bool TableZones=true,DominoEnabled=true;public bool PersistPreferences=true;
        public ToyPower? ActivePower{get;private set;}public float Remaining{get;private set;}public bool Assisted{get;private set;}
        public int Mastery{get;private set;}public int BestMastery{get;private set;}
        public float WorldFactor=>ActivePower==ToyPower.Slow?Mathf.Lerp(1,.32f,Mathf.SmoothStep(0,1,Remaining/.25f)):ActivePower==ToyPower.Rocket?1+.85f*Mathf.SmoothStep(0,1,Remaining/.35f)*Mathf.SmoothStep(0,1,powerAge/.15f):1;
        private const float JetDescentSeconds = .45f;
        public bool Protected => ActivePower == ToyPower.Rocket
            || (ActivePower == ToyPower.Jetpack && Remaining <= JetDescentSeconds)
            || grace > 0;
        private GameManager game;private float grace,powerAge,masteryLeft;private int lastMiss,lastStars,lastJumps,mask;private bool wasAir;
        public string Status{get;private set;}="";
        public Transform PowerRig,RocketRig,JetRig;
        private void Awake(){game=GetComponent<GameManager>();TableZones=PlayerPrefs.GetInt("BrakeToy.v08.zones",1)!=0;DominoEnabled=!GetComponent<TidyRoom>()&&PlayerPrefs.GetInt("BrakeToy.v08.domino",1)!=0;}
        private void Start(){game.Review.InitializePowers();}
        public void ResetRun(){game=GetComponent<GameManager>();ActivePower=null;Remaining=grace=powerAge=0;Assisted=false;Mastery=BestMastery=mask=lastMiss=lastStars=lastJumps=0;masteryLeft=0;wasAir=false;GetComponent<PlaygroundEncounters>()?.ResetRun();GetComponent<AdventureEncounters>()?.ResetRun();}
        public bool Activate(ToyPower power)
        {
            if(game.State!=RunState.Running||ActivePower.HasValue||grace>0)return false;
            if(!game.Review.UsePower(power)){Status=game.Review.SaveFailed?"Hak kaydedilemedi":"Hak yok — menüde TP ile yenile";return false;}
            ActivePower=power;Remaining=PlaygroundRules.Duration(power);powerAge=0;Assisted=true;
            game.Arcade.HUD.Notify(power==ToyPower.Slow?"ZAMAN YAVAŞLADI · 3 SN":power==ToyPower.Rocket?"KORUMALI ROKET · 3 SN":"JETPACK · 5 SN",new Color(.08f,.24f,.36f));return true;
        }
        public float JetHeight(float ahead=0)
        {if(ActivePower!=ToyPower.Jetpack)return 0;float age=powerAge+ahead,left=Remaining-ahead;return 2.25f*Mathf.Min(Mathf.SmoothStep(0,1,age/.35f),Mathf.SmoothStep(0,1,Mathf.Max(0,left)/JetDescentSeconds));}
        public void Tick(float dt,float moved,float oldX,float x)
        {
            grace=Mathf.Max(0,grace-dt);
            if (ActivePower.HasValue)
            {
                bool enteringJetDescent = ActivePower == ToyPower.Jetpack && Remaining > JetDescentSeconds;
                powerAge += dt;
                Remaining = Mathf.Max(0, Remaining - dt);
                // Landing starts before the timer expires: clear and protect the actual descent.
                if (enteringJetDescent && Remaining <= JetDescentSeconds && Remaining > 0) ClearLanding();
                if (Remaining <= 0)
                {
                    var ended = ActivePower.Value;
                    ActivePower = null;
                    if (ended != ToyPower.Slow) { grace = .85f; ClearLanding(); }
                    game.Arcade.HUD.Notify("GÜÇ BİTTİ · DEVAM!", new Color(.1f,.3f,.4f));
                }
            }
            var a=game.Arcade;var jump=GetComponent<JoyfulRun>();int stars=a.Challenges.Goals.Pickups;
            if(a.Style.NearMisses>lastMiss)Skill(1);if(stars>lastStars)Skill(2);if(jump.Jumps>lastJumps)Skill(4);if(wasAir&&!jump.Airborne&&jump.Height<.08f)Skill(8);
            lastMiss=a.Style.NearMisses;lastStars=stars;lastJumps=jump.Jumps;wasAir=jump.Airborne;
            masteryLeft=Mathf.Max(0,masteryLeft-dt);if(masteryLeft<=0){mask=0;Mastery=0;}
            GetComponent<PlaygroundEncounters>()?.Advance(dt,moved,oldX,x);GetComponent<AdventureEncounters>()?.Advance(dt,moved,oldX,x);
        }
        private void ClearLanding(){foreach(var h in game.Arcade.Pool)if(h.InUse&&h.transform.position.z> -2&&h.transform.position.z<game.Arcade.Speed*.85f)h.Release();GetComponent<PlaygroundEncounters>()?.ClearLanding(game.Arcade.Speed*.85f);GetComponent<AdventureEncounters>()?.ClearLanding(game.Arcade.Speed*.85f);}
        public void Skill(int bit)
        {if((mask&bit)!=0)return;mask|=bit;masteryLeft=7;Mastery=Mathf.Min(4,Mastery+1);BestMastery=Mathf.Max(BestMastery,Mastery);if(Mastery>=2){game.Arcade.Style.AwardMastery(50*Mastery);game.Arcade.HUD.Notify("USTALIK ×"+Mastery+"  +"+(50*Mastery),new Color(.24f,.08f,.40f));}if(Mastery==4)mask=0;}
        public void EndRun(){ActivePower=null;Remaining=grace=0;GetComponent<JoyfulRun>().StopFlight();}
        public void ToggleZones(){TableZones=!TableZones;if(PersistPreferences){PlayerPrefs.SetInt("BrakeToy.v08.zones",TableZones?1:0);PlayerPrefs.Save();}GetComponent<PlaygroundAtmosphere>()?.ApplyZones();}
        public void ToggleDomino(){DominoEnabled=!DominoEnabled;if(PersistPreferences){PlayerPrefs.SetInt("BrakeToy.v08.domino",DominoEnabled?1:0);PlayerPrefs.Save();}if(!DominoEnabled)GetComponent<PlaygroundEncounters>()?.DisableDomino();}
        private void LateUpdate(){if(!PowerRig||!game||!game.Player.Visual)return;PowerRig.SetPositionAndRotation(game.Player.Visual.position,game.Player.Visual.rotation);RocketRig.gameObject.SetActive(ActivePower==ToyPower.Rocket);JetRig.gameObject.SetActive(ActivePower==ToyPower.Jetpack);}
    }
}
