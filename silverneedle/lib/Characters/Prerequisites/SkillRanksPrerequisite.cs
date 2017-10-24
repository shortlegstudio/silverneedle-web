// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    /// <summary>
    /// Skill rank prerequisite.
    /// </summary>
    public class SkillRankPrerequisite : IPrerequisite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkillRankPrerequisite"/> class.
        /// </summary>
        /// <param name="value">Value to meet the requirements.</param>
        public SkillRankPrerequisite(string value)
        {
            var vals = value.Split(' ');
            this.SkillName = vals[0];
            this.Minimum = int.Parse(vals[1]);
        }

        /// <summary>
        /// Gets or sets the name of the skill.
        /// </summary>
        /// <value>The name of the skill.</value>
        public string SkillName { get; set; }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum { get; set; }

        /// <summary>
        /// Determines whether this instance is qualified the specified character.
        /// </summary>
        /// <returns>true if the character is qualified</returns>
        /// <param name="character">Character to assess qualification.</param>
        public bool IsQualified(CharacterSheet character)
        {
            return false;
        }
    }
}