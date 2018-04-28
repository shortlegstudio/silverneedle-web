// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class ManeuverTraining : IAbility, IComponent, INameByType
    {
        public ComponentContainer Parent { get; set; }
        IValueStatModifier modifier;
        IValueStatistic baseAttackBonus;
        ClassLevel monkLevels; 
        
        public void Initialize(ComponentContainer components)
        {
            baseAttackBonus = components.FindStat<IValueStatistic>(StatNames.BaseAttackBonus);
            monkLevels = components.Get<ClassLevel>();
            modifier = new DelegateStatModifier(
                StatNames.CMB,
                "Monk",
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

        public string DisplayString()
        {
            return this.Name();
        }
    }
}