// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    public class ForceMissile : SpecialAbility, IComponent
    {
        private AbilityScore baseAbility;
        private IntenseSpells intenseSpells;

        public int UsesPerDay
        {
            get
            {
                return 3 + baseAbility.TotalModifier;
            }
        }

        public Cup Damage
        {
            get
            {
                var cup = new Cup(Die.D4());
                cup.Modifier = intenseSpells.BonusDamage;
                return cup;
            }
        }

        public override string Name
        {
            get { return "{0} {1} ({2}/day)".Formatted(base.Name, Damage, UsesPerDay); }
        }

        public void Initialize(ComponentBag components)
        {
            baseAbility = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Intelligence);
            intenseSpells = components.Get<IntenseSpells>();
        }
    }
}