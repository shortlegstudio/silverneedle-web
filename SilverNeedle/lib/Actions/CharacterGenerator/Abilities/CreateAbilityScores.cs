// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.Abilities
{
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class CreateAbilityScores : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var roller = strategy.AbilityScoreRoller.Instantiate<ICharacterDesignStep>();
            roller.Process(character, strategy);
        }
    }
}