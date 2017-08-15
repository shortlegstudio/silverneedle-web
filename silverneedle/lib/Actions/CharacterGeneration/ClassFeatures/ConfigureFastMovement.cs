// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class ConfigureFastMovement : ICharacterDesignStep
    {
        public int SpeedBonus { get; private set; }

        public ConfigureFastMovement(IObjectStore data)
        {
            SpeedBonus = data.GetInteger("speed");
        }

        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var fastMovement = new FastMovement(SpeedBonus); 
            character.Add(fastMovement);
        }
    }
}