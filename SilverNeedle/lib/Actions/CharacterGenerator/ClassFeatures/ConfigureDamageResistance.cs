// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGenerator.ClassFeatures
{

    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ConfigureDamageResistance : ICharacterDesignStep
    {
        private int Amount { get; set; }
        private string Type { get; set; }
        public ConfigureDamageResistance(IObjectStore data)
        {
            this.Amount = data.GetInteger("amount");
            this.Type = data.GetString("type");
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var dr = new DamageResistance(Amount, Type);
            character.Defense.AddDamageResistance(dr);
        }
    }
}