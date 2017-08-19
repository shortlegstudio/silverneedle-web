// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using Xunit;
    using SilverNeedle.Lexicon;
    
    public class ParagraphBuilderTests
    {
        [Fact]
        public void TakesAGroupOfSentencesToCreateAParagraph()
        {
            var paragraph = new ParagraphBuilder();
            paragraph.AddSentence("I am a good boy.");
            paragraph.AddSentence("Everybody loves me.");
            paragraph.AddSentence("Hold on to your seat.");


            Assert.Equal("I am a good boy. Everybody loves me. Hold on to your seat.", paragraph.GetParagraph());
        }
    }
}