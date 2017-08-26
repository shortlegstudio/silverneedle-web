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

    public class ChooseWordFromGatewayObjectTests
    {
        [Fact]
        public void RegistersHelperForSelectingGatewayObject()
        {
            var red = new Color();
            red.Name = "Red";
            var gateway = EntityGateway<Color>.LoadWithSingleItem(red);
            var helper = new ChooseWordFromGatewayObject<Color>(gateway);

            var handleBarsTemplate = Handlebars.Compile("{{choose-color}}");
            var result = handleBarsTemplate(string.Empty);
            Assert.Equal("Red", result);

        }
    }
}