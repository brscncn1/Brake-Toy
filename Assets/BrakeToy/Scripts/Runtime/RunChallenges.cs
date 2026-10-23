using BrakeToy.Core;
using UnityEngine;
using UnityEngine.UI;

namespace BrakeToy
{
    [DefaultExecutionOrder(50)]
    public sealed class RunChallenges : MonoBehaviour
    {
        public Transform[] RiskStars;
        public Text GoalLabel;
        public readonly RunGoals Goals=new RunGoals();
        public int PoolExhaustions{get;private set;}
        private GameManager game;
        private ArcadeDirector arcade;
        public void Initialize(GameManager manager,ArcadeDirector director){game=manager;arcade=director;}
        public void ResetRun(){Goals.Reset();PoolExhaustions=0;foreach(var star in RiskStars)star.gameObject.SetActive(false);}
        public void SpawnRisk(ArcadeRow row,float z)
        {
            if(row.Ball || row.Index==0)return;
            float direction=row.SafeLane==0?1:row.SafeLane==2?-1:row.IsBlocked(0)?-1:1;
            float x=ArcadeRules.LaneX(row.SafeLane)+direction*.72f;
            foreach(var star in RiskStars)if(!star.gameObject.activeSelf)
            {star.position=new Vector3(x,.25f,z);star.gameObject.SetActive(true);return;}
            PoolExhaustions++;Debug.LogError("Risk star pool exhausted.");
        }
        public void Step(float moved,float oldX,float newX,bool crashed)
        {
            foreach(var star in RiskStars)
            {
                if(!star.gameObject.activeSelf)continue;
                var a=star.position;var b=a-Vector3.forward*moved;b.y=.25f+Mathf.Sin(arcade.Elapsed*4+a.x)*.035f;star.position=b;
                if(!crashed && (!game.Arcade.Joyful||game.GetComponent<JoyfulRun>().Height<.6f) && SweptCircleBox.TryHit(oldX-a.x,-a.z,newX-b.x,-b.z,game.Config.PlayerHalfWidth,game.Config.PlayerHalfLength,.11f,out _))
                {
                    star.gameObject.SetActive(false);Goals.Collect();arcade.Style.AwardRiskStar();
                    arcade.HUD.Notify("RİSK YILDIZI  +75",new Color(1,.83f,.25f));arcade.Sound.NearMiss(1);
                }
                else if(b.z < -5)star.gameObject.SetActive(false);
            }
            if(!crashed && Goals.Advance(arcade.Distance,arcade.Style.NearMisses))
            {arcade.HUD.Notify("HEDEF TAMAM  "+Goals.Completed+" / 3",new Color(.55f,1,.8f));arcade.Sound.Shift();}
        }
        private void LateUpdate()
        {
            if(!game)return;
            GoalLabel.gameObject.SetActive(game.State==RunState.Running);
            string name=Goals.Completed==0?"60 m sür":Goals.Completed==1?"3 risk yıldızı topla":"6 yakın geçiş yap";
            GoalLabel.text=Goals.Completed>=3?"ÜÇ HEDEF TAMAM  ★ ★ ★":"HEDEF "+(Goals.Completed+1)+"/3  •  "+name+"  "+Goals.Progress(arcade.Distance,arcade.Style.NearMisses)+"/"+Goals.Target;
            if(game.State==RunState.Ready)game.UI.Description.text="Sürükle, aradan sıyrıl.\nYıldızlı geçişi seç: +75 stil.\n\nTUR HEDEFLERİ\n60 m • 3 yıldız • 6 yakın geçiş";
            else if(game.State==RunState.Crashed)game.UI.Description.text+="\nHEDEF "+Goals.Completed+"/3  •  YILDIZ "+Goals.Pickups;
        }
    }
}
