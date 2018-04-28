// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class PhysicalEnhancement : AbilityDisplayAsName, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private IValueStatModifier modifier;
        private ClassLevel sourceLevel;
        public void Initialize(ComponentContainer components)
        {
            this.sourceLevel = components.Get<ClassLevel>();
            modifier = new DelegateStatModifier(
                "Physical Attribute",
                "enhancement",
                CalculateBonus
            );

            var abilityType = new AbilityScoreTypes[] {
                AbilityScoreTypes.Strength, 
                AbilityScoreTypes.Dexterity, 
                AbilityScoreTypes.Constitution
            }.ChooseOne();

            var ability = components.Get<AbilityScores>().GetAbility(abilityType);
            ability.AddModifier(modifier);
        }

        private float CalculateBonus()
        {
            return 1 + sourceLevel.Level / 5;
        }
    }
}