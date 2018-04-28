// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    public class TelekineticFist : IAbility, INameByType, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel sourceLevel;
        private AbilityScore baseAbility;

        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
            baseAbility = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Intelligence);
        }

        public int UsesPerDay
        {
            get { return 3 + baseAbility.TotalModifier; }
        }

        public Cup Damage
        {
            get 
            {
                return new Cup(Die.D4(), sourceLevel.Level / 2);
            }
        }

        public string DisplayString()
        {
            return "{0} {1} ({2}/day)".Formatted(
                this.Name(),
                Damage,
                UsesPerDay
            );
        }

    }
}