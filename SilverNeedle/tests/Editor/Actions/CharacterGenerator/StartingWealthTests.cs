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
    public class StartingWealthTests 
    {
        [Test]
        public void DoesNothingIfStartingWealthIsNullForClass()
        {
            var cls = new Class();
            var character = new CharacterSheet();
            character.SetClass(cls);
            var action = new StartingWealth();
            action.Process(character, new CharacterBuildStrategy());
        }

        [Test]
        public void CalculatesWealthBasedOnTheDiceInGoldPiecesTimesTen()
        {
            var cls = new Class();
            cls.StartingWealthDice = SilverNeedle.Dice.DiceStrings.ParseDice("2d6");
            var character = new CharacterSheet();
            character.SetClass(cls);

            var action = new StartingWealth();
            action.Process(character, new CharacterBuildStrategy());

            Assert.Greater(character.Inventory.CoinPurse.Gold.Pieces, 19);
            Assert.Less(character.Inventory.CoinPurse.Gold.Pieces, 121);
        }
    }
}