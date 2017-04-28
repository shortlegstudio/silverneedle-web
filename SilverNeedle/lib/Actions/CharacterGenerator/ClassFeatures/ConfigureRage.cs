// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.ClassFeatures
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class ConfigureRage : ICharacterDesignStep
    {
        IObjectStore rageConfiguration;
        public ConfigureRage(IObjectStore configuration)
        {
            this.rageConfiguration = configuration;
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var rage = character.Components.Get<Rage>();
            if (rage == null)
            {
                rage = new Rage(rageConfiguration);
                character.Add(rage);
            }

            rage.Update(this.rageConfiguration);
        }
    }
}