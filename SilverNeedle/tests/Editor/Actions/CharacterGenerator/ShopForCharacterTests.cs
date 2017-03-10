// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;

    [TestFixture]
    public class ShopForCharacterTests
    {
        [Test]
        public void IfStrategyIsNotFoundJustUseDefault()
        {
            var shopper = new ShopForCharacter();
            shopper.Process(new CharacterSheet(), new CharacterBuildStrategy());
        }
    }
}