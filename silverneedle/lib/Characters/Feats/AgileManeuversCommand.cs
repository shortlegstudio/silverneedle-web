// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Feats
{
    using System.Linq;
    using SilverNeedle.Utility;
    public class AgileManeuversCommand : IFeatureCommand
    {
        public void Execute(ComponentContainer components)
        {
            var cmd = components.FindStat<IValueStatistic>(StatNames.CMB);
            var strMod = cmd.Modifiers.OfType<StatisticStatModifier>().FirstOrDefault(x => x.TrackingStatName.EqualsIgnoreCase("strength-modifier"));
            if(strMod != null)
            {
                cmd.RemoveModifier(strMod);
            }

            var dexMod = new StatisticStatModifier(cmd.Name, components.FindStat<IValueStatistic>("dexterity-modifier"));
            cmd.AddModifier(dexMod);
        }
    }
}