// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.CustomClassSteps
{
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;

    public class CommonerCustomSteps : ICharacterDesignStep 
    {
        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            // Select a simple weapon
            var choice = GatewayProvider.Get<Weapon>().SimpleWeapons().ChooseOne();
            character.Offense.AddWeaponProficiency(choice.ProficiencyName);
        }
    }
}