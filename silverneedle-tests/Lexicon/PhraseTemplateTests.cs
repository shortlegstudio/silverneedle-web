// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using Xunit;
    using SilverNeedle.Lexicon;

    public class PhraseTemplateTests
    {
        [Fact]
        public void PhrasesCanCompileTemplates()
        {
            var template = "Hello {{name}}.";
            var phrase = new PhraseTemplate(template);
            var context = new { name = "Bob" };
            var result = phrase.WritePhrase(context);
            Assert.Equal("Hello Bob.", result);

        }

        [Fact]
        public void PhrasesCanCascadeCallsToOtherTemplatesIfNecessary()
        {
            var template = "Good Day, {{name}}.";
            var phrase = new PhraseTemplate(template);
            var context = new { name = "{{fullname}}", fullname = "Bob Woodward" };
            var result = phrase.WritePhrase(context);
            Assert.Equal("Good Day, Bob Woodward.", result);
        }
    }
}