// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.Describe
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Actions.Describe;

    [TestFixture]
    public class DescriptionBuilderTests
    {
        [Test]
        public void BeAbleToUseCorrectPronoun()
        {
            var desc = new DescriptionBuilder();
            desc.FormatString = "{{pronoun}} has a long mustache.";
            var character = new CharacterSheet();
            character.Gender = Gender.Female;

            var result = desc.Describe(character);
            Assert.That(result, Is.EqualTo("She has a long mustache."));

            character.Gender = Gender.Male;
            result = desc.Describe(character);
            Assert.That(result, Is.EqualTo("He has a long mustache."));
        }
    }
}