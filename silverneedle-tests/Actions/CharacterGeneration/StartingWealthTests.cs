// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using System.Collections.Generic;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    
    
    public class StartingWealthTests : RequiresDataFiles
    {
        EntityGateway<CharacterWealth> wealthGateway;

        public StartingWealthTests()
        {
            var list = new List<CharacterWealth>();
            var wealth = new CharacterWealth();
            wealth.Name = "adventurer";
            wealth.Levels.Add(new CharacterWealth.CharacterWealthLevel(1, 0));
            wealth.Levels.Add(new CharacterWealth.CharacterWealthLevel(2, 2000));
            list.Add(wealth);
            wealthGateway = EntityGateway<CharacterWealth>.LoadFromList(list);
        }

        [Fact]
        public void DoesNothingIfStartingWealthIsNullForClass()
        {
            var cls = Class.CreateForTesting();
            var character = CharacterTestTemplates.AverageBob();
            character.SetClass(cls);
            var action = new StartingWealth();
            action.ExecuteStep(character);
        }

        [Fact]
        public void CalculatesWealthBasedOnTheDiceInGoldPiecesTimesTen()
        {
            var cls = Class.CreateForTesting();
            cls.StartingWealthDice = SilverNeedle.Dice.DiceStrings.ParseDice("2d6");
            var character = CharacterTestTemplates.AverageBob();
            character.SetClass(cls);

            var action = new StartingWealth();
            action.ExecuteStep(character);

            Assert.True(character.Inventory.CoinPurse.Gold.Pieces > 19);
            Assert.True(character.Inventory.CoinPurse.Gold.Pieces < 121);
        }

        [Fact]
        public void IfAfterFirstLevelPickStartValueFromWealthList()
        {
            var character = CharacterTestTemplates.Barbarian();
            character.SetLevel(2);
            var action = new StartingWealth(wealthGateway);
            action.ExecuteStep(character);

            Assert.Equal(character.Inventory.CoinPurse.Value, 2000);

        }
    }
}