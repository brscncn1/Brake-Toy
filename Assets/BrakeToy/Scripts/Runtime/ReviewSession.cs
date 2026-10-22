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
        {game=manager;save=new CareerSave(Path.Combine(Application.persistentDataPath,"career-v1.json"));if(Persist)Data=save.Load();Data.arcadeDistance=Mathf.Max(Data.arcadeDistance,PlayerPrefs.GetFloat("BrakeToy.v03.distance",0));Data.arcadeStyle=Mathf.Max(Data.arcadeStyle,PlayerPrefs.GetInt("BrakeToy.v05.style",0));Missions=CareerRules.ActiveMissions(Data);SelectVisual(Data.selectedCar);}
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
            var a=game.Arcade;LastReward=CareerRules.Settle(Data,runId,new RunSummary(a.Distance,a.Style.Total,a.Style.NearMisses,a.Challenges.Goals.Pickups,Mode),Missions);
            GetComponent<GhostReplay>()?.Finish(a.Distance);GetComponent<ReviewOnline>()?.Submit();if(Persist)SaveFailed=!save.Save(Data);
        }
        public bool BuyOrSelect(int index)
        {if(!CareerRules.Buy(Data,index))return false;SelectVisual(index);if(Persist)SaveFailed=!save.Save(Data);return true;}
        public bool Cosmetic(bool horn)
        {int index=((horn?Data.horn:Data.trail)+1)%3;if(!CareerRules.BuyCosmetic(Data,horn,index))return false;if(Persist)SaveFailed=!save.Save(Data);return true;}
        public void SelectVisual(int index)
        {
            if(Visuals==null||Visuals.Length==0)return;index=Mathf.Clamp(index,0,Visuals.Length-1);
            foreach(var v in Visuals)v.gameObject.SetActive(false);var visual=Visuals[index];visual.gameObject.SetActive(true);game.Player.Visual=visual;
            var wheels=new System.Collections.Generic.List<Transform>();foreach(Transform t in visual)if(t.name.StartsWith("Wheel"))wheels.Add(t);game.Player.GetComponent<ToyWheelAnimation>().Wheels=wheels.ToArray();
        }
    }
}
