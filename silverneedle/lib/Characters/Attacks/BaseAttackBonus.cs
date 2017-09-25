// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    public class BaseAttackBonus : BasicStat
    {
        public BaseAttackBonus() : base(StatNames.BaseAttackBonus)
        {

        }

        public int NumberOfAttacks
        {
            get
            {
                return 1 + (this.TotalValue - 1) / 5;
            }
        }

        public int GetAttackBonus(int attack)
        {
            if(attack > NumberOfAttacks)
                throw new ForbiddenByRulesException();
            var penalty = (attack - 1) * 5;
            return this.TotalValue - penalty;
        }
    }
}