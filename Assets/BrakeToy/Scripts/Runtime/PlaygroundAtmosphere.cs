using UnityEngine;
namespace BrakeToy
{
    [DefaultExecutionOrder(500)]
    public sealed class PlaygroundAtmosphere:MonoBehaviour
    {
        public GameObject[] Zones;private PlaygroundRun run;private GameManager game;private int previousZone=-1;private Vector3[][] starts;
        private void Start(){run=GetComponent<PlaygroundRun>();game=GetComponent<GameManager>();starts=new Vector3[Zones.Length][];for(int i=0;i<Zones.Length;i++){starts[i]=new Vector3[Zones[i].transform.childCount];for(int n=0;n<starts[i].Length;n++)starts[i][n]=Zones[i].transform.GetChild(n).localPosition;}ApplyZones();}
        public void ApplyZones(){if(!run)run=GetComponent<PlaygroundRun>();int selected=game?Mathf.FloorToInt(game.Arcade.Distance/220)%3:0;for(int i=0;i<Zones.Length;i++)Zones[i].SetActive(run.TableZones&&i==selected);}
        private void LateUpdate()
        {
            if(!game)return;if(!GetComponent<TidyRoom>())for(int i=0;i<Zones.Length;i++)for(int n=0;n<starts[i].Length;n++){var p=starts[i][n];p.z=Mathf.Repeat(p.z+6-game.Arcade.Distance,42)-6;Zones[i].transform.GetChild(n).localPosition=p;}var cycle=GetComponent<RoomDayCycle>();float day=Mathf.SmoothStep(0,1,Mathf.InverseLerp(-.18f,.3f,Mathf.Cos((cycle.TimeOfDay-.25f)*Mathf.PI*2)));
            cycle.SunLight.intensity=Mathf.Lerp(.3f,1.45f,day);cycle.SunLight.shadows=LightShadows.Soft;cycle.SunLight.shadowStrength=.70f;
            RenderSettings.ambientIntensity=.7f;RenderSettings.ambientSkyColor=Color.Lerp(new Color(.14f,.20f,.37f),new Color(.37f,.48f,.62f),day);RenderSettings.ambientEquatorColor=Color.Lerp(new Color(.14f,.13f,.22f),new Color(.26f,.29f,.33f),day);RenderSettings.ambientGroundColor=new Color(.13f,.10f,.09f);
            cycle.RoomLamp.intensity=Mathf.Lerp(1.4f,.18f,day);int zone=Mathf.FloorToInt(game.Arcade.Distance/220)%3;if(zone!=previousZone){previousZone=zone;ApplyZones();if(game.State==RunState.Running&&run.TableZones)game.Arcade.HUD.Notify(zone==0?"OYUNCAK KASABASI":zone==1?"YAPIM ALANI":"YARIŞ KÖŞESİ",new Color(.12f,.22f,.3f));}
        }
    }
}
