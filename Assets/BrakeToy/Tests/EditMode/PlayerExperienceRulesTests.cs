using System.Collections.Generic;
using BrakeToy.Core;
using NUnit.Framework;

namespace BrakeToy.Tests
{
    public sealed class PlayerExperienceRulesTests
    {
        [TestCase(0f,0,120f,400)]
        [TestCase(50f,200,120f,400)]
        [TestCase(200f,900,120f,400)]
        [TestCase(90000f,900000,90000f,900000)]
        public void AirBreakdownEqualsActualAdditionalSettlement(float air,int airStyle,float distance,int style)
        {
            var ground=new CareerData{achievements=(1<<15)-1};
            var jumping=new CareerData{achievements=(1<<15)-1};
            var missions=System.Array.Empty<MissionDefinition>();
            int a=CareerRules.Settle(ground,"ground",new RunSummary(distance,style,0,0,PlayMode.Arcade),missions);
            int b=CareerRules.Settle(jumping,"air",new RunSummary(distance,style,0,0,PlayMode.Arcade,air,airStyle),missions);
            Assert.That(b-a,Is.EqualTo(RunRewardBreakdown.AirBonus(air,airStyle,distance,style)));
        }
        [Test]
        public void SeededGrammarRemembersThreePatternsAndVariesLengthAndRampRole()
        {
            for(int seed=1;seed<=100;seed++)
            {
                var sequence=new AdventureSequence(seed,false,true);
                var recent=new Queue<int>();var lengths=new HashSet<float>();var roles=new HashSet<int>();
                for(int n=0;n<100;n++)
                {
                    var beat=sequence.Next();int pattern=TabletopCourseRules.Pattern(beat);
                    Assert.That(recent.Contains(pattern),Is.False,"seed="+seed+" beat="+n);
                    recent.Enqueue(pattern);if(recent.Count>3)recent.Dequeue();
                    lengths.Add(TabletopCourseRules.Spacing(beat,false));roles.Add(TabletopCourseRules.RampRole(beat));
                    Assert.That(TabletopCourseRules.ExitDistance(beat)-beat.Distance,
                        Is.LessThan(TabletopCourseRules.Spacing(beat,false)));
                }
                Assert.That(lengths.Count,Is.EqualTo(3));Assert.That(roles.Count,Is.EqualTo(3));
            }
        }
    }
}
