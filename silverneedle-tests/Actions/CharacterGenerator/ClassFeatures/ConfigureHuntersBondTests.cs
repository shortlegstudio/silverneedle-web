// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class ConfigureHuntersBondTests
    {
        [Fact]
        public void ChoosesAnItemFromPotentialHuntersBondsForAbility()
        {
            var options = new MemoryStore();
            options.SetValue("bonds", "wolf, cat, tiger, elephant, giraffe");
            var step = new ConfigureHuntersBond(options);
            var character = new CharacterSheet();
            step.Process(character, new CharacterBuildStrategy());

            var bond = character.Get<HuntersBond>();
            Assert.Contains(bond.Bond, options.GetString("bonds"));
        }
    }
}