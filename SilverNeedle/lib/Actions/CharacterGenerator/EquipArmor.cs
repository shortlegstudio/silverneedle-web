// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGenerator
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;

    public class EquipArmor : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var armor = character.Inventory.GearOfType<IArmor>();
            foreach(var a in armor)
            {
                character.Inventory.EquipItem(a);
            }
        }
    }
}