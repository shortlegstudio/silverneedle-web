// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace  SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using SilverNeedle.Utility;

    public class AddBloodlineArcana : ICharacterDesignStep, IFeatureCommand
    {
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(ComponentContainer components)
        {
            var bloodline = components.Get<Bloodline>();
            var arcana = bloodline.BloodlineArcana.Instantiate<BloodlineArcana>();
            components.Add(arcana);
        }
    }
}