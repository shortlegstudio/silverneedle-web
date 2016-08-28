//-----------------------------------------------------------------------
// <copyright file="Prerequisites.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Yaml;


    /// <summary>
    /// Prerequisites needed to be able to use a feat
    /// </summary>
    public class Prerequisites : List<Prerequisites.Prerequisite>
    {
        /// <summary>
        /// The prerequisite key names.
        /// </summary>
        private string[] prerequisiteKeyNames =
            { 
                "ability", "race", "feat", "classfeature", "proficiency", 
                "casterlevel", "baseattackbonus", "classlevel", "skillranks"
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Prerequisites"/> class.
        /// </summary>
        public Prerequisites()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Prerequisites"/> class.
        /// </summary>
        /// <param name="yaml">Yaml node wrapper to parse requisites from.</param>
        public Prerequisites(YamlNodeWrapper yaml)
        {
            this.ParseYaml(yaml);
        }

        /// <summary>
        /// Determines whether this instance is qualified the specified sheet.
        /// </summary>
        /// <returns><c>true</c> if this instance is qualified the specified sheet; otherwise, <c>false</c>.</returns>
        /// <param name="character">Character to check qualifications</param>
        public bool IsQualified(CharacterSheet character)
        {
            if (this.Count == 0)
            {
                return true;
            }

            return this.All(x => x.IsQualified(character));
        }

        /// <summary>
        /// Parses the yaml and find prerequisites
        /// </summary>
        /// <param name="yaml">Yaml Node to parse </param>
        private void ParseYaml(YamlNodeWrapper yaml)
        {
            foreach (var prereq in yaml.Children())
            {
                Prerequisite newreq = null;

                // TODO: This seems clunky and weird
                foreach (var key in this.prerequisiteKeyNames)
                {
                    var val = prereq.GetStringOptional(key);
                    if (val != null)
                    {
                        switch (key)
                        {
                            case "ability":
                                newreq = new AbilityPrerequisite(val);
                                break;
                            case "baseattackbonus":
                                newreq = new BaseAttackBonusPrerequisite(val);
                                break;
                            case "casterlevel":
                                newreq = new CasterLevelPrerequisite(val);
                                break;
                            case "classfeature":
                                newreq = new ClassFeaturePrerequisite(val);
                                break;
                            case "classlevel":
                                newreq = new ClassLevelPrerequisite(val);
                                break;
                            case "feat":
                                newreq = new FeatPrerequisite(val);
                                break;
                            case "proficiency":
                                newreq = new ProficiencyPrerequisite(val);
                                break;
                            case "race":
                                newreq = new RacePrerequisite(val);
                                break;
                            case "skillranks":
                                newreq = new SkillRankPrerequisite(val);
                                break;
                        }

                        break;
                    }
                }

                if (newreq != null)
                {
                    this.Add(newreq);
                }
            }
        }

        /// <summary>
        /// Basic prerequisite abstraction
        /// </summary>
        public abstract class Prerequisite
        {
            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns><c>true</c> if this instance is qualified the specified character; otherwise, <c>false</c>.</returns>
            /// <param name="character">Character to assess qualification.</param>
            public abstract bool IsQualified(CharacterSheet character);
        }

        /// <summary>
        /// Ability prerequisite are based on an ability score
        /// </summary>
        public class AbilityPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="AbilityPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Requisite string.</param>
            public AbilityPrerequisite(string value)
            {
                // TODO: Determine whether it's logical to be parsing at this level
                var vals = value.Split(' ');
                this.Ability = AbilityScore.GetType(vals[0]);
                this.Minimum = int.Parse(vals[1]);
            }

            /// <summary>
            /// Gets or sets the ability for the prerequisite.
            /// </summary>
            /// <value>The ability type.</value>
            public AbilityScoreTypes Ability { get; set; }

            /// <summary>
            /// Gets or sets the minimum value for the ability
            /// </summary>
            /// <value>The minimum value.</value>
            public int Minimum { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// Character must have equal to or more of the specified ability
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public override bool IsQualified(CharacterSheet character)
            {
                return character.AbilityScores.GetScore(this.Ability) >= this.Minimum;
            }
        }

        /// <summary>
        /// Race prerequisite requires a specific racial type
        /// </summary>
        public class RacePrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="RacePrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public RacePrerequisite(string value)
            {
                this.Race = value;
            }

            /// <summary>
            /// Gets or sets the race.
            /// </summary>
            /// <value>The race the character must be.</value>
            public string Race { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }

        /// <summary>
        /// Feat prerequisite.
        /// </summary>
        public class FeatPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="FeatPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public FeatPrerequisite(string value)
            {
                this.Feat = value;
            }

            /// <summary>
            /// Gets or sets the feat.
            /// </summary>
            /// <value>The feat.</value>
            public string Feat { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }

        /// <summary>
        /// Class feature prerequisite.
        /// </summary>
        public class ClassFeaturePrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="ClassFeaturePrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public ClassFeaturePrerequisite(string value)
            {
                this.ClassFeature = value;
            }

            /// <summary>
            /// Gets or sets the class feature.
            /// </summary>
            /// <value>The class feature.</value>
            public string ClassFeature { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }

        /// <summary>
        /// Proficiency prerequisite.
        /// </summary>
        public class ProficiencyPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="ProficiencyPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public ProficiencyPrerequisite(string value)
            {
                this.Proficiency = value;
            }

            /// <summary>
            /// Gets or sets the proficiency.
            /// </summary>
            /// <value>The proficiency.</value>
            public string Proficiency { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }

        /// <summary>
        /// Caster level prerequisite.
        /// </summary>
        public class CasterLevelPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="CasterLevelPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public CasterLevelPrerequisite(string value)
            {
                this.CasterLevel = value;
            }

            /// <summary>
            /// Gets or sets the caster level.
            /// </summary>
            /// <value>The caster level.</value>
            public string CasterLevel { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }

        /// <summary>
        /// Base attack bonus prerequisite.
        /// </summary>
        public class BaseAttackBonusPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="BaseAttackBonusPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public BaseAttackBonusPrerequisite(string value)
            {
                this.AttackBonus = value;
            }

            /// <summary>
            /// Gets or sets the attack bonus.
            /// </summary>
            /// <value>The attack bonus.</value>
            public string AttackBonus { get; set; }

            /// <summary>
            /// Determines whether this instance is qualified the specified character.
            /// </summary>
            /// <returns>true if the character is qualified</returns>
            /// <param name="character">Character to assess qualification.</param>
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }

        /// <summary>
        /// Class level prerequisite.
        /// </summary>
        public class ClassLevelPrerequisite : Prerequisite
        {
            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="ClassLevelPrerequisite"/> class.
            /// </summary>
            /// <param name="value">Value to meet the requirements.</param>
            public ClassLevelPrerequisite(string value)
            {
                var vals = value.Split(' ');
                this.Class = vals[0];
                this.Minimum = int.Parse(vals[1]);
            }

            /// <summary>
            /// Gets or sets the class.
            /// </summary>
            /// <value>The class.</value>
            public string Class { get; set; }

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
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }

        /// <summary>
        /// Skill rank prerequisite.
        /// </summary>
        public class SkillRankPrerequisite : Prerequisite
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
            public override bool IsQualified(CharacterSheet character)
            {
                return false;
            }
        }
    }
}