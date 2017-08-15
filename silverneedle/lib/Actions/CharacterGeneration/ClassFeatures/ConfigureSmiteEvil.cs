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

    public class ConfigureSmiteEvil : ICharacterDesignStep
    {
        private int UsesPerDay { get; set; }

        public ConfigureSmiteEvil(IObjectStore configuration)
        {
            this.UsesPerDay = configuration.GetInteger("uses-per-day");
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var smiteEvil = character.Get<SmiteEvil>();
            if(smiteEvil == null)
            {
                smiteEvil = new SmiteEvil(UsesPerDay);
                character.Add(smiteEvil);
            }
            smiteEvil.UpdateUsesPerDay(this.UsesPerDay);
        }
    }
}