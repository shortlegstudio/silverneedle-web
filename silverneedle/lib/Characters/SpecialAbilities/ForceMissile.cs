// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    public class ForceMissile : IAbility, INameByType, IComponent
    {
        public ComponentContainer Parent { get; set; }
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

        public string DisplayString()
        {
            return "{0} {1} ({2}/day)".Formatted(this.Name(), Damage, UsesPerDay); 
        }

        public void Initialize(ComponentContainer components)
        {
            baseAbility = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Intelligence);
            intenseSpells = components.Get<IntenseSpells>();
        }
    }
}