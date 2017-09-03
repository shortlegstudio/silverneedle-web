// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Lexicon
{
    using Xunit;
    using HandlebarsDotNet;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class ChooseDescriptorTests
    {
        [Fact]
        public void ChoosingADescriptorLoadsFromGatewayDescriptorsMatchingName()
        {
            var color = new Descriptor("color", new string[] { "Red" });
            var gateway = EntityGateway<Descriptor>.LoadWithSingleItem(color);
            var choose = ChooseDescriptor.CreateIsolatedForUnitTesting(gateway, "choose-testing");
            var handleBarsTemplate = Handlebars.Compile("{{choose-testing \"color\"}}");
            var result = handleBarsTemplate(string.Empty);
            Assert.Equal("Red", result);
        }

        [Fact]
        public void ChoosingAWordAddsToContextIfPossible()
        {
            var color = new Descriptor("color", new string[] { "Red" });
            var gateway = EntityGateway<Descriptor>.LoadWithSingleItem(color);
            var choose = ChooseDescriptor.CreateIsolatedForUnitTesting(gateway, "choose-testing");
            var handleBarsTemplate = Handlebars.Compile("{{choose-testing \"color\"}}");
            var context = new PhraseContext();
            dynamic contextObj = context.CreateObject();
            var result = handleBarsTemplate(contextObj);
            Assert.Equal("Red", context.GetValue<string>("color"));
            Assert.Equal("Red", contextObj.color);
        }
    }
}