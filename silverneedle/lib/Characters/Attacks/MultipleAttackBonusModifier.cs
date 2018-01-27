// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using System.Collections.Generic;
    public class MultipleAttackBonusModifier : IValueStatModifier
    {
        public MultipleAttackBonusModifier(int attackNumber)
        {
            Modifier = (attackNumber - 1) * -5;
        }

        public float Modifier { get; private set; }

        public string ModifierType { get { return "penalty"; } }

        public string StatisticName { get { return StatNames.BaseAttackBonus; } } 
        public string Condition { get; set; }
        public string StatisticType { get; set; }

        private static IList<ConditionalStatModifier> multipleAttackList;
        private static int attacksToSupport = 10;

        public static IEnumerable<ConditionalStatModifier> GetConditionalMultipleAttackModifiers()
        {
            if(multipleAttackList == null)
            {
                multipleAttackList = new List<ConditionalStatModifier>();
                for(int i = 1; i < attacksToSupport; i++)
                {
                    var mod = new MultipleAttackBonusModifier(i);
                    var cond = new ConditionalStatModifier(mod, "attack {0}".Formatted(i));
                    multipleAttackList.Add(cond);
                }
            }

            return multipleAttackList;
        }
    }
}