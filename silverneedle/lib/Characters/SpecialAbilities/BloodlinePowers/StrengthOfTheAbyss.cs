// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class StrengthOfTheAbyss : SpecialAbility, IComponent, IBloodlinePower
    {
        private IStatModifier strengthModifier;
        private ClassLevel sorcererLevels;
        public void Initialize(ComponentContainer components)
        {
            var strength = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Strength);
            this.sorcererLevels = components.Get<ClassLevel>();
            strengthModifier = new DelegateStatModifier(strength.Name, "bonus", this.Name, this.CalculateStrengthBonus);
            strength.AddModifier(strengthModifier);
            
        }

        private float CalculateStrengthBonus()
        {
            if(sorcererLevels.Level >= 17)
                return 6;
            if(sorcererLevels.Level >= 13)
                return 4;
            
            return 2;
        }
    }
}