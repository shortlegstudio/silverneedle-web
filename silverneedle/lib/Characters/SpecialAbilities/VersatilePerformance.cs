// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Linq;
    using System.Collections.Generic;

    public class VersatilePerformance : SpecialAbility
    {
        private IList<CharacterSkill> skills = new List<CharacterSkill>();
        public IEnumerable<CharacterSkill> Skills
        {
            get { return skills; }
        }

        public void AddSkill(CharacterSkill skill)
        {
            if(!skill.Name.ContainsIgnoreCase("perform"))
                throw new System.ArgumentException("Must be a perform skill for versatile performance");
            skills.Add(skill);
        }

        public override string Name 
        {
            get
            {
                return "{0} ({1})".Formatted(base.Name, string.Join(", ", this.Skills.Select(x => x.Name)));
            }
        }

    }
}