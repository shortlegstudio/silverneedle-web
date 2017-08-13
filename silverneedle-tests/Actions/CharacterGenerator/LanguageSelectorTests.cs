using Xunit;
using SilverNeedle.Characters;
using SilverNeedle.Actions.CharacterGenerator;
using System.Linq;
using System.Collections.Generic;
using SilverNeedle;
using SilverNeedle.Serialization;


namespace Actions {

    
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
            var character = new CharacterSheet();
            var strategy = new CharacterBuildStrategy();
            strategy.LanguagesKnown.Add ("Elvish");
            strategy.LanguagesKnown.Add ("Giant");
            var subject = new LanguageSelector (languageGateway);
            subject.Process(character, strategy);
            Assert.NotStrictEqual(character.Languages.Select(x => x.Name), new string[] { "Elvish", "Giant"});
        }

        [Fact]
        public void PickExtraLanguagesIfSmartEnough() {
            var strategy = new CharacterBuildStrategy();
            strategy.LanguagesKnown.Add ("Elvish");
            strategy.LanguageChoices.Add ("Corgi");
            strategy.LanguageChoices.Add ("Giant");
            var subject = new LanguageSelector (languageGateway);

            //Pick two bonus Language -> This should always return all the above
            for (int i = 0; i < 1000; i++) {
                var character = new CharacterSheet();
                character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 14);
                subject.Process(character, strategy);
                var res = character.GetAll<Language>().Select(x => x.Name);
                Assert.NotStrictEqual(res, new string[] { "Elvish", "Giant", "Corgi"});
            }
        }

        [Fact]
        public void IfRunOutOfLanguagesItsOk() {
            var strategy = new CharacterBuildStrategy();
            strategy.LanguagesKnown.Add ("Elvish");
            strategy.LanguageChoices.Add ("Corgi");
            strategy.LanguageChoices.Add ("Giant");
            var subject = new LanguageSelector (languageGateway);

            //Pick two bonus Language -> This should always return all the above
            for (int i = 0; i < 1000; i++) {
                var character = new CharacterSheet();
                character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 24);
                subject.Process (character, strategy);
                var res = character.GetAll<Language>().Select(x => x.Name);
                Assert.NotStrictEqual(res, new string[] { "Elvish", "Giant", "Corgi" });
            }
        }

        [Fact]
        public void IfAvailableLanguagesIsSetToALLThenAnythingIsPossible()
        {
            var strategy = new CharacterBuildStrategy();
            strategy.LanguageChoices.Add ("ALL");
            var subject = new LanguageSelector (languageGateway);

            //Pick two bonus Language -> This should always return all the above
            for (int i = 0; i < 1000; i++) {
                var character = new CharacterSheet();
                character.AbilityScores.SetScore(AbilityScoreTypes.Intelligence, 24);
                subject.Process (character, strategy);
                var res = character.GetAll<Language>().Select(x => x.Name);
                Assert.NotStrictEqual(res, new string[] { "Elvish", "Giant", "Corgi", "Boo" });
            }
        }

        [Fact]
        public void DoNotRepeatedlyAddKnownLanguages()
        {
            var strategy = new CharacterBuildStrategy();
            strategy.LanguagesKnown.Add ("Corgi");
            strategy.LanguagesKnown.Add ("Corgi");
            strategy.LanguagesKnown.Add ("Elvish");
            var subject = new LanguageSelector (languageGateway);
            var character = new CharacterSheet();

            subject.Process(character, strategy);
            Assert.NotStrictEqual(character.GetAll<Language>().Select(x => x.Name), new string[] {"Corgi", "Elvish"});
        }
    }
}