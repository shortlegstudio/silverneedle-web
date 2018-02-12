// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class CasterLevelModifierTests : RequiresDataFiles
    {
        [Fact]
        public void CalculatesTheModifierBasedOnTheNumberOfLevelsDividedByTheRate()
        {
            string yaml = @"---
name: Extra Damage
modifier-type: bonus
rate: 2
caster-type: arcane";
            var modifier = new CasterLevelModifier(yaml.ParseYaml());
            var character = CharacterTestTemplates.Wizard().WithWizardCasting();
            character.Add(modifier);
            Assert.Equal(0, modifier.Modifier);
            character.SetLevel(6);
            Assert.Equal(3, modifier.Modifier);
            Assert.Equal(SilverNeedle.Spells.SpellType.Arcane, modifier.CasterType);
        }
    }
}