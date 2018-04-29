// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using Xunit;
using SilverNeedle.Characters;
using SilverNeedle.Actions.CharacterGeneration;
using System.Linq;
using System.Collections.Generic;
using SilverNeedle;
using SilverNeedle.Serialization;


namespace Tests.Actions.CharacterGeneration {

    
    public class LanguageSelectorTests {
        public EntityGateway<Language> languageGateway;
        public LanguageSelectorTests()
        {
            var languages = new List<Language> ();
            languages.Add (new Language ("Elvish", "Foo"));
            languages.Add (new Language ("Boo", "foo"));
            languages.Add (new Language ("Giant", "Rawr"));
            languages.Add (new Language ("Corgi", "woof"));
            languageGateway = EntityGateway<Language>.LoadFromList(languages);
        }
        [Fact]
        public void PickLanguagesThatAreKnownToTheRace() {
            var character = CharacterTestTemplates.AverageBob();
            character.Strategy.AddLanguageKnown("Elvish");
            character.Strategy.AddLanguageKnown ("Giant");
            var subject = new LanguageSelector (languageGateway);
            subject.ExecuteStep(character);
            Assert.NotStrictEqual(character.Languages.Select(x => x.Name), new string[] { "Elvish", "Giant"});
        }

        [Fact]
        public void PickExtraLanguagesIfSmartEnough() {
            var character = CharacterTestTemplates.AverageBob();
            character.Strategy.AddLanguageKnown ("Elvish");
            character.Strategy.AddLanguageChoice ("Corgi");
            character.Strategy.AddLanguageChoice ("Giant");
            var subject = new LanguageSelector (languageGateway);

            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14);
            subject.ExecuteStep(character);
            var res = character.GetAll<Language>().Select(x => x.Name);
            
            AssertExtensions.EquivalentLists(new string[] { "Elvish", "Giant", "Corgi"}, res);
        }

        [Fact]
        public void IfRunOutOfLanguagesItsOk() {
            var character = CharacterTestTemplates.AverageBob();
            character.Strategy.AddLanguageKnown ("Elvish");
            character.Strategy.AddLanguageChoice ("Corgi");
            character.Strategy.AddLanguageChoice ("Giant");
            var subject = new LanguageSelector (languageGateway);

            //Pick two bonus Language -> This should always return all the above
            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 24);
            subject.ExecuteStep (character);
            var res = character.GetAll<Language>().Select(x => x.Name);
            Assert.NotStrictEqual(res, new string[] { "Elvish", "Giant", "Corgi" });
        }

        [Fact]
        public void IfAvailableLanguagesIsSetToALLThenAnythingIsPossible()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.Strategy.AddLanguageChoice ("ALL");
            var subject = new LanguageSelector (languageGateway);

            character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 24);
            subject.ExecuteStep (character);
            var res = character.GetAll<Language>().Select(x => x.Name);
            Assert.NotStrictEqual(res, new string[] { "Elvish", "Giant", "Corgi", "Boo" });
        }

        [Fact]
        public void DoNotRepeatedlyAddKnownLanguages()
        {
            var character = CharacterTestTemplates.AverageBob();
            character.Strategy.AddLanguageKnown ("Corgi");
            character.Strategy.AddLanguageKnown ("Corgi");
            character.Strategy.AddLanguageKnown ("Elvish");
            var subject = new LanguageSelector (languageGateway);

            subject.ExecuteStep(character);
            Assert.NotStrictEqual(character.GetAll<Language>().Select(x => x.Name), new string[] {"Corgi", "Elvish"});
        }
    }
}