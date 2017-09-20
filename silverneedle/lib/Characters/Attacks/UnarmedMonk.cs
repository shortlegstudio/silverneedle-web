// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    public class UnarmedMonk : AttackStatistic
    {
        public UnarmedMonk(string damage)
        {
            this.Damage = Dice.DiceStrings.ParseDice(damage);
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