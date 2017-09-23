// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Dice;
    public class UnarmedMonk : AttackStatistic
    {
        private MonkUnarmedStrike unarmedStrike;
        public UnarmedMonk(MonkUnarmedStrike unarmed)
        {
            this.unarmedStrike = unarmed;
        }

        public override Cup Damage
        {
            get 
            {
                return Dice.DiceStrings.ParseDice(unarmedStrike.GetDamage());
            }
        }

        public override string Name 
        {
            get { return "Unarmed"; }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.Name, this.Damage);
        }
    }
}