// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class DivinersFortune : SpecialAbility, IComponent
    {
        private ClassLevel sourceLevel;
        private AbilityScore baseAbility;
        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
            baseAbility = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Intelligence);
        }

        public int Bonus
        {
            get { return (sourceLevel.Level/2).AtLeast(1); }
        }

        public int UsesPerDay
        {
            get { return 3 + baseAbility.TotalModifier; }
        }

        public override string Name
        {
            get { return "{0} {1} ({2}/day)".Formatted(base.Name, Bonus.ToModifierString(), UsesPerDay); }
        }
    }
}