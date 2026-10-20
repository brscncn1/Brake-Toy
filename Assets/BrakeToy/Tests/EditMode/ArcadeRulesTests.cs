using System;
using BrakeToy.Core;
using NUnit.Framework;

namespace BrakeToy.Tests
{
    public sealed class ArcadeRulesTests
    {
        [Test]
        public void PaceStartsImmediatelyAndHasSmoothBoundedAcceleration()
        {
            Assert.That(ArcadeRules.Speed(0),Is.EqualTo(10));
            Assert.That(ArcadeRules.Speed(10),Is.GreaterThan(13));
            Assert.That(ArcadeRules.Speed(30),Is.GreaterThan(16));
            float previous=10;
            for(int i=1;i<10000;i++){float value=ArcadeRules.Speed(i*.1f);Assert.That(value,Is.InRange(previous,18));previous=value;}
            Assert.That(ArcadeRules.Distance(2.5f),Is.GreaterThan(24),"First encounter must arrive within 2.5 s.");
        }
        [Test]
        public void IntegratedDistanceAgreesAt30And120FramesPerSecond()
        {
            foreach(int fps in new[]{30,60,120})
            {
                double sum=0;for(int i=0;i<fps*60;i++)sum+=ArcadeRules.Speed((i+.5f)/fps)/fps;
                Assert.That(sum,Is.EqualTo(ArcadeRules.Distance(60)).Within(.01));
            }
        }
        [Test]
        public void SeedReproducesEncountersAndDoesNotDependOnFrameRate()
        {
            var a=new ArcadeSequence(7331);var b=new ArcadeSequence(7331);var c=new ArcadeSequence(7332);int differences=0;
            for(int i=0;i<1000;i++)
            {
                var x=a.Next();var y=b.Next();var z=c.Next();
                Assert.That(x.SafeLane,Is.EqualTo(y.SafeLane));Assert.That(x.BlockedMask,Is.EqualTo(y.BlockedMask));Assert.That(x.Distance,Is.EqualTo(y.Distance));
                if(x.SafeLane!=z.SafeLane||x.Variant!=z.Variant)differences++;
            }
            Assert.That(differences,Is.GreaterThan(100));
        }
        [Test]
        public void OneHundredTwentyEightThousandRowsHaveContinuousSafeWitnessPathsIncludingBalls()
        {
            for(int seed=1;seed<=1000;seed++)
            {
                var sequence=new ArcadeSequence(seed);float previousX=0,previousDistance=0;int previousVariant=-1;
                for(int i=0;i<128;i++)
                {
                    var row=sequence.Next();float x=ArcadeRules.LaneX(row.SafeLane),gap=row.Distance-previousDistance;
                    Assert.That(row.IsBlocked(row.SafeLane),Is.False,"seed="+seed+" row="+i);
                    Assert.That(row.Variant,Is.Not.EqualTo(previousVariant));
                    Assert.That(Math.Abs(x-previousX),Is.LessThanOrEqualTo(2.25f));
                    // The reserved lane has enough lateral transition time after the previous row clears.
                    Assert.That(gap/18f - 4f/18f,Is.GreaterThan(Math.Abs(x-previousX)/9.5f+.25f));
                    for(float z=2;z>=-2;z-=.2f)
                    {
                        if(row.Ball)Assert.That(Math.Abs(x-row.BallX(z)),Is.GreaterThan(.39f+.51f+.1f));
                        else for(int lane=0;lane<3;lane++)if(row.IsBlocked(lane))Assert.That(Math.Abs(x-ArcadeRules.LaneX(lane)),Is.GreaterThan(.39f+.82f+.1f));
                    }
                    Assert.That(95f/18,Is.GreaterThan(ArcadeRules.ReactionSeconds));
                    previousX=x;previousDistance=row.Distance;previousVariant=row.Variant;
                }
            }
        }
        [Test]
        public void NearMissChainExpiresAndCrashPreservesEarnedScoreOnly()
        {
            var s=new StyleScore();Assert.That(s.Award(),Is.EqualTo(100));s.Award();Assert.That(s.Award(),Is.EqualTo(200));
            for(int i=0;i<5;i++)s.Award();Assert.That(s.Fever,Is.True);Assert.That(s.Multiplier,Is.EqualTo(5));
            int earned=s.Total;s.Advance(6.01f);Assert.That(s.Chain,Is.Zero);Assert.That(s.Total,Is.EqualTo(earned));
            s.Award();s.Crash();Assert.That(s.Chain,Is.Zero);Assert.That(s.Total,Is.EqualTo(earned+100));
            s.Reset();Assert.That(s.Total,Is.Zero);Assert.That(s.NearMisses,Is.Zero);
        }
        [Test]
        public void RollingSphereDoesNotUseAnUnfairSquareCorner()
        {
            Assert.That(SweptCircleBox.TryHit(.8f,1.1f,.8f,1.1f,.39f,.7f,.51f,out _),Is.False);
            Assert.That(SweptCircleBox.TryHit(0,20,0,-20,.39f,.7f,.51f,out float t),Is.True);Assert.That(t,Is.InRange(.46f,.49f));
            Assert.That(SweptCircleBox.TryHit(-10,0,10,0,.39f,.7f,.51f,out _),Is.True);
        }
        [Test]
        public void SweptSphereMatchesFineSampleOracleAcrossTenThousandMovingCases()
        {
            var random=new Random(741);int hits=0;
            for(int i=0;i<10000;i++)
            {
                float x0=(float)random.NextDouble()*10-5,z0=(float)random.NextDouble()*10-5;
                float x1=(float)random.NextDouble()*10-5,z1=(float)random.NextDouble()*10-5;
                bool swept=SweptCircleBox.TryHit(x0,z0,x1,z1,.39f,.7f,.51f,out float hit);
                bool sampled=false;
                for(int j=0;j<=500;j++)
                {
                    float t=j/500f,x=x0+(x1-x0)*t,z=z0+(z1-z0)*t;
                    float dx=Math.Max(0,Math.Abs(x)-.39f),dz=Math.Max(0,Math.Abs(z)-.7f);
                    if(dx*dx+dz*dz<=.51f*.51f){sampled=true;break;}
                }
                Assert.That(!sampled||swept,Is.True,"Missed contact case="+i);
                if(swept)
                {
                    hits++;float x=x0+(x1-x0)*hit,z=z0+(z1-z0)*hit;
                    float dx=Math.Max(0,Math.Abs(x)-.39f),dz=Math.Max(0,Math.Abs(z)-.7f);
                    Assert.That(dx*dx+dz*dz,Is.LessThanOrEqualTo(.51f*.51f+.0001f));
                }
            }
            Assert.That(hits,Is.GreaterThan(1000));
        }
        [Test]
        public void VariedRowsKeepSafePathsWithoutLongIdenticalLaneRuns()
        {
            var gaps=new System.Collections.Generic.HashSet<float>();var ballIntervals=new System.Collections.Generic.HashSet<int>();
            for(int seed=1;seed<=1000;seed++)
            {
                var sequence=new ArcadeSequence(seed,true);var replay=new ArcadeSequence(seed,true);int previous=1,repeats=0,lastBall=-1;float previousDistance=0;
                for(int i=0;i<128;i++)
                {
                    var row=sequence.Next();var same=replay.Next();Assert.That(row.Distance,Is.EqualTo(same.Distance));Assert.That(row.BallSlope,Is.EqualTo(same.BallSlope));Assert.That(row.SafeLane,Is.EqualTo(same.SafeLane));
                    Assert.That(row.IsBlocked(row.SafeLane),Is.False);Assert.That(Math.Abs(row.SafeLane-previous),Is.LessThanOrEqualTo(1));
                    repeats=row.SafeLane==previous?repeats+1:1;Assert.That(repeats,Is.LessThanOrEqualTo(2));
                    float gap=row.Distance-previousDistance;if(i>0){gaps.Add(gap);Assert.That(gap,Is.InRange(25,34));}
                    if(row.Ball)
                    {
                        if(lastBall>=0)ballIntervals.Add(i-lastBall);lastBall=i;
                        for(float z=-2;z<=2;z+=.2f)Assert.That(Math.Abs(ArcadeRules.LaneX(row.SafeLane)-row.BallX(z)),Is.GreaterThan(1));
                    }
                    previous=row.SafeLane;previousDistance=row.Distance;
                }
            }
            Assert.That(gaps.Count,Is.GreaterThanOrEqualTo(8));Assert.That(ballIntervals.Count,Is.EqualTo(4));
        }
        [Test]
        public void LayeredAudioStaysFiniteAndWithinHeadroom()
        {
            for(int kind=0;kind<6;kind++)
            {
                var clip=BrakeToy.ArcadeAudio.LayeredClip("Audio audit",1,kind);var samples=new float[clip.samples];clip.GetData(samples,0);double energy=0;
                foreach(float sample in samples){Assert.That(float.IsNaN(sample)||float.IsInfinity(sample),Is.False);Assert.That(Math.Abs(sample),Is.LessThan(.91f));energy+=sample*sample;}
                Assert.That(Math.Sqrt(energy/samples.Length),Is.InRange(.01,.5));Assert.That(samples[0],Is.Zero);Assert.That(samples[samples.Length-1],Is.Zero);
                UnityEngine.Object.DestroyImmediate(clip);
            }
        }
    }
}
