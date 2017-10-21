// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class ConfigureWeaponMastery : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var chosenWeapon = GatewayProvider.Get<Weapon>().ChooseOne();
            var mastery = new WeaponMastery(chosenWeapon);
            character.Add(mastery);
        }
    }
}