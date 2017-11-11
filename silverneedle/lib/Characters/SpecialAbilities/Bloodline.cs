// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    
    public class Bloodline : SpecialAbility, IComponent, IGatewayObject
    {
        private Dictionary<int, string> powers = new Dictionary<int, string>();
        private Dictionary<int, string> bonusSpells = new Dictionary<int, string>();
        private string[] bonusFeats;
        private string classSkill;
        private string bloodlineName;
        public Bloodline(IObjectStore configuration) : base()
        {
            bloodlineName = configuration.GetString("name");
            classSkill = configuration.GetString("class-skill");
            var bonuses = configuration.GetObject("bonus-spells");
            foreach(var level in bonuses.Keys)
            {
                bonusSpells[level.ToInteger()] = bonuses.GetString(level);
            }

            var pows = configuration.GetObject("powers");
            foreach(var level in pows.Keys)
            {
                powers[level.ToInteger()] = pows.GetString(level);
            }

            bonusFeats = configuration.GetList("bonus-feats");
            this.Name = string.Format("{0} ({1})", base.Name, bloodlineName);
        }

        public string GetBonusSpell(int level)
        {
            if(bonusSpells.ContainsKey(level))
                return bonusSpells[level];

            return string.Empty;
        }

        public string GetPower(int level)
        {
            if(powers.ContainsKey(level))
                return powers[level];

            return string.Empty;
        }

        public IEnumerable<string> GetBonusFeats()
        {
            return bonusFeats;
        }

        public void Initialize(ComponentBag components)
        {
            var skills = components.Get<SkillRanks>();
            skills.GetSkill(classSkill).ClassSkill = true;

        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}