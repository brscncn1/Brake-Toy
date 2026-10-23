using System;
using BrakeToy.Core;
using NUnit.Framework;
using PlayMode=BrakeToy.Core.PlayMode;
namespace BrakeToy.Tests
{
    public sealed class JoyfulRulesTests
    {
        [Test]public void DifficultyStartsImmediatelyAndDistanceIsConsistent()
        {Assert.That(JoyfulRules.Speed(0),Is.EqualTo(14));Assert.That(JoyfulRules.Speed(10),Is.GreaterThan(22));Assert.That(JoyfulRules.Speed(30),Is.GreaterThan(28));Assert.That(JoyfulRules.Speed(600),Is.InRange(29.9f,30.01f));for(int t=0;t<=600;t++){Assert.That(JoyfulRules.Distance(t),Is.GreaterThanOrEqualTo(0));Assert.That(JoyfulRules.TimeAtDistance(JoyfulRules.Distance(t)),Is.EqualTo(t).Within(.001f));Assert.That(JoyfulRules.Speed(t,true),Is.LessThan(JoyfulRules.Speed(t)));}}
        [Test]public void PatternMemoryAndReachableTransitionsHoldFor128000Rows()
        {for(int seed=1;seed<=256;seed++){var a=new JoyfulSequence(seed);var b=new JoyfulSequence(seed);var history=new int[8];for(int j=0;j<8;j++)history[j]=-1;JoyfulRow previous=default;for(int i=0;i<500;i++){var r=a.Next();Assert.That(r.Pattern,Is.EqualTo(b.Next().Pattern));Assert.That(Array.IndexOf(history,r.Pattern),Is.EqualTo(-1));history[i%8]=r.Pattern;Assert.That(r.SafeLane,Is.InRange(0,2));Assert.That(r.Balls,Is.InRange(0,3));if(i>0){float interval=JoyfulRules.TimeAtDistance(r.Distance)-JoyfulRules.TimeAtDistance(previous.Distance);Assert.That(interval,Is.GreaterThanOrEqualTo(1.549f));Assert.That(Math.Abs(r.SafeX-previous.SafeX)/9.5f,Is.LessThan(interval-.7f));}previous=r;}}}
        [Test]public void MovingObjectsKeepReservedCorridorAtContact()
        {for(int seed=1;seed<=128;seed++){var sequence=new JoyfulSequence(seed);for(int n=0;n<100;n++){var r=sequence.Next();for(float z=-2;z<=2;z+=.2f){if(r.Bus){float x=JoyfulRules.MotionX(r.SafeLane==0?1.7f:-1.7f,z,1.05f,.10f+r.Stage*.012f,r.Phase);Assert.That(Math.Abs(r.SafeX-x),Is.GreaterThan(.9f+.39f+.12f));}else if(r.Balls>0){for(int lane=0;lane<3;lane++)if(lane!=r.SafeLane){float x=JoyfulRules.MotionX(ArcadeRules.LaneX(lane),z,.8f+r.Stage*.16f,.055f+r.Stage*.008f,r.Phase);Assert.That(Math.Abs(r.SafeX-x),Is.GreaterThan(.48f+.39f+.12f));}}}}}}
        [Test]public void FlightStartsAtRampTipAndOnlyAirEarnedTpIsDoubled()
        {Assert.That(JoyfulRules.FlightHeight(0),Is.EqualTo(.65f));Assert.That(JoyfulRules.FlightHeight(.8f),Is.GreaterThan(1.8f));Assert.That(JoyfulRules.FlightHeight(1.65f),Is.Zero);var a=new CareerData();var b=new CareerData();int ordinary=CareerRules.Settle(a,"a",new RunSummary(150,1200,2,0,PlayMode.Arcade),Array.Empty<MissionDefinition>());int jump=CareerRules.Settle(b,"b",new RunSummary(150,1200,2,0,PlayMode.Arcade,30,200),Array.Empty<MissionDefinition>());Assert.That(jump-ordinary,Is.EqualTo(5));Assert.That(CareerRules.Settle(b,"bad",new RunSummary(1,0,0,0,PlayMode.Arcade,float.NaN),Array.Empty<MissionDefinition>()),Is.Zero);}
        [Test]public void VerticalReplayRoundTripsAndRejectsImpossibleHeight()
        {string path=System.IO.Path.Combine(UnityEngine.Application.temporaryCachePath,"jump-ghost-"+Guid.NewGuid().ToString("N")+".gz");try{short[] x={0,100,200,0},y={0,650,2000,0};GhostReplay.Write(path,7,0,123,x,4,y);GhostReplay.Read(path,7,0,out var distance,out var values,out var heights);Assert.That(distance,Is.EqualTo(123));Assert.That(values,Is.EqualTo(x));Assert.That(heights,Is.EqualTo(y));y[2]=3000;GhostReplay.Write(path,7,0,123,x,4,y);GhostReplay.Read(path,7,0,out _,out values,out _);Assert.That(values,Is.Null);}finally{foreach(string file in new[]{path,path+".pending"})if(System.IO.File.Exists(file))System.IO.File.Delete(file);}}
    }
}
