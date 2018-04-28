// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class TouchOfDestiny : IAbility, INameByType, IBloodlinePower, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private AbilityScore charisma;
        private ClassLevel sorcererLevels;
        public void Initialize(ComponentContainer components)
        {
            charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
            this.sorcererLevels = components.Get<ClassLevel>();
        }

        public int UsesPerDay
        {
            get { return 3 + charisma.TotalModifier; }
        }

        public int Bonus
        {
            get { return (sorcererLevels.Level / 2).AtLeast(1); }
        }

        public string DisplayString()
        {
            return "{0} ({1} bonus {2}/day)".Formatted(this.Name(), Bonus.ToModifierString(), UsesPerDay);
        }
    }
}