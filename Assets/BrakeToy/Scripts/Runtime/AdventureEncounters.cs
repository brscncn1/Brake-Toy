using System;
using BrakeToy.Core;
using UnityEngine;
namespace BrakeToy
{
    public sealed class AdventureEncounters:MonoBehaviour
    {
        public AdventureToy[] Templates;public int SlotsPerKind=6;public AdventureToy[] Pool{get;private set;}
        public int PoolExhaustions{get;private set;}public float SurfaceFactor{get;private set;}=1;public bool OnIce{get;private set;}
        private static readonly string[] labels={"KARTON TÜNEL · YEŞİL ÇIKIŞ AÇIK","KURMALI OYUNCAK · HAREKETİNİ OKU","VİNÇ · GÖLGEYİ TAKİP ET","TREN GEÇİDİ · VAGONLARA DİKKAT","MİSKET AKIŞI · YEŞİL YOL AÇIK","BANT YAVAŞLATIR · KART KAYDIRIR","CETVEL · YEŞİL OKTAN ZIPLA","GARAJ · KAPI VE FARLARA DİKKAT"};
        private GameManager game;private JoyfulRun jump;private PlaygroundRun run;
        private void Bind(){if(game)return;game=GetComponent<GameManager>();jump=GetComponent<JoyfulRun>();run=GetComponent<PlaygroundRun>();}
        public void ResetRun()
        {
            Bind();game.Player.SetSurfaceProvider(this);if(Pool==null){Pool=new AdventureToy[Templates.Length*SlotsPerKind];for(int k=0;k<Templates.Length;k++)for(int i=0;i<SlotsPerKind;i++){var a=Instantiate(Templates[k],transform);a.name="Pooled "+a.Kind+" "+i;Pool[k*SlotsPerKind+i]=a;}}
            foreach(var a in Pool)a.Release();PoolExhaustions=0;SurfaceFactor=1;OnIce=false;
        }
        public AdventureToy Place(TrackToy kind,PlaygroundBeat beat,float z)
        {
            Bind();if(Pool==null)ResetRun();foreach(var a in Pool)if(a.Kind==kind&&!a.InUse){a.InUse=true;a.Launched=a.Notified=false;a.SafeLane=beat.SafeLane;a.RiskLane=beat.RiskLane;a.Variant=beat.Variant;a.Phase=(beat.Variant%63)*.1f;a.transform.position=new Vector3(kind==TrackToy.Tunnel?0:beat.RiskX,0,z*(kind==TrackToy.Marbles?a.TravelFactor:1));a.gameObject.SetActive(true);
                if(kind==TrackToy.Tunnel){for(int i=0;i<3;i++)a.Moving[i].gameObject.SetActive(i!=beat.SafeLane&&i!=beat.RiskLane);}
                if(kind==TrackToy.Train)for(int i=0;i<a.Moving.Length;i++)a.Moving[i].gameObject.SetActive(beat.SafeLane!=1||i==0);
                if(kind==TrackToy.Marbles)for(int i=0;i<a.Moving.Length;i++)a.Moving[i].gameObject.SetActive(i<=beat.Variant%3);
                if(kind==TrackToy.Tunnel&&a.ExitArrows!=null)for(int i=0;i<a.ExitArrows.Length;i++){a.ExitArrows[i].gameObject.SetActive(i==beat.SafeLane||i==beat.RiskLane);foreach(var r in a.ExitArrows[i].GetComponentsInChildren<Renderer>())r.sharedMaterial=i==beat.SafeLane?GetComponent<PlaygroundEncounters>().SafeMaterial:GetComponent<PlaygroundEncounters>().RiskMaterial;}
                if(kind==TrackToy.Marbles)foreach(var m in a.Moving)m.localRotation=Quaternion.identity;
                if(kind==TrackToy.Windup&&a.Options!=null)for(int i=0;i<a.Options.Length;i++)a.Options[i].gameObject.SetActive(i==(beat.Variant%2==0?1:0));
                if(kind==TrackToy.Surfaces)for(int i=0;i<a.Options.Length;i++)a.Options[i].gameObject.SetActive(i==beat.Variant%2);a.Pose();return a;}PoolExhaustions++;throw new InvalidOperationException("Adventure pool exhausted "+kind);
        }
        public void Spawn(PlaygroundBeat b,float z,Func<int,float,float,float,float,ArcadeHazard> spawn)
        {
            Bind();Place((TrackToy)b.Family,b,z+3);var e=GetComponent<PlaygroundEncounters>();e.MarkRoute(b.SafeX,z,false);e.MarkRoute(b.RiskX,z,true);
            int third=3-b.SafeLane-b.RiskLane;spawn(b.Variant%2==0?4:2,ArcadeRules.LaneX(third),z+23,0,0);
            game.Arcade.Challenges.SpawnAt(b.RiskX,z-6);game.Arcade.Challenges.SpawnAt(b.RiskX,z-2);
            // Approach -> toy decision -> optional jump/landing. Every phrase ends before +30 m.
            if(b.Family!=(int)TrackToy.Seesaw){float offset=b.Family==(int)TrackToy.Tunnel?11:17;jump.SpawnRamp(b.RiskX,z+offset);game.Arcade.Challenges.SpawnAt(b.RiskX,z+offset+9,1.8f);}
            else game.Arcade.Challenges.SpawnAt(b.RiskX,z+12,1.8f);
            if(b.Family==(int)TrackToy.Tunnel)Place(TrackToy.Windup,b,z+23);
            else if(b.Family==(int)TrackToy.Surfaces)Place(TrackToy.Marbles,b,z+10);
        }
        private Vector3 Centre(AdventureToy a,AdventureSolid s,float moved)
        {var p=a.transform.position-Vector3.forward*(moved*a.TravelFactor);int i=Array.IndexOf(a.Moving,s.Part);if(i>=0)p+=a.Motion(i,p.z);return p+s.Centre;}
        public bool FirstHit(float dt,float moved,float fromX,float toX,out float fraction)
        {
            Bind();fraction=1;bool hit=false;foreach(var a in Pool){if(!a.InUse)continue;foreach(var s in a.Solids){if(s.Part&&!s.Part.gameObject.activeSelf)continue;var p=Centre(a,s,0);var q=Centre(a,s,moved);float h0=jump.Height,h1=jump.PredictHeight(dt,moved,toX);if(Mathf.Min(h0,h1)>Mathf.Max(p.y,q.y)+s.Half.y+.02f||Mathf.Max(h0,h1)+.6f<Mathf.Min(p.y,q.y)-s.Half.y)continue;
                float t;bool contact=s.Sphere?SweptCircleBox.TryHit(fromX-p.x,-p.z,toX-q.x,-q.z,game.Config.PlayerHalfWidth,game.Config.PlayerHalfLength,s.Half.x,out t):SweptCollision.TryHit(fromX-p.x,-p.z,toX-q.x,-q.z,game.Config.PlayerHalfWidth+s.Half.x,game.Config.PlayerHalfLength+s.Half.z,out t);
                if(contact&&run.Protected){a.Release();break;}if(contact&&t<=fraction){hit=true;fraction=t;}}}return hit;
        }
        public float SurfaceHeight(float x,float moved)
        {
            if(Pool==null)return 0;float h=0;foreach(var a in Pool)if(a.InUse&&a.Kind==TrackToy.Seesaw&&!a.Launched){float z=-a.transform.position.z+moved;if(Mathf.Abs(x-a.transform.position.x)<.85f+game.Config.PlayerHalfWidth-.015f&&z>=-2.6f-game.Config.PlayerHalfLength&&z<=2.6f+game.Config.PlayerHalfLength)h=Mathf.Max(h,Mathf.Lerp(.04f,.59f,Mathf.InverseLerp(-2.6f,2.6f,z+game.Config.PlayerHalfLength)));}return h;
        }
        public void Advance(float dt,float moved,float oldX,float x)
        {
            Bind();SurfaceFactor=1;OnIce=false;foreach(var a in Pool){if(!a.InUse)continue;float before=a.transform.position.z;a.Move(moved);float z=a.transform.position.z;
                if(!a.Notified&&z<game.Arcade.Speed*2*a.TravelFactor&&z>0){a.Notified=true;game.Arcade.HUD.Notify(labels[(int)a.Kind],new Color(.07f,.26f,.30f));}
                if(a.Kind==TrackToy.Surfaces&&jump.Height<.09f&&Mathf.Abs(x-a.transform.position.x)<.85f&&Mathf.Abs(z)<3){if(a.Variant%2==0)SurfaceFactor=AdventureRules.TapeSpeed(false);else OnIce=true;}
                if(z< -14)a.Release();}
        }
        public void TryRulerLaunch(float moved,float x)
        {if(Pool==null)return;float lip=-2.6f-game.Config.PlayerHalfLength;foreach(var a in Pool)if(a.InUse&&a.Kind==TrackToy.Seesaw&&!a.Launched&&a.transform.position.z>lip&&a.transform.position.z-moved<=lip&&Mathf.Abs(x-a.transform.position.x)<.85f+game.Config.PlayerHalfWidth-.015f&&jump.Height>.12f){a.Launched=true;jump.LaunchFromToy(.59f);}}
        public void ClearLanding(float distance){if(Pool!=null)foreach(var a in Pool)if(a.InUse&&a.transform.position.z<distance&&a.transform.position.z> -10)a.Release();SurfaceFactor=1;OnIce=false;}
    }
}
