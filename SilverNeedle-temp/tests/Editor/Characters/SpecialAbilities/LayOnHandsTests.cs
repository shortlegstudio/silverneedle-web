// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class LayOnHandsTests 
    {
        private CharacterSheet level4Paladin;
        [SetUp]
        public void Configure()
        {
            level4Paladin = new CharacterSheet();
            level4Paladin.AbilityScores.SetScore(AbilityScoreTypes.Charisma, 14); //+2 Modifier
            var paladin = new Class();
            paladin.Name = "Paladin";
            level4Paladin.SetClass(paladin);
            level4Paladin.SetLevel(4);
            var layOnHands = new LayOnHands();
            level4Paladin.Add(layOnHands);
        }

        [Test]
        public void LayOnHandsCanBeUsedANumberOfTimesBasedOnCharismaAndPaladinLevels()
        {
            // Charisma + 1/2 Paladin Level 
            Assert.That(level4Paladin.Get<LayOnHands>().UsesPerDay, Is.EqualTo(4));
        }

        [Test]
        public void LayOnHandsHealingIsBasedOnLevel()
        {
            Assert.That(level4Paladin.Get<LayOnHands>().HealingDice.ToString(), Is.EqualTo("2d6"));
        }

        [Test]
        public void NameReflectsUsesAndHealing()
        {
            Assert.That(level4Paladin.Get<LayOnHands>().Name, Is.EqualTo("Lay on Hands (2d6, 4/day)"));
        }

        [Test]
        public void CanBeSetToAlwaysReturnTheMaximumValue()
        {
            var layOn = level4Paladin.Get<LayOnHands>();
            layOn.MaximizeAmount = true;
            Assert.That(layOn.HealingDice.Roll(), Is.EqualTo(12));
            Assert.That(layOn.Name, Is.EqualTo("Lay on Hands (12 points, 4/day)"));
        }
    }
}