// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;

    public class BaseAttackBonusPrerequisite : IPrerequisite
    {
        public BaseAttackBonusPrerequisite(string value)
        {
            this.AttackBonus = value.ToInteger();
        }

        public int AttackBonus { get; set; }

        public bool IsQualified(ComponentContainer components)
        {
            return components.FindStat(StatNames.BaseAttackBonus).TotalValue >= AttackBonus;
        }
    }

}