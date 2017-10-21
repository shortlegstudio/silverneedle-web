// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class ConfigureSmiteEvilTests
    {
        [Fact]
        public void UpdatesSmiteEvilUsesPerDayIfAlreadyConfigured()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.Add(new SmiteEvil(1));
            var configuration = new MemoryStore();
            configuration.SetValue("uses-per-day", 2);

            var process = new ConfigureSmiteEvil(configuration);
            process.ExecuteStep(character, new CharacterStrategy());
            var smite = character.Components.GetAll<SmiteEvil>();
            Assert.Equal(smite.Count(), 1);
            Assert.Equal(smite.First().UsesPerDay, 2);
        }
    }
}