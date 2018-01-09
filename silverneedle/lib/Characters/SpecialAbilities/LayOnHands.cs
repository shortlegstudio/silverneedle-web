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
    public class LayOnHands : IAbility, IComponent
    {
        public int UsesPerDay 
        { 
            get { return charismaScore.TotalModifier + paladinLevel.Level / 2; } 
        }
        public Cup HealingDice 
        { 
            get 
            { 
                var cup = new Cup();
                cup.MaximizeAmount = this.MaximizeAmount;
                cup.AddDice(Die.GetDice(DiceSides.d6, paladinLevel.Level / 2)); 
                return cup;
            }
        }

        private ClassLevel paladinLevel;
        private AbilityScore charismaScore; 
        
        public void Initialize(ComponentContainer components)
        {
            paladinLevel = components.GetAll<ClassLevel>().First(x => x.Class.Name.EqualsIgnoreCase("paladin"));
            charismaScore = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Charisma);
        }

        public string DisplayString() 
        {
            return string.Format("Lay on Hands ({0}, {1}/day)", this.HealingDice.ToString(), this.UsesPerDay);
        }

        public bool MaximizeAmount { get; set; }
    }
}