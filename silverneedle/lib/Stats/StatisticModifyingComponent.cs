// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class StatisticModifyingComponent : IComponent
    {
        private IList<ValueStatModifier> modifiers = new List<ValueStatModifier>();
        public StatisticModifyingComponent(IObjectStore configuration)
        {
            foreach(var modifier in configuration.GetObject("modifiers").Children)
            {
                modifiers.Add(
                    new ValueStatModifier(modifier)
                );
            }
        }

        public void Initialize(ComponentBag components)
        {
            foreach(var mod in modifiers)
            {
                var statistic = components.FindStat(mod.StatisticName);
                if(statistic == null)
                    throw new StatisticNotFoundException(mod.StatisticName);
                statistic.AddModifier(mod);
            }
        }
    }
}