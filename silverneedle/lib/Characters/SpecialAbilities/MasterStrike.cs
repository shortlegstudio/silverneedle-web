// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Utility;
    public class MasterStrike : IAbility, IComponent, INameByType
    {
        private AbilityScore intelligence;
        private ClassLevel rogueLevel;
        public void Initialize(ComponentContainer components)
        {
            rogueLevel = components.Get<ClassLevel>();
            intelligence = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Intelligence);
        }
        public int SaveDC
        {
            get
            {
                return 10 + intelligence.TotalModifier + rogueLevel.Level / 2;
            }
        }

        public string DisplayString()
        {
            return string.Format("{0} (DC {1})", this.Name(), this.SaveDC);
        }
    }
}