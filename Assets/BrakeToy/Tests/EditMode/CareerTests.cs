using System;
using System.IO;
using BrakeToy.Core;
using NUnit.Framework;
using UnityEngine;
using PlayMode=BrakeToy.Core.PlayMode;
namespace BrakeToy.Tests
{
    public sealed class CareerTests
    {
        [Test]public void RewardsAreIdempotentAcrossRecentRunsAndDoNotSpendRecords()
        {var d=new CareerData();var missions=CareerRules.ActiveMissions(d);var run=new RunSummary(350,1200,6,4,PlayMode.Arcade);int earned=CareerRules.Settle(d,"a",run,missions);Assert.That(earned,Is.GreaterThan(0));CareerRules.Settle(d,"b",run,missions);int points=d.points;Assert.That(CareerRules.Settle(d,"a",run,missions),Is.Zero);Assert.That(d.points,Is.EqualTo(points));float record=d.arcadeDistance;d.points=200;Assert.That(CareerRules.Buy(d,1),Is.True);Assert.That(d.points,Is.Zero);Assert.That(d.arcadeDistance,Is.EqualTo(record));Assert.That(CareerRules.Buy(d,1),Is.True);Assert.That(CareerRules.Buy(d,9),Is.False);}
        [Test]public void RelaxNeverChangesArcadeOrDailyRecords()
        {var d=new CareerData{arcadeDistance=10,arcadeStyle=20,dailyDistance=30,dailyStyle=40};CareerRules.Settle(d,"relax",new RunSummary(1000,5000,10,10,PlayMode.Relax),Array.Empty<MissionDefinition>());Assert.That(d.arcadeDistance,Is.EqualTo(10));Assert.That(d.arcadeStyle,Is.EqualTo(20));Assert.That(d.dailyDistance,Is.EqualTo(30));Assert.That(d.relaxDistance,Is.EqualTo(1000));}
        [Test]public void UnlockAndCosmeticTransactionsCannotOverspend()
        {var d=new CareerData{points=100};Assert.That(CareerRules.BuyCosmetic(d,true,1),Is.True);Assert.That(d.points,Is.Zero);Assert.That(CareerRules.BuyCosmetic(d,true,1),Is.True);Assert.That(CareerRules.BuyCosmetic(d,false,2),Is.False);Assert.That(CareerRules.Buy(d,-1),Is.False);Assert.That(CareerRules.Buy(d,10),Is.False);Assert.That(d.unlocked,Is.EqualTo(1));}
        [Test]public void SlowerRelaxDistanceIsFrameIndependentAndBounded()
        {for(int i=0;i<=600;i++){Assert.That(CareerRules.Speed(i,PlayMode.Relax),Is.InRange(7f,10f));Assert.That(CareerRules.Distance(i,PlayMode.Relax),Is.LessThanOrEqualTo(ArcadeRules.Distance(i)));}foreach(int fps in new[]{30,60,120}){float sum=0;for(int j=0;j<fps*30;j++)sum+=CareerRules.Distance((j+1)/(float)fps,PlayMode.Relax)-CareerRules.Distance(j/(float)fps,PlayMode.Relax);Assert.That(sum,Is.EqualTo(CareerRules.Distance(30,PlayMode.Relax)).Within(.02f));}}
        [Test]public void SaveRecoversBackupAndRejectsCorruptOrFutureSchemas()
        {string directory=Path.Combine(Application.temporaryCachePath,"career-test-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(directory);string path=Path.Combine(directory,"save.json");try{var s=new CareerSave(path);var d=new CareerData{points=123};Assert.That(s.Save(d),Is.True);d.points=456;Assert.That(s.Save(d),Is.True);Assert.That(s.Load().points,Is.EqualTo(456));File.WriteAllText(path,"broken");Assert.That(s.Load().points,Is.EqualTo(123));Assert.That(s.Recovered,Is.True);File.WriteAllText(path+".backup","future");Assert.That(s.Load().points,Is.Zero);}finally{foreach(string f in Directory.GetFiles(directory))File.Delete(f);Directory.Delete(directory);}}
        [Test]public void ReplayIsCompressedAndRejectsWrongSeedModeAndExcessiveCoordinates()
        {string path=Path.Combine(Application.temporaryCachePath,"ghost-test-"+Guid.NewGuid().ToString("N")+".gz");try{var data=new short[]{0,12,250,-300};GhostReplay.Write(path,42,0,123,data,data.Length);GhostReplay.Read(path,42,0,out var best,out var values);Assert.That(best,Is.EqualTo(123));Assert.That(values,Is.EqualTo(data));GhostReplay.Read(path,43,0,out _,out values);Assert.That(values,Is.Null);GhostReplay.Read(path,42,1,out _,out values);Assert.That(values,Is.Null);GhostReplay.Write(path,42,0,123,new short[]{0,32000},2);GhostReplay.Read(path,42,0,out _,out values);Assert.That(values,Is.Null);}finally{if(File.Exists(path))File.Delete(path);if(File.Exists(path+".pending"))File.Delete(path+".pending");}}
        [Test]public void InvalidSaveFieldsAreRejectedAndRecentCursorWraps()
        {Assert.That(CareerRules.IsValid(new CareerData{recentRuns=null}),Is.False);Assert.That(CareerRules.IsValid(new CareerData{recentCursor=32}),Is.False);Assert.That(CareerRules.IsValid(new CareerData{trail=2}),Is.False);Assert.That(CareerRules.IsValid(new CareerData{dailyDistance=float.NaN}),Is.False);Assert.That(CareerRules.IsValid(new CareerData{ownedHorns=8}),Is.False);var d=new CareerData();for(int i=0;i<100;i++)CareerRules.Settle(d,i.ToString(),new RunSummary(1,0,0,0,PlayMode.Arcade),Array.Empty<MissionDefinition>());Assert.That(d.recentCursor,Is.EqualTo(4));Assert.That(CareerRules.IsValid(d),Is.True);}
        [Test]public void ThreeMissionsRotateAndFifteenAchievementsHaveStableIds()
        {var a=CareerRules.ActiveMissions(new CareerData());var b=CareerRules.ActiveMissions(new CareerData{runs=2});Assert.That(a.Length,Is.EqualTo(3));Assert.That(a[0].Title,Is.Not.EqualTo(b[0].Title));Assert.That(CareerRules.Achievements.Length,Is.EqualTo(15));Assert.That(CareerRules.IsValid(new CareerData{points=-1}),Is.False);Assert.That(CareerRules.IsValid(new CareerData{selectedCar=5}),Is.False);Assert.That(CareerRules.IsValid(new CareerData{version=2}),Is.False);}
    }
}
