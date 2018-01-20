// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using System.Linq;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class LayOnHands : IAbility, IComponent, IUsesPerDay
    {
        private BasicStat usesPerDayStatistic;
        private DiceStatistic healingDice;
        public int UsesPerDay 
        { 
            get { return usesPerDayStatistic.TotalValue; }
        }
        public Cup HealingDice 
        { 
            get 
            { 
                return healingDice.Dice;
            }
        }

        private ClassLevel paladinLevel;
        
        public void Initialize(ComponentContainer components)
        {
            this.usesPerDayStatistic = new BasicStat(this.UsesPerDayStatName());
            this.healingDice = new DiceStatistic("Lay On Hands Dice", "1d6");
            components.Add(usesPerDayStatistic);
            components.Add(healingDice);
            paladinLevel = components.Get<ClassLevel>();
        }

        public string DisplayString() 
        {
            return string.Format("Lay on Hands ({0}, {1}/day)", this.HealingDice.ToString(), this.UsesPerDay);
        }

        public bool MaximizeAmount { get; set; }
    }
}