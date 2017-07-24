// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class ChannelEnergy : SpecialAbility, IComponent
    {
        public ChannelEnergyAttack ChannelAttack { get; private set; }
        public void Initialize(ComponentBag components)
        {
            this.ChannelAttack = new ChannelEnergyAttack(components);
            var offense = components.Get<OffenseStats>();
            offense.AddAttack(ChannelAttack);
        }

        public class ChannelEnergyAttack : AttackStatistic
        {
            private AbilityScore charisma;
            private ClassLevel classLevel;
            public ChannelEnergyAttack(ComponentBag components)
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
        }
    }
}