// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class RageTests
    {
        [Fact]
        public void RageConfiguresBoostsToStrengthAndConstitution()
        {
            var data = new MemoryStore();
            data.SetValue("strength", 4);
            data.SetValue("constitution", 4);
            data.SetValue("will", 2);
            data.SetValue("armor-class", -2);
            var rage = new Rage(data);
            Assert.Equal(rage.StrengthModifier.Modifier, 4);
            Assert.Equal(rage.ConstitutionModifier.Modifier, 4);
            Assert.Equal(rage.WillModifier.Modifier, 2);
            Assert.Equal(rage.ACModifier.Modifier, -2);
        }

        [Fact]
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
            Assert.Equal(character.Defense.BaseArmorClass.TotalValue, 8);
            Assert.Equal(character.Defense.WillSave.TotalValue, 2);
            Assert.Equal(character.AbilityScores.GetScore(AbilityScoreTypes.Strength), 4);
            Assert.Equal(character.AbilityScores.GetScore(AbilityScoreTypes.Constitution), 4);
        }

        [Fact]
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
            Assert.Equal(rage.Name, "Greater Rage");
            Assert.Equal(rage.StrengthModifier.Modifier, 6);
            Assert.Equal(rage.ConstitutionModifier.Modifier, 6);
            Assert.Equal(rage.WillModifier.Modifier, 3);
            Assert.Equal(rage.ACModifier.Modifier, -2);
        }
    }

}