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
            var strategy = new CharacterStrategy();
            strategy.AddLanguageKnown("Elvish");
            strategy.AddLanguageKnown ("Giant");
            var character = new CharacterSheet(strategy);
            var subject = new LanguageSelector (languageGateway);
            subject.ExecuteStep(character, strategy);
            Assert.NotStrictEqual(character.Languages.Select(x => x.Name), new string[] { "Elvish", "Giant"});
        }

        [Fact]
        public void PickExtraLanguagesIfSmartEnough() {
            var strategy = new CharacterStrategy();
            strategy.AddLanguageKnown ("Elvish");
            strategy.AddLanguageChoice ("Corgi");
            strategy.AddLanguageChoice ("Giant");
            var subject = new LanguageSelector (languageGateway);

            //Pick two bonus Language -> This should always return all the above
            for (int i = 0; i < 1000; i++) {
                var character = new CharacterSheet(strategy);
                character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14);
                subject.ExecuteStep(character, strategy);
                var res = character.GetAll<Language>().Select(x => x.Name);
                
                AssertExtensions.EquivalentLists(new string[] { "Elvish", "Giant", "Corgi"}, res);
            }
        }

        [Fact]
        public void IfRunOutOfLanguagesItsOk() {
            var strategy = new CharacterStrategy();
            strategy.AddLanguageKnown ("Elvish");
            strategy.AddLanguageChoice ("Corgi");
            strategy.AddLanguageChoice ("Giant");
            var subject = new LanguageSelector (languageGateway);

            //Pick two bonus Language -> This should always return all the above
            for (int i = 0; i < 1000; i++) {
                var character = new CharacterSheet(strategy);
                character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 24);
                subject.ExecuteStep (character, strategy);
                var res = character.GetAll<Language>().Select(x => x.Name);
                Assert.NotStrictEqual(res, new string[] { "Elvish", "Giant", "Corgi" });
            }
        }

        [Fact]
        public void IfAvailableLanguagesIsSetToALLThenAnythingIsPossible()
        {
            var strategy = new CharacterStrategy();
            strategy.AddLanguageChoice ("ALL");
            var subject = new LanguageSelector (languageGateway);

            //Pick two bonus Language -> This should always return all the above
            for (int i = 0; i < 1000; i++) {
                var character = new CharacterSheet(strategy);
                character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 24);
                subject.ExecuteStep (character, strategy);
                var res = character.GetAll<Language>().Select(x => x.Name);
                Assert.NotStrictEqual(res, new string[] { "Elvish", "Giant", "Corgi", "Boo" });
            }
        }

        [Fact]
        public void DoNotRepeatedlyAddKnownLanguages()
        {
            var strategy = new CharacterStrategy();
            strategy.AddLanguageKnown ("Corgi");
            strategy.AddLanguageKnown ("Corgi");
            strategy.AddLanguageKnown ("Elvish");
            var subject = new LanguageSelector (languageGateway);
            var character = new CharacterSheet(strategy);

            subject.ExecuteStep(character, strategy);
            Assert.NotStrictEqual(character.GetAll<Language>().Select(x => x.Name), new string[] {"Corgi", "Elvish"});
        }
    }
}