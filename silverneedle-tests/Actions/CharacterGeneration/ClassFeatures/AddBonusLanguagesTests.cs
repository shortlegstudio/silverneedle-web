// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    
    public class AddBonusLanguagesTests 
    {
        [Fact]
        public void AddSomeLanguages()
        {
            var data = new MemoryStore();
            data.SetValue("languages", "elvish, draconic");
            var build = new AddBonusLanguages(data);
            var strategy = new CharacterStrategy();
            var character = new CharacterSheet(strategy);
            build.ExecuteStep(character);
            Assert.NotStrictEqual(strategy.LanguageChoices, new string[] { "elvish", "draconic"});
        }
    }
}