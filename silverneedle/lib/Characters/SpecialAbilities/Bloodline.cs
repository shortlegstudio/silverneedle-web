// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    
    public class Bloodline : IAbility, IComponent, IGatewayObject
    {
        public ComponentContainer Parent { get; set; }
        private Dictionary<int, string> powers = new Dictionary<int, string>();
        private Dictionary<int, string> bonusSpells = new Dictionary<int, string>();
        private string[] bonusFeats;
        private string[] classSkillOptions;
        private string bloodlineName;

        protected Bloodline(string name, string[] classSkillOptions, Dictionary<int, string> powers, Dictionary<int, string> bonusSpells, string[] bonusFeats)
        {
            this.bloodlineName = name;
            this.classSkillOptions = classSkillOptions;
            this.powers = powers;
            this.bonusFeats = bonusFeats;
            this.bonusSpells = bonusSpells;
        }
        public Bloodline(IObjectStore configuration) : base()
        {
            bloodlineName = configuration.GetString("name");
            BloodlineArcana = configuration.GetString("arcana");
            classSkillOptions = configuration.GetList("class-skill");
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
        }

        public string BloodlineArcana { get; private set; }

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

        public virtual void Initialize(ComponentContainer components)
        {
            var skills = components.Get<SkillRanks>();
            var strategy = components.Get<CharacterStrategy>();
            AssignClassSkill(skills, strategy);
        }

        private void AssignClassSkill(SkillRanks skillRanks, CharacterStrategy strategy)
        {
            var chooseSkill = classSkillOptions.ChooseOne();
            skillRanks.GetSkill(chooseSkill).ClassSkill = true;
        }

        public bool Matches(string name)
        {
            return this.bloodlineName.EqualsIgnoreCase(name);
        }

        public static Bloodline CreateWithValues(string name, string[] classSkillOptions, Dictionary<int, string> powers, Dictionary<int, string> bonusSpells, string[] bonusFeats)
        {
            return new Bloodline(name, classSkillOptions, powers, bonusSpells, bonusFeats);
        }

        public virtual string DisplayString()
        {
            return string.Format("Bloodline ({0})", bloodlineName);
        }
    }
}