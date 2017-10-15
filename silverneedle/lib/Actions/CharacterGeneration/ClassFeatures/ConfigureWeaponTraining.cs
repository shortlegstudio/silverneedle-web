// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class ConfigureWeaponTraining : ICharacterDesignStep
    {
        public int WeaponTrainingLevel { get; private set; }
        public ConfigureWeaponTraining(IObjectStore data)
        {
            WeaponTrainingLevel = data.GetInteger("level");
        }
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var trainings = character.Components.GetAll<WeaponTraining>();
            foreach(var t in trainings)
            {
                t.SetLevel(t.Level + 1);
            }
                

            // Add a new group
            var group = EnumHelpers.GetValues<WeaponGroup>().Where(grp => !trainings.Any(already => already.Group == grp)).ChooseOne();
            var weaponTraining = new WeaponTraining(group, 1);
            character.Add(weaponTraining);
        }
    }
}
