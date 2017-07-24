// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class RageTests
    {
        [Test]
        public void RageConfiguresBoostsToStrengthAndConstitution()
        {
            var data = new MemoryStore();
            data.SetValue("strength", 4);
            data.SetValue("constitution", 4);
            data.SetValue("will", 2);
            data.SetValue("armor-class", -2);
            var rage = new Rage(data);
            Assert.That(rage.StrengthModifier.Modifier, Is.EqualTo(4));
            Assert.That(rage.ConstitutionModifier.Modifier, Is.EqualTo(4));
            Assert.That(rage.WillModifier.Modifier, Is.EqualTo(2));
            Assert.That(rage.ACModifier.Modifier, Is.EqualTo(-2));
        }

        [Test]
        public void RageUpdatesStatsOnCharacterSheet()
        {
            var data = new MemoryStore();
            data.SetValue("strength", 4);
            data.SetValue("constitution", 4);
            data.SetValue("will", 2);
            data.SetValue("armor-class", -2);
            var rage = new Rage(data);
            var character = new CharacterSheet();
            character.Add(rage);
            Assert.That(character.Defense.BaseArmorClass.TotalValue, Is.EqualTo(8));
            Assert.That(character.Defense.WillSave.TotalValue, Is.EqualTo(2));
            Assert.That(character.AbilityScores.GetScore(AbilityScoreTypes.Strength), Is.EqualTo(4));
            Assert.That(character.AbilityScores.GetScore(AbilityScoreTypes.Constitution), Is.EqualTo(4));
        }

        [Test]
        public void RageCanBeImprovedToHigherPower()
        {
            var data = new MemoryStore();
            data.SetValue("strength", 4);
            data.SetValue("constitution", 4);
            data.SetValue("will", 2);
            data.SetValue("armor-class", -2);
            var rage = new Rage(data);
            var improved = new MemoryStore();
            improved.SetValue("name", "Greater Rage");
            improved.SetValue("strength", 6);
            improved.SetValue("constitution", 6);
            improved.SetValue("will", 3);
            rage.Update(improved);
            Assert.That(rage.Name, Is.EqualTo("Greater Rage"));
            Assert.That(rage.StrengthModifier.Modifier, Is.EqualTo(6));
            Assert.That(rage.ConstitutionModifier.Modifier, Is.EqualTo(6));
            Assert.That(rage.WillModifier.Modifier, Is.EqualTo(3));
            Assert.That(rage.ACModifier.Modifier, Is.EqualTo(-2));
        }
    }

}