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
        public void Initialize(ComponentBag components)
        {
            var offense = components.Get<OffenseStats>();
            offense.AddAttack(new ChannelEnergyAttack(components));
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
                    return new Cup(Die.GetDice(DiceSides.d6, classLevel.Level / 2));
                }
            }

            public override string ToString()
            {
                return string.Format("{0} (DC: {1}, {2})", Name, SaveDC, Damage.ToString());
            }
        }
    }
}