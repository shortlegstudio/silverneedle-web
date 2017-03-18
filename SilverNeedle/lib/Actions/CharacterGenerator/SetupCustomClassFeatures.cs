// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using SilverNeedle.Actions;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class SetupCustomClassFeatures : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(!string.IsNullOrEmpty(character.Class.CustomBuildStep))
            {
                var step = character.Class.CustomBuildStep.Instantiate<ICharacterDesignStep>();
                step.Process(character, strategy);
            }
        }
    }
}