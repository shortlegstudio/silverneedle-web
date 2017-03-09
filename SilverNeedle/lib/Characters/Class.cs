// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SilverNeedle;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    /// <summary>
    /// Represents a character's Class or profession
    /// </summary>
    public class Class : IGatewayObject
    {
        /// <summary>
        /// The good save rate. TODO: This should be configurable or moved
        /// somewhere closer to saves
        /// </summary>
        public const float GoodSaveRate = 0.667f;

        /// <summary>
        /// The poor save rate. TODO: This should be configurable or moved
        /// somewhere closer to saves
        /// </summary>
        public const float PoorSaveRate = 0.334f;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Class"/> class.
        /// </summary>
        public Class()
        {
            this.ClassSkills = new List<string>();
            this.ArmorProficiencies = new List<string>();
            this.WeaponProficiencies = new List<string>();
            this.Levels = new List<Level>();
        }

        public Class(IObjectStore data) : this()
        {
            LoadFromObjectStore(data);
        }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>The class name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the class skills. These are skills this class is more
        /// capable in.
        /// </summary>
        /// <value>The class skills.</value>
        public IList<string> ClassSkills { get; set; }

        /// <summary>
        /// Gets or sets the skill points per level
        /// </summary>
        /// <value>The skill points.</value>
        public int SkillPoints { get; set; }

        /// <summary>
        /// Gets or sets the hit dice for each level
        /// </summary>
        /// <value>The hit dice die sides.</value>
        public DiceSides HitDice { get; set; }

        /// <summary>
        /// Gets or sets the base attack bonus rate per level.
        /// </summary>
        /// <value>The base attack bonus rate.</value>
        public float BaseAttackBonusRate { get; set; }

        /// <summary>
        /// Gets or sets the fortitude save rate.
        /// </summary>
        /// <value>The fortitude save rate.</value>
        public float FortitudeSaveRate { get; set; }

        /// <summary>
        /// Gets or sets the reflex save rate.
        /// </summary>
        /// <value>The reflex save rate.</value>
        public float ReflexSaveRate { get; set; }

        /// <summary>
        /// Gets or sets the will save rate.
        /// </summary>
        /// <value>The will save rate.</value>
        public float WillSaveRate { get; set; }

        /// <summary>
        /// Gets or sets the armor proficiencies.
        /// </summary>
        /// <value>The armor proficiencies.</value>
        public IList<string> ArmorProficiencies { get; set; }

        /// <summary>
        /// Gets or sets the weapon proficiencies.
        /// </summary>
        /// <value>The weapon proficiencies.</value>
        public IList<string> WeaponProficiencies { get; set; }

        public ClassDevelopmentAge ClassDevelopmentAge { get; set; }

        public IList<Level> Levels { get; set; }

        /// <summary>
        /// Gets a value indicating whether this class has a good fortitude save.
        /// </summary>
        /// <value><c>true</c> if this instance is fortitude good save; otherwise, <c>false</c>.</value>
        public bool IsFortitudeGoodSave
        { 
            get { return this.FortitudeSaveRate == GoodSaveRate; }
        }

        /// <summary>
        /// Gets a value indicating whether this class has a good reflex save.
        /// </summary>
        /// <value><c>true</c> if this instance is reflex good save; otherwise, <c>false</c>.</value>
        public bool IsReflexGoodSave
        {
            get { return this.ReflexSaveRate == GoodSaveRate; }
        }

        /// <summary>
        /// Gets a value indicating whether this class has a good will save.
        /// </summary>
        /// <value><c>true</c> if this instance is will good save; otherwise, <c>false</c>.</value>
        public bool IsWillGoodSave
        {
            get { return this.WillSaveRate == GoodSaveRate; }
        }

        /// <summary>
        /// Determines whether this class treats the skill as a class skills.
        /// </summary>
        /// <returns><c>true</c> if this skill is a class skill; otherwise, <c>false</c>.</returns>
        /// <param name="name">Name of the skill.</param>
        public bool IsClassSkill(string name)
        {
            // Craft, Profession, and Perform are special cases 
            // All skills in that group are considered class skills 
            // in this case 
            //
            // Truncate any skill names like 'Craft (Food)' -> Craft
            // Profession (Farmer) => Profession
            // etc...
            var pattern = "(\\(.*\\))";
            var skillName = Regex.Replace(name, pattern, string.Empty).Trim();
            return this.ClassSkills.Any(x => x == skillName);
        }

        /// <summary>
        /// Adds a class skill.
        /// </summary>
        /// <param name="name">Name of the skill.</param>
        public void AddClassSkill(string name)
        {
            if (!this.IsClassSkill(name))
            {
                this.ClassSkills.Add(name);
            }
            else
            {
                ShortLog.Debug("Not adding class skill as it already is there: " + name);
            }
        }

        public Level GetLevel(int levelNumber)
        {
            var l = Levels.FirstOrDefault(x => x.Number == levelNumber);
            if (l == null) {
                l = new Level(levelNumber);
            }
            return l;
        }

        private void LoadFromObjectStore(IObjectStore data)
        {
            Name = data.GetString("name"); 
            ShortLog.Debug("Loading Class: " + Name);
            SkillPoints = data.GetInteger("skillpoints");
            HitDice = DiceStrings.ParseSides(data.GetString("hitdice"));
            BaseAttackBonusRate = data.GetFloat("baseattackbonus");
            FortitudeSaveRate = data.GetFloat("fortitude");
            ReflexSaveRate = data.GetFloat("reflex");
            WillSaveRate = data.GetFloat("will");
            ClassDevelopmentAge = data.GetEnum<ClassDevelopmentAge>("developedage");

            var armor = data.GetListOptional("armorproficiencies");
            ArmorProficiencies.Add(armor);

            var weapons = data.GetListOptional("weaponproficiencies");
            WeaponProficiencies.Add(weapons);

            // Get the Skills for this class
            var skills = data.GetObject("skills").Children;
            foreach (var s in skills)
            {
                AddClassSkill(s.Value);
            }

            //Load Levels
            var levels = data.GetObjectOptional("levels");
            if (levels != null)
            {
                foreach(var l in levels.Children)
                {
                    var level = new Level(l);
                    Levels.Add(level);
                }
            }
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}