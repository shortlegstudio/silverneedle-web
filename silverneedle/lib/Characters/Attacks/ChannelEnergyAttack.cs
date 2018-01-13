// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Utility;
    using SilverNeedle.Dice;
     public class ChannelEnergyAttack : IAttack
    {
        private AbilityScore charisma;
        private ClassLevel classLevel;
        public ChannelEnergyAttack(ComponentContainer components)
        {
            this.charisma = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
            this.classLevel = components.Get<ClassLevel>();
        }

        public string Name { get { return "Channel Energy"; } }
        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public bool MaximizeAmount { get; set; }

        public int SaveDC
        {
            get
            {
                return 10 + classLevel.Level / 2 + charisma.TotalModifier;
            }
        }

        public Cup Damage
        {
            get
            {
                var cup = new Cup(Die.GetDice(DiceSides.d6, classLevel.Level / 2));
                cup.MaximizeAmount = this.MaximizeAmount;
                return cup;
            }
        }

        public string DisplayString()
        {
            return string.Format("{0} (DC: {1}, {2})", Name, SaveDC, Damage.ToString());
        }
    }
}