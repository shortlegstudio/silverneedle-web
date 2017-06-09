// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class AddBonusLanguagesTests 
    {
        [Test]
        public void AddSomeLanguages()
        {
            var data = new MemoryStore();
            data.SetValue("languages", "elvish, draconic");
            var build = new AddBonusLanguages(data);
            var character = new CharacterSheet();
            var strategy = new CharacterBuildStrategy();
            build.Process(character, strategy);
            Assert.That(strategy.LanguageChoices, Is.EquivalentTo(new string[] { "elvish", "draconic"}));
        }
    }
}