//-----------------------------------------------------------------------
// <copyright file="SkillModifier.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    /// <summary>
    /// Skill modifier changes skill values
    /// </summary>
    public class SkillModifier : BasicStatModifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.SkillModifier"/> class.
        /// </summary>
        /// <param name="modifier">Modifier for the skill.</param>
        /// <param name="reason">Reason this modifier is applied.</param>
        /// <param name="skillName">Name of the skill.</param>
        public SkillModifier(int modifier, string reason, string skillName)
            : base(modifier, reason)
        {
            this.SkillName = skillName;
        }

        /// <summary>
        /// Gets the name of the skill.
        /// </summary>
        /// <value>The name of the skill.</value>
        public string SkillName { get; private set; }
    }
}