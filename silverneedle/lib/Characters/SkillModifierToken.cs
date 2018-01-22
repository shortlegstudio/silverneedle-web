// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SilverNeedle.Serialization;

    public class SkillModifierToken 
    {
        [ObjectStore("skills")]
        public string[] Skills { get; private set; }

        [ObjectStore("modifier")]
        public int Modifier { get; private set; }

        [ObjectStore("modifier-type")]
        public string ModifierType { get; private set; }

        public SkillModifierToken(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public SkillModifierToken(IEnumerable<string> skills, int modifier, string type)
        {
            this.Skills = skills.ToArray();
            this.Modifier = modifier;
            this.ModifierType = type;
        }

        public bool Qualifies(Skill skill)
        {
            bool matches = false;
            foreach(var skillName in this.Skills)
            {
                matches = skill.Name.SearchFor(skillName);
                if(matches)
                    return true;
            }

            return matches;
        }

        public IValueStatModifier CreateModifier(Skill skill)
        {
            return new ValueStatModifier(
                skill.Name,
                Modifier,
                ModifierType
            );
        }
    }
}