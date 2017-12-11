// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    public class TelekineticFist : SpecialAbility, IComponent
    {
        private ClassLevel sourceLevel;
        private AbilityScore baseAbility;

        public void Initialize(ComponentBag components)
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

        public override string Name
        {
            get
            {
                return "{0} {1} ({2}/day)".Formatted(
                    base.Name,
                    Damage,
                    UsesPerDay
                );
            }
        }

    }
}