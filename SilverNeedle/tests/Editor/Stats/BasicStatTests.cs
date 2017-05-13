// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Stats
{

    using NUnit.Framework;
    using System.Linq;
    using SilverNeedle;

    [TestFixture]
    public class BasicStatTests {
        [Test]
        public void StatsTotalUpAdjustments() 
        {
            var stat = new BasicStat ("TestStat", 10);
            stat.AddModifier (new ValueStatModifier (5, "Foo"));
            Assert.AreEqual (15, stat.TotalValue);
        }

        [Test]
        public void StatModifiersCanHaveConditionalModifiers() 
        {
            var stat = new BasicStat("TestStat", 10);
            var mod = new ConditionalStatModifier(new ValueStatModifier("Skill", 5, "bonus", "Feat"),"vs. Giants");
            stat.AddModifier(mod);
            Assert.AreEqual(10, stat.TotalValue);
            Assert.AreEqual(15, stat.GetConditionalValue("vs. Giants"));
            Assert.AreEqual(1, stat.GetConditions().Count());
            Assert.AreEqual("vs. Giants", stat.GetConditions().First());
        }

        [Test]
        public void StatsCanHaveAListOfConditionalModifiers() 
        {
            var stat = new BasicStat("TestStat", 10);
            stat.AddModifier(
                new ConditionalStatModifier(new ValueStatModifier("Skill", 3, "bonus", "Trait"), "vs. Corgis") 
            );
            stat.AddModifier(
                new ConditionalStatModifier(new ValueStatModifier("Skill", 3, "bonus", "Trait"), "vs. Corgis") 
            );
            stat.AddModifier(
                new ConditionalStatModifier(new ValueStatModifier("Skill", 3, "bonus", "Trait"), "Trapfinding") 
            );

            Assert.AreEqual(2, stat.GetConditions().Count());
            Assert.IsTrue(stat.GetConditions().Any(x => x == "vs. Corgis"));
            Assert.IsTrue(stat.GetConditions().Any(x => x == "Trapfinding"));
        }

        [Test]
        public void CastingDoesntBreakConditionalModifiers() 
        {
            var stat = new BasicStat("TestStat", 10);
            IStatModifier mod = new ConditionalStatModifier(new ValueStatModifier("Attack Bonus", 3, "bonus", "Food"), "vs. Thor");
            stat.AddModifier(mod);
            Assert.AreEqual(1, stat.GetConditions().Count());
            Assert.AreEqual(10, stat.TotalValue);
            Assert.AreEqual(13, stat.GetConditionalValue("vs. Thor"));
        }
      
        [Test]
        public void FormatNiceStringVersionOfStat() 
        {
            var stat = new BasicStat("TestStat", 20);
            IStatModifier mod = new ConditionalStatModifier(new ValueStatModifier("Attack Bonus", 3, "bonus", "Food"), "vs. Thor");
            stat.AddModifier(mod);
            Assert.AreEqual("Fight +20 (+23 vs. Thor)", stat.ToString("Fight"));
        }

        [Test]
        public void AlwaysRoundDownStats() 
        {
            var stat = new BasicStat("TestStat", 2);
            stat.AddModifier(new ValueStatModifier(-1, "Food"));
            stat.AddModifier(new ValueStatModifier(0.667f, "Because"));
            Assert.AreEqual(-1, stat.SumBasicModifiers());
            Assert.AreEqual(1, stat.TotalValue);
        }

        [Test]
        public void StatsCanHaveMaximumValuesThatClampTheResult()
        {
            var stat = new BasicStat("Stat 1");
            stat.Maximum = 29;
            var mod = new ValueStatModifier(30, "Because");
            stat.AddModifier(mod);
            Assert.That(stat.TotalValue, Is.EqualTo(29));
        }

        [Test]
        public void StatsCanHaveAMinimumValueThatClampTheResult() 
        {
            var stat = new BasicStat("Stat!");
            stat.Minimum = 3;
            var mod = new ValueStatModifier(-20, "Loser");
            stat.AddModifier(mod);
            Assert.That(stat.TotalValue, Is.EqualTo(3));
        }

        [Test]
        public void CanAddMultipleModifiersAtOnce()
        {
            var stat = new BasicStat("Statistic");
            stat.AddModifiers(
                new ValueStatModifier(1, "Bonus"),
                new ValueStatModifier(3, "Foo"),
                new ValueStatModifier(-2, "Penalty")
            );
            Assert.That(stat.Modifiers.Count(), Is.EqualTo(3));
        }
    }
}