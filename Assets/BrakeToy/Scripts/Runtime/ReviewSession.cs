using System;
using System.IO;
using BrakeToy.Core;
using UnityEngine;
namespace BrakeToy
{
    [DefaultExecutionOrder(-50)]
    public sealed class ReviewSession:MonoBehaviour
    {
        public VehicleDefinition[] Vehicles;
        public Transform[] Visuals;
        public PlayMode Mode=PlayMode.Arcade;
        public bool Persist=true;
        public CareerData Data{get;private set;}=new CareerData();
        public MissionDefinition[] Missions{get;private set;}
        public int Lives{get;private set;}=3;
        public int LastReward{get;private set;}
        public bool SaveFailed{get;private set;}
        public bool Recovered=>save!=null&&save.Recovered;
        private CareerSave save;private string runId;private GameManager game;private bool settled;
        public void Initialize(GameManager manager)
        {game=manager;bool joyful=GetComponent<JoyfulRun>();string path=Path.Combine(Application.persistentDataPath,GetComponent<PlaygroundRun>()?"career-playground-v1.json":joyful?"career-joyful-v1.json":"career-v1.json");save=new CareerSave(path);if(Persist){Data=save.Load();if(joyful&&!File.Exists(path)){string legacy=GetComponent<PlaygroundRun>()&&File.Exists(Path.Combine(Application.persistentDataPath,"career-joyful-v1.json"))?"career-joyful-v1.json":"career-v1.json";Data=new CareerSave(Path.Combine(Application.persistentDataPath,legacy)).Load();Data.arcadeDistance=Data.relaxDistance=Data.dailyDistance=0;Data.arcadeStyle=Data.relaxStyle=Data.dailyStyle=0;Data.dailyDate="";}}Data.arcadeDistance=Mathf.Max(Data.arcadeDistance,PlayerPrefs.GetFloat(game.Arcade.DistanceRecordKey,0));Data.arcadeStyle=Mathf.Max(Data.arcadeStyle,PlayerPrefs.GetInt(GetComponent<PlaygroundRun>()?"BrakeToy.v08.style":joyful?"BrakeToy.v07.style":"BrakeToy.v05.style",0));Missions=CareerRules.ActiveMissions(Data);SelectVisual(Data.selectedCar);}
        public void NewRun(){runId=Guid.NewGuid().ToString("N");settled=false;LastReward=0;Lives=Mode==PlayMode.Relax?3:1;Missions=CareerRules.ActiveMissions(Data);GetComponent<GhostReplay>()?.Begin(game.Arcade.CurrentSeed,Mode);GetComponent<ReviewOnline>()?.BeginRecord();}
        public bool SurviveHit()
        {
            Lives=Mathf.Max(0,Lives-1);if(Mode!=PlayMode.Relax||Lives==0)return false;
            foreach(var hazard in game.Arcade.Pool)if(hazard.InUse&&hazard.transform.position.z<14)hazard.Release();
            foreach(var star in game.Arcade.Challenges.RiskStars)if(star.gameObject.activeSelf&&star.position.z<14)star.gameObject.SetActive(false);
            game.Input.ResetCapture();game.Player.EndDrag();game.Arcade.HUD.Notify("DEVAM!  "+Lives+" HAK KALDI",new Color(.6f,1,.7f));return true;
        }
        public void Finish()
        {
            if(settled)return;settled=true;
            var a=game.Arcade;var jump=GetComponent<JoyfulRun>();LastReward=CareerRules.Settle(Data,runId,new RunSummary(a.Distance,a.Style.Total,a.Style.NearMisses,a.Challenges.Goals.Pickups,Mode,jump?jump.AirDistance:0,jump?jump.AirStyle:0),Missions);
            GetComponent<GhostReplay>()?.Finish(a.Distance);GetComponent<ReviewOnline>()?.Submit();if(Persist)SaveFailed=!save.Save(Data);
        }
        public bool BuyOrSelect(int index)
        {if(!CareerRules.Buy(Data,index))return false;SelectVisual(index);if(Persist)SaveFailed=!save.Save(Data);return true;}
        public bool Cosmetic(bool horn)
        {int index=((horn?Data.horn:Data.trail)+1)%3;if(!CareerRules.BuyCosmetic(Data,horn,index))return false;if(Persist)SaveFailed=!save.Save(Data);return true;}
        public void InitializePowers(){if(!PlaygroundRules.EnsureStarter(Data))return;if(Persist)SaveFailed=!save.Save(Data);}
        public bool UsePower(ToyPower power)
        {int old=PlaygroundRules.Charges(Data,power);if(!PlaygroundRules.Spend(Data,power))return false;if(Persist&&!save.Save(Data)){if(power==ToyPower.Slow)Data.slowCharges=old;else if(power==ToyPower.Rocket)Data.rocketCharges=old;else Data.jetCharges=old;SaveFailed=true;return false;}SaveFailed=false;return true;}
        public bool RefillPower(ToyPower power)
        {int points=Data.points,old=PlaygroundRules.Charges(Data,power);if(!PlaygroundRules.Refill(Data,power))return false;if(Persist&&!save.Save(Data)){Data.points=points;if(power==ToyPower.Slow)Data.slowCharges=old;else if(power==ToyPower.Rocket)Data.rocketCharges=old;else Data.jetCharges=old;SaveFailed=true;return false;}SaveFailed=false;return true;}
        public void SelectVisual(int index)
        {
            if(Visuals==null||Visuals.Length==0)return;index=Mathf.Clamp(index,0,Visuals.Length-1);
            foreach(var v in Visuals)v.gameObject.SetActive(false);var visual=Visuals[index];visual.gameObject.SetActive(true);game.Player.Visual=visual;
            var wheels=new System.Collections.Generic.List<Transform>();foreach(Transform t in visual)if(t.name.StartsWith("Wheel"))wheels.Add(t);game.Player.GetComponent<ToyWheelAnimation>().Wheels=wheels.ToArray();
        }
    }
}
