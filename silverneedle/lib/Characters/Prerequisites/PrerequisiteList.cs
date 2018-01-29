// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    /// <summary>
    /// Prerequisites needed to be able to use a feat
    /// </summary>
    public class PrerequisiteList : List<IPrerequisite>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Prerequisites"/> class.
        /// </summary>
        public PrerequisiteList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Prerequisites"/> class.
        /// </summary>
        /// <param name="yaml">Yaml node wrapper to parse requisites from.</param>
        public PrerequisiteList(IObjectStore yaml)
        {
            this.ParseYaml(yaml);
        }

        /// <summary>
        /// Determines whether this instance is qualified the specified sheet.
        /// </summary>
        /// <returns><c>true</c> if this instance is qualified the specified sheet; otherwise, <c>false</c>.</returns>
        /// <param name="character">Character to check qualifications</param>
        public bool IsQualified(ComponentContainer components)
        {
            if (this.Count == 0)
            {
                return true;
            }

            return this.All(x => x.IsQualified(components));
        }

        /// <summary>
        /// Parses the yaml and find prerequisites
        /// </summary>
        /// <param name="yaml">Yaml Node to parse </param>
        private void ParseYaml(IObjectStore yaml)
        {
            if(yaml == null || !yaml.HasChildren)
                return;
                
            foreach (var prereq in yaml.Children)
            {
                var key = prereq.Keys.First();
                IPrerequisite newreq = null;
                switch (key)
                {
                    case "ability":
                        newreq = new SpecialAbilityPrerequisite(prereq.GetString(key));
                        break;
                    case "baseattackbonus":
                        newreq = new BaseAttackBonusPrerequisite(prereq.GetString(key));
                        break;
                    case "casterlevel":
                        newreq = new CasterLevelPrerequisite(prereq.GetString(key));
                        break;
                    case "classfeature":
                        newreq = new ClassFeaturePrerequisite(prereq.GetString(key));
                        break;
                    case "classlevel":
                        newreq = new ClassLevelPrerequisite(prereq.GetString(key));
                        break;
                    case "weapon-proficiency":
                        newreq = new WeaponProficiencyPrerequisite(prereq);
                        break;
                    case "race":
                        newreq = new RacePrerequisite(prereq.GetString(key));
                        break;
                    case "skillranks":
                        newreq = new SkillRankPrerequisite(prereq);
                        break;
                    case "strength":
                    case "dexterity":
                    case "constitution":
                    case "intelligence":
                    case "wisdom":
                    case "charisma":
                        newreq = new AbilityPrerequisite(key.EnumValue<AbilityScoreTypes>(), prereq.GetInteger(key));
                        break;
                    case "custom":
                        newreq = prereq.GetString(key).Instantiate<IPrerequisite>();
                        break;
                    default:
                        throw new KeyNotFoundException(key);
                }

                if (newreq != null)
                {
                    this.Add(newreq);
                }
            }
        }
    }
}