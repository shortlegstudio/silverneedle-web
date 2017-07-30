// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Describe
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Actions.Describe;

    
    public class DescriptionBuilderTests
    {
        [Fact]
        public void BeAbleToUseCorrectPronoun()
        {
            var desc = new DescriptionBuilder();
            desc.FormatString = "{{pronoun}} has a long mustache.";
            var character = new CharacterSheet();
            character.InitializeComponents();
            character.Gender = Gender.Female;
            character.Add(new Quirks());

            var result = desc.Describe(character);
            Assert.Equal(result, "She has a long mustache.");

            character.Gender = Gender.Male;
            result = desc.Describe(character);
            Assert.Equal(result, "He has a long mustache.");
        }
    }
}