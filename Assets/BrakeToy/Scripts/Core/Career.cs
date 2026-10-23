using System;
namespace BrakeToy.Core
{
    public enum PlayMode { Arcade, Relax, Daily }
    [Serializable] public sealed class CareerData
    {
        public int version=1,points,selectedCar,unlocked=1,achievements,completedMissions,runs,arcadeRuns,relaxRuns;
        public long totalDistance;
        public float arcadeDistance,relaxDistance,dailyDistance;
        public int arcadeStyle,relaxStyle,dailyStyle;
        public string lastRun="",dailyDate="";
        public string[] recentRuns=new string[32];public int recentCursor,trail,horn,ownedTrails=1,ownedHorns=1;
    }
    [Serializable] public sealed class MissionDefinition
    {
        public string Title;public int Metric,Target,Reward;
        public MissionDefinition(string title,int metric,int target,int reward){Title=title;Metric=metric;Target=target;Reward=reward;}
    }
    public readonly struct RunSummary
    {
        public readonly float Distance,AirDistance;public readonly int AirStyle; public readonly int Style,NearMisses,Stars;public readonly PlayMode Mode;
        public RunSummary(float distance,int style,int misses,int stars,PlayMode mode,float airDistance=0,int airStyle=0){AirDistance=airDistance;AirStyle=airStyle;Distance=distance;Style=style;NearMisses=misses;Stars=stars;Mode=mode;}
        public float Metric(int metric)=>metric==0?Distance:metric==1?Style:metric==2?NearMisses:Stars;
    }
    public static class CareerRules
    {
        public static readonly int[] Costs={0,200,500,900,1500,2400,3600,5200,7200,10000};
        public static readonly MissionDefinition[] Missions={new MissionDefinition("150 m sür",0,150,25),new MissionDefinition("3 yakın geçiş",2,3,30),new MissionDefinition("500 stil kazan",1,500,30),new MissionDefinition("3 yıldız topla",3,3,25),new MissionDefinition("300 m sür",0,300,45),new MissionDefinition("5 yakın geçiş",2,5,45),new MissionDefinition("1000 stil kazan",1,1000,50),new MissionDefinition("5 yıldız topla",3,5,40),new MissionDefinition("500 m sür",0,500,65)};
        public static readonly MissionDefinition[] Achievements={new MissionDefinition("İlk tur",4,1,20),new MissionDefinition("100 metre",0,100,20),new MissionDefinition("300 metre",0,300,30),new MissionDefinition("500 metre",0,500,40),new MissionDefinition("1000 metre",0,1000,75),new MissionDefinition("İlk yakın geçiş",2,1,15),new MissionDefinition("5 yakın geçiş",2,5,30),new MissionDefinition("10 yakın geçiş",2,10,60),new MissionDefinition("500 stil",1,500,25),new MissionDefinition("2000 stil",1,2000,50),new MissionDefinition("5000 stil",1,5000,100),new MissionDefinition("3 yıldız",3,3,25),new MissionDefinition("10 yıldız",3,10,60),new MissionDefinition("5 tur",4,5,40),new MissionDefinition("20 tur",4,20,100)};
        public static float Speed(float seconds,PlayMode mode)=>mode==PlayMode.Relax?7+3*(1-(float)Math.Exp(-Math.Max(0,seconds)/35)):ArcadeRules.Speed(seconds);
        public static float Distance(float seconds,PlayMode mode){seconds=Math.Max(0,seconds);return mode==PlayMode.Relax?10*seconds-105*(1-(float)Math.Exp(-seconds/35)):ArcadeRules.Distance(seconds);}
        public static bool Buy(CareerData data,int car)
        {if(car<0||car>=Costs.Length)return false;int bit=1<<car;if((data.unlocked&bit)==0){if(data.points<Costs[car])return false;data.points-=Costs[car];data.unlocked|=bit;}data.selectedCar=car;return true;}
        public static bool BuyCosmetic(CareerData data,bool horn,int index)
        {if(index<0||index>2)return false;int owned=horn?data.ownedHorns:data.ownedTrails,bit=1<<index;if((owned&bit)==0){if(data.points<100)return false;data.points-=100;owned|=bit;}if(horn){data.ownedHorns=owned;data.horn=index;}else{data.ownedTrails=owned;data.trail=index;}return true;}
        public static int Settle(CareerData data,string runId,RunSummary run,MissionDefinition[] missions)
        {
            if(string.IsNullOrEmpty(runId)||data.lastRun==runId||Array.IndexOf(data.recentRuns,runId)>=0||!float.IsFinite(run.Distance)||run.Distance<0||run.Distance>10000000||run.Style<0||!float.IsFinite(run.AirDistance)||run.AirDistance<0||run.AirStyle<0)return 0;
            data.recentRuns[data.recentCursor]=runId;data.recentCursor=(data.recentCursor+1)%data.recentRuns.Length;
            data.lastRun=runId;data.runs++;data.totalDistance+=(long)run.Distance;
            if(run.Mode==PlayMode.Relax){data.relaxRuns++;data.relaxDistance=Math.Max(data.relaxDistance,run.Distance);data.relaxStyle=Math.Max(data.relaxStyle,run.Style);}
            else if(run.Mode==PlayMode.Arcade){data.arcadeRuns++;data.arcadeDistance=Math.Max(data.arcadeDistance,run.Distance);data.arcadeStyle=Math.Max(data.arcadeStyle,run.Style);}
            else{data.dailyDistance=Math.Max(data.dailyDistance,run.Distance);data.dailyStyle=Math.Max(data.dailyStyle,run.Style);}
            int reward=Math.Min(2500,(int)(run.Distance/10)+run.Style/100+(int)(Math.Max(0,Math.Min(run.Distance,run.AirDistance))/10)+Math.Max(0,Math.Min(run.Style,run.AirStyle))/100);
            foreach(var m in missions)if(run.Metric(m.Metric)>=m.Target){reward+=m.Reward;data.completedMissions++;}
            for(int i=0;i<Achievements.Length;i++){var a=Achievements[i];if((data.achievements&(1<<i))==0&&(a.Metric==4?data.runs:run.Metric(a.Metric))>=a.Target){data.achievements|=1<<i;reward+=a.Reward;}}
            data.points=(int)Math.Min(int.MaxValue,(long)data.points+reward);return reward;
        }
        public static MissionDefinition[] ActiveMissions(CareerData data)
        {var result=new MissionDefinition[3];int start=(data.runs/2*3)%Missions.Length;for(int i=0;i<3;i++)result[i]=Missions[(start+i)%Missions.Length];return result;}
        public static bool IsValid(CareerData d)=>d!=null&&d.version==1&&d.points>=0&&d.runs>=0&&d.arcadeRuns>=0&&d.relaxRuns>=0&&d.totalDistance>=0&&d.completedMissions>=0&&d.unlocked>0&&d.unlocked<1024&&(d.unlocked&1)!=0&&d.selectedCar>=0&&d.selectedCar<10&&(d.unlocked&(1<<d.selectedCar))!=0&&ValidDistance(d.arcadeDistance)&&ValidDistance(d.relaxDistance)&&ValidDistance(d.dailyDistance)&&d.arcadeStyle>=0&&d.relaxStyle>=0&&d.dailyStyle>=0&&d.achievements>=0&&d.achievements<(1<<15)&&d.recentRuns!=null&&d.recentRuns.Length==32&&d.recentCursor>=0&&d.recentCursor<32&&ValidCosmetic(d.trail,d.ownedTrails)&&ValidCosmetic(d.horn,d.ownedHorns);
        private static bool ValidDistance(float value)=>float.IsFinite(value)&&value>=0;
        private static bool ValidCosmetic(int selected,int owned)=>selected>=0&&selected<3&&owned>0&&owned<8&&(owned&1)!=0&&(owned&(1<<selected))!=0;
    }
}
