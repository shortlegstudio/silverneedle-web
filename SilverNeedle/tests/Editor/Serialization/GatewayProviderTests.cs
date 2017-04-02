// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using NUnit.Framework;
    using SilverNeedle;

    [TestFixture]
    public class GatewayProviderTests
    {
        [Test]
        public void LoadsGatewayForCharacterCreator()
        {
            var subject = new GatewayProvider();
            var creatorGateway = subject.GetImpl<SilverNeedle.Actions.CharacterGenerator.CharacterDesigner>();
            Assert.IsNotNull(creatorGateway);
            Assert.Greater(creatorGateway.Count(), 0);
        }
    }
}