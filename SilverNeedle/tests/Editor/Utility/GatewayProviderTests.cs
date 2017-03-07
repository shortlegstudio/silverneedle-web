// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
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
            var creatorGateway = subject.Get<SilverNeedle.Actions.CharacterGenerator.CharacterCreator>();
            Assert.IsNotNull(creatorGateway);
            Assert.Greater(creatorGateway.Count(), 0);
        }
    }
}