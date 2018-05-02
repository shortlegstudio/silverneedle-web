// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class LimitStatModifierTests : RequiresDataFiles
    {
        [Fact]
        public void FindsTheModifiersAndUsesTheLowestOne()
        {
            var character = CharacterTestTemplates.AverageBob();
            var yaml = @"
name: Armor Class
min-value: dexterity-modifier
max-value: strength-modifier
modifier-type: ss-tt";
            var limitMod = new LimitStatModifier(yaml.ParseYaml());
            character.Add(limitMod);
            Assert.Equal(0, limitMod.Modifier);
            character.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            Assert.Equal(0, limitMod.Modifier);
            character.AbilityScores.SetScore(AbilityScoreTypes.Dexterity, 18);
            Assert.Equal(3, limitMod.Modifier);
        }
    }
}