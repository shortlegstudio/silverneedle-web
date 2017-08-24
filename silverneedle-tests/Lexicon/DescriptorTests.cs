// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class DescriptorTests : RequiresDataFiles
    {
        [Fact]
        public void CanLoadTheDictionaryDataFiles()
        {
            var gateway = GatewayProvider.Get<Descriptor>();
            Assert.NotEqual(0, gateway.Count());
        }

        [Fact]
        public void CanFindAndChooseAWord()
        {
            var word = Descriptor.FindAndChooseWord("animal");
            Assert.NotEmpty(word);
        }
    }
}