// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Utility;

    /// <summary>
    /// Skill ranks tracker for the character
    /// </summary>
    public class SkillRanks : ISkillRanks, IComponent
    {
        public ComponentContainer Parent { get; set; }

        public IValueStatistic ArmorCheckPenalty { get { return Parent.FindStat<IValueStatistic>(StatNames.ArmorCheckPenalty); } }

        public SkillRanks() { }

        public int GetScore(string skill)
        {
            return GetSkill(skill).Score();
        }

        public CharacterSkill GetSkill(string skill)
        {
            try 
            {
                return Parent.FindStat<CharacterSkill>(skill);
            }
            catch
            {
                ShortLog.ErrorFormat("Cannot find skill: {0}", skill);
                throw;
            }
        }

        /// <summary>
        /// Gets the skills.
        /// </summary>
        /// <returns>The skills.</returns>
        public IEnumerable<CharacterSkill> GetSkills()
        {
            return Parent.GetAll<CharacterSkill>();
        }

        /// <summary>
        /// Gets the ranked skills.
        /// </summary>
        /// <returns>The ranked skills.</returns>
        public IEnumerable<CharacterSkill> GetRankedSkills()
        {
            return this.GetSkills().Where(x => x.Ranks > 0);
        }

        public void SetClassSkill(string skillName)
        {
            ShortLog.DebugFormat("Setting Class Skill: {0}", skillName);
            switch(skillName.ToLower())
            {
                case "craft":
                case "profession":
                case "perform":
                    var skills = GetSkills().Where(x => x.Name.StartsWith(skillName, StringComparison.OrdinalIgnoreCase));
                    foreach (var craft in skills)
                    {
                        craft.ClassSkill = true;
                    }
                    break;
                default:
                    GetSkill(skillName).ClassSkill = true;
                    break;
            }
        }

        public IEnumerable<CharacterSkill> GetClassSkills()
        {
            return GetSkills().Where(x => x.ClassSkill);
        }

        public void Initialize(ComponentContainer components)
        {
            ArmorCheckPenalty.AddModifier(new EquippedArmorCheckPenaltyModifier(components));
            var armorCheckSkills = GetSkills().Where(x => x.ArmorCheckPenalty);
            foreach(var skl in armorCheckSkills)
            {
                var modifier = new DelegateStatModifier(skl.Name, "Armor", () => { return ArmorCheckPenalty.TotalValue; });
                skl.AddModifier(modifier);
            }
        }
    }
}