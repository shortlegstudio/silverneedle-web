// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;

    public class NotifyComponentsOfLevelUp : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var levelsUp = character.GetAll<IImprovesWithLevels>();
            foreach(var l in levelsUp)
            {
                l.LeveledUp(character.Components);
            }
        }
    }
}