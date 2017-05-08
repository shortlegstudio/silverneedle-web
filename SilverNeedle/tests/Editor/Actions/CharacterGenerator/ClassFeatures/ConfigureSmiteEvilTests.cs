// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class ConfigureSmiteEvilTests
    {
        [Test]
        public void UpdatesSmiteEvilUsesPerDayIfAlreadyConfigured()
        {
            var character = new CharacterSheet();
            character.Add(new SmiteEvil(1));
            var configuration = new MemoryStore();
            configuration.SetValue("uses-per-day", 2);

            var process = new ConfigureSmiteEvil(configuration);
            process.Process(character, new CharacterBuildStrategy());
            var smite = character.Components.GetAll<SmiteEvil>();
            Assert.That(smite.Count(), Is.EqualTo(1));
            Assert.That(smite.First().UsesPerDay, Is.EqualTo(2));
        }
    }
}