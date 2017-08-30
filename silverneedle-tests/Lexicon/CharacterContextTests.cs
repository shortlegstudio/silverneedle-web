// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Lexicon;

    public class CharacterContextTests
    {
        [Fact]
        public void SupportsPronounsForCharacter()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var context = new CharacterContext(bob);
            Assert.Equal("he", context.GetValue<string>("pronoun"));
            Assert.Equal("his", context.GetValue<string>("possessivepronoun"));
        }

        [Fact]
        public void SupportsParentNames()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var context = new CharacterContext(bob);
            Assert.Equal("Bob's Father", context.GetValue<string>("character-father-name"));
            Assert.Equal("Bob's Mother", context.GetValue<string>("character-mother-name"));
        }
    }
}