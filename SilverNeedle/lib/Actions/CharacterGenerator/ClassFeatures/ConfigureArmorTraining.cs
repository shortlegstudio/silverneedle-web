// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGenerator.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class ConfigureArmorTraining : ICharacterDesignStep
    {
        public int ArmorTrainingLevel { get; private set; }
        public ConfigureArmorTraining(IObjectStore data)
        {
            ArmorTrainingLevel = data.GetInteger("level");
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {

            var armorTraining = character.Components.Get<ArmorTraining>();
            if(armorTraining == null)
            {
                armorTraining = new ArmorTraining();
                character.AddAbility(armorTraining);
            } 

            armorTraining.Level = ArmorTrainingLevel;
            armorTraining.Name = string.Format("Armor Training {0}", armorTraining.Level);
        }
    }
}
