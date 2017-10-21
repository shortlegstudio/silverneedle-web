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

    public class ConfigureArmorMastery : ICharacterDesignStep
    {
        public int Amount { get; private set; }
        public string Type { get; private set; }
        public ConfigureArmorMastery(IObjectStore data)
        {
            Amount = data.GetInteger("amount");
            Type = data.GetString("damage-type");
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var ability = new ArmorMastery(Amount, Type);
            character.Add(ability);
        }
    }
}