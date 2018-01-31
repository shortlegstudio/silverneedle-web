// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    
    public class CombatStyle : IAbility, IGatewayObject
    {
        private IDictionary<int, string[]> bonusFeats;


        public CombatStyle(IObjectStore configuration)
        {
            this.CombatStyleName = configuration.GetString("name");

            bonusFeats = new Dictionary<int, string[]>();
            var bonus = configuration.GetObject("bonus-feats");
            foreach(var level in bonus.Children)
            {
                bonusFeats.Add(level.GetInteger("level"), level.GetList("feats"));
            }
        }

        public string CombatStyleName { get; set; }

        public string[] GetFeats(int level)
        {
            List<string> feats = new List<string>();
            foreach(var featLevel in bonusFeats.Keys)
            {
                if(featLevel <= level)
                    feats.Add(bonusFeats[featLevel]);

            }
            return feats.ToArray();
        }

        public bool Matches(string name)
        {
            return this.CombatStyleName.EqualsIgnoreCase(name);
        }

        public string DisplayString()
        {
            return string.Format("Combat Style ({0})", CombatStyleName);
        }
    }
}