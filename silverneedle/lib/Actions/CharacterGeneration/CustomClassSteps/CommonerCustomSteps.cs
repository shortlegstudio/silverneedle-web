// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.CustomClassSteps
{
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class CommonerCustomSteps : ICharacterFeatureCommand 
    {
        public void Execute(Utility.ComponentContainer components)
        {
            // Select a simple weapon
            var choice = GatewayProvider.Get<Weapon>().SimpleWeapons().ChooseOne();
            components.Add(new WeaponProficiency(choice.ProficiencyName));
        }
    }
}