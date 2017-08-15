// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    public class ProcessCustomClassFeatures : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var currentClass = character.Class;
            var currentLevel = character.Level;
            var level = currentClass.GetLevel(currentLevel);

            if(level != null)
            {
                foreach(var step in level.Steps)
                {
                    step.Process(character, strategy);
                }

                foreach(var ability in level.Abilities)
                {
                    character.Add(ability);
                }
            }
        }
    }
}