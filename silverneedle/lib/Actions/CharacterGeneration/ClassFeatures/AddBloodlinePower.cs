// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using SilverNeedle.Utility;

    public class AddBloodlinePower : ICharacterDesignStep, ICharacterFeatureCommand
    {
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var bloodline = components.Get<Bloodline>();
            var bloodlineLevel = components.Get<ClassLevel>();
            var powerType = bloodline.GetPower(bloodlineLevel.Level);
            var power = Reflector.Instantiate<IBloodlinePower>(powerType);
            components.Add(power);
        }
    }
}