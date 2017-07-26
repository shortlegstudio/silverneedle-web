// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using Xunit;
    using SilverNeedle;

    public class GatewayProviderTests
    {
        [Fact]
        public void LoadsGatewayForCharacterCreator()
        {
            var subject = new GatewayProvider();
            var creatorGateway = subject.GetImpl<SilverNeedle.Actions.CharacterGenerator.CharacterDesigner>();
            Assert.NotNull(creatorGateway);
            Assert.True(creatorGateway.Count() > 0);
        }
    }
}