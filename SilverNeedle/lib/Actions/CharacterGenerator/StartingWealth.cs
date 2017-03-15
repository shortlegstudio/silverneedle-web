// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;
    
    /// <summary>
    /// Hit point generator rolls hitpoints for a character
    /// </summary>
    public class StartingWealth : ICharacterDesignStep
    {
        private EntityGateway<CharacterWealth> wealthGateway;

        public StartingWealth(EntityGateway<CharacterWealth> wealthGateway)
        {
            this.wealthGateway = wealthGateway;
        }

        public StartingWealth()
        {
            this.wealthGateway = GatewayProvider.Get<CharacterWealth>();
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(character.Level > 1)
            {
                UseStartingWealthTable(character);
            }
            else
            {
                UseClassWealth(character);
            }
        }

        private void UseStartingWealthTable(CharacterSheet character)
        {
            var table = wealthGateway.Find("adventurer");
            var wealth = table.Levels.First(x => x.Level == character.Level);
            character.Inventory.CoinPurse.SetValue(wealth.Value);
        }

        private void UseClassWealth(CharacterSheet character)
        {
            ShortLog.Debug("Starting Wealth");
            ShortLog.DebugFormat("Class: {0}", character.Class.Name);

            if (character.Class.StartingWealthDice != null)
            {
                var value = character.Class.StartingWealthDice.Roll() * 10;
                character.Inventory.CoinPurse.AddGold(value);
            }
        }
    }
}