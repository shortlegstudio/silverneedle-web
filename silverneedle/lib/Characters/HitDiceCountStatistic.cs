// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System.Collections.Generic;
using SilverNeedle.Utility;

namespace SilverNeedle.Characters
{
    public class HitDiceCountStatistic : IValueStatistic, IComponent
    {
        private IDiceStatistic _hitDice;
        public int TotalValue { get { return _hitDice.Dice.Count; } }

        public string Name { get { return "Hit Dice Count"; } }

        public IEnumerable<IStatisticModifier> Modifiers => throw new System.NotImplementedException();

        public void AddModifier(IStatisticModifier modifier)
        {
            throw new System.NotImplementedException();
        }

        public int GetConditionalValue(string condition)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetConditions()
        {
            throw new System.NotImplementedException();
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        public void RemoveModifier(IStatisticModifier modifier)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(ComponentContainer container)
        {
            _hitDice = container.FindStat<IDiceStatistic>("hit dice");
        }
    }
}