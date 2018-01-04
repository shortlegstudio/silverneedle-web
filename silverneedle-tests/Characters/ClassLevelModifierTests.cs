// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ClassLevelModifierTests
    {
        [Fact]
        public void CalculatesTheModifierBasedOnTheNumberOfLevelsDividedByTheRate()
        {
            string yaml = @"---
name: strength
modifier-type: bonus
rate: 2
class: wizard";
            var modifier = new ClassLevelModifier(yaml.ParseYaml());
            var character = CharacterTestTemplates.Wizard();
            character.Add(modifier);
            Assert.Equal(0, modifier.Modifier);
            character.SetLevel(6);
            Assert.Equal(3, modifier.Modifier);
        }

        [Fact]
        public void AMinimumValueCanBeProvidedForFiguringOutTheCalculation()
        {
            string yaml = @"---
name: strength
modifier-type: bonus
rate: 2
minimum: 1
class: wizard";
            var modifier = new ClassLevelModifier(yaml.ParseYaml());
            var character = CharacterTestTemplates.Wizard();
            character.Add(modifier);
            Assert.Equal(1, modifier.Modifier);
        }
    }
}