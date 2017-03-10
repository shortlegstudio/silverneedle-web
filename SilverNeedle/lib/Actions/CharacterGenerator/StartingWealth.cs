// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using SilverNeedle.Characters;
    
    /// <summary>
    /// Hit point generator rolls hitpoints for a character
    /// </summary>
    public class StartingWealth : ICreateStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if (character.Class.StartingWealthDice != null)
            {
                var value = character.Class.StartingWealthDice.Roll() * 10;
                character.CoinPurse.AddGold(value);
            }
        }
    }
}