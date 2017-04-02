// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    
    [TestFixture]
    public class StartingWealthTests 
    {
        EntityGateway<CharacterWealth> wealthGateway;

        [SetUp]
        public void SetUp()
        {
            var list = new List<CharacterWealth>();
            var wealth = new CharacterWealth();
            wealth.Name = "adventurer";
            wealth.Levels.Add(new CharacterWealth.CharacterWealthLevel(1, 0));
            wealth.Levels.Add(new CharacterWealth.CharacterWealthLevel(2, 2000));
            list.Add(wealth);
            wealthGateway = new EntityGateway<CharacterWealth>(list);
        }

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

        [Test]
        public void IfAfterFirstLevelPickStartValueFromWealthList()
        {
            var character = new CharacterSheet();
            character.SetLevel(2);
            var action = new StartingWealth(wealthGateway);
            action.Process(character, new CharacterBuildStrategy());

            Assert.That(character.Inventory.CoinPurse.Value, Is.EqualTo(2000));

        }
    }
}