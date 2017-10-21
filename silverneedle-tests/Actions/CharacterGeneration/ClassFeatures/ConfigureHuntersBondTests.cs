// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class ConfigureHuntersBondTests
    {
        [Fact]
        public void ChoosesAnItemFromPotentialHuntersBondsForAbility()
        {
            var options = new MemoryStore();
            options.SetValue("bonds", new string[] { "wolf", "cat", "tiger", "elephant", "giraffe" });
            var step = new ConfigureHuntersBond(options);
            var character = new CharacterSheet(CharacterStrategy.Default());
            step.ExecuteStep(character);

            var bond = character.Get<HuntersBond>();
            Assert.Contains(bond.Bond, options.GetList("bonds"));
        }
    }
}