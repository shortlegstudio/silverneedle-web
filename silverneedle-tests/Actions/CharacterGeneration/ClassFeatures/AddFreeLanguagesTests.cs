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
    using SilverNeedle.Serialization;

    public class AddFreeLanguagesTests
    {
        [Fact]
        public void AddsLanguagesToCharacterForFree()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var strategy = new CharacterBuildStrategy();
            var objectProps = new MemoryStore();
            objectProps.SetValue("languages", new string[] { "Druidic" , "Draconic" });
            var freeLanguages = new AddFreeLanguages(objectProps);
            freeLanguages.ExecuteStep(bob, strategy);
            Assert.Contains("Druidic", strategy.LanguagesKnown);
            Assert.Contains("Draconic", strategy.LanguagesKnown);
            Assert.Equal(2, strategy.LanguagesKnown.Count());
        }
    }
}