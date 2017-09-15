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
        private IList<IStatModifier> modifiers = new List<IStatModifier>();
        public StatisticModifyingComponent(IObjectStore configuration)
        {
            var mods = configuration.GetObjectOptional("modifiers");
            if(mods != null)
            {
                foreach(var modifier in mods.Children)
                {
                    modifiers.Add(
                        new ValueStatModifier(modifier)
                    );
                }
            }

            var conditionalModifiers = configuration.GetObjectOptional("conditional-modifiers");
            if(conditionalModifiers != null)
            {
                foreach(var conditional in conditionalModifiers.Children)
                {
                    var mod = new ValueStatModifier(conditional);
                    var condition = conditional.GetString("condition");
                    modifiers.Add(
                        new ConditionalStatModifier(mod, condition)
                    );
                }
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