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

    public class AddBloodlinePower : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var bloodline = character.Get<Bloodline>();
            var bloodlineLevel = character.Get<ClassLevel>();
            var powerType = bloodline.GetPower(bloodlineLevel.Level);
            var power = Reflector.Instantiate<IBloodlinePower>(powerType);
            character.Add(power);
        }
    }
}