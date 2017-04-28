// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    public class DamageResistance : SpecialAbility
    {
        public DamageResistance(int amnt, string damageType) 
        {
            this.DamageType = damageType;
            this.Amount = amnt;
            this.Name = StatNames.DamageResistance + " " + damageType;
        }

        public string DamageType { get; private set; }
        public int Amount { get; set; }

        public override string ToString() 
        {
            return string.Format("{0}/{1}", this.Amount, this.DamageType);
        }
    }
}