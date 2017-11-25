// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class AddBloodlineBonusFeat : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var bloodline = character.Get<Bloodline>();
            character.Add(new FeatToken(bloodline.GetBonusFeats()));
        }
    }
}