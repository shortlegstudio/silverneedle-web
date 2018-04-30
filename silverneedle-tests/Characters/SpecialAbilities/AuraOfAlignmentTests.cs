// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities; 
    using Xunit;

    public class AuraOfAlignmentTests : RequiresDataFiles
    {
        [Fact]
        public void CalculatesTheAlignmentAndStrengthBasedOnCharacterLevelAndAlignment()
        {
            var cleric = CharacterTestTemplates.Cleric();
            cleric.Alignment = CharacterAlignment.ChaoticGood;
            var aura = new AuraOfAlignment();
            cleric.Add(aura);
            Assert.Equal("Faint", aura.Strength);
            cleric.SetLevel(2);
            Assert.Equal("Moderate", aura.Strength);
            cleric.SetLevel(5);
            Assert.Equal("Strong", aura.Strength);
            cleric.SetLevel(15);
            Assert.Equal("Overwhelming", aura.Strength);
            Assert.Equal("Aura (Overwhelming Chaotic Good)", aura.DisplayString());
        }
    }
}