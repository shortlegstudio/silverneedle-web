// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class ManeuverTraining : SpecialAbility, IComponent
    {
        IStatModifier modifier;
        IStatistic baseAttackBonus;
        ClassLevel monkLevels; 
        
        public void Initialize(ComponentContainer components)
        {
            baseAttackBonus = components.FindStat(StatNames.BaseAttackBonus);
            monkLevels = components.Get<ClassLevel>();
            modifier = new DelegateStatModifier(
                StatNames.CMB,
                "Monk",
                "Maneuver Training",
                Modifier
            );
            components.ApplyStatModifier(modifier);
        }

        private float Modifier()
        {
            // Base Attack Bonus is built into the CMB calculation, so add Monk levels
            // If the difference would improve
            return System.Math.Max(0, monkLevels.Level - baseAttackBonus.TotalValue);
        }
    }
}