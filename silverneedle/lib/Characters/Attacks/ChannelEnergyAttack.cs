// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Utility;
    using SilverNeedle.Dice;
     public class ChannelEnergyAttack : WeaponAttack
    {
        private AbilityScore charisma;
        private ClassLevel classLevel;
        public ChannelEnergyAttack(ComponentContainer components)
        {
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
            this.classLevel = components.Get<ClassLevel>();
            this.Name = "Channel Energy";
            this.AttackType = AttackTypes.Special;
        }

        public bool MaximizeAmount { get; set; }

        public override int SaveDC
        {
            get
            {
                return 10 + classLevel.Level / 2 + charisma.TotalModifier;
            }
        }

        public override Cup Damage
        {
            get
            {
                var cup = new Cup(Die.GetDice(DiceSides.d6, classLevel.Level / 2));
                cup.MaximizeAmount = this.MaximizeAmount;
                return cup;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} (DC: {1}, {2})", Name, SaveDC, Damage.ToString());
        }

        public override string DisplayString()
        {
            return string.Format("{0} (DC: {1}, {2})", Name, SaveDC, Damage.ToString());
        }
    }
}