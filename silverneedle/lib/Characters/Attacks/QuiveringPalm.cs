// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using System;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class QuiveringPalm : WeaponAttack, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private IValueStatistic saveDC = new BasicStat("Quivering Palm Save DC", 10);
        public override int SaveDC 
        { 
            get { return saveDC.TotalValue; }
            protected set { throw new NotImplementedException(); }
        }

        public void Initialize(ComponentContainer components)
        {
            this.AttackType = AttackTypes.Special;
            saveDC.AddModifier(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom).UniversalStatModifier);
            var monkLevels = components.Get<ClassLevel>();
            saveDC.AddModifier(new DelegateStatModifier(saveDC.Name,
                "Monk Levels",
                () => { return monkLevels.Level / 2; })
            );
        }

        public override string DisplayString()
        {
            return "{0} (Fort DC: {1})".Formatted(this.Name, this.SaveDC);
        }
    
    }
}