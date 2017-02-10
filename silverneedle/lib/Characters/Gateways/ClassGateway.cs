//-----------------------------------------------------------------------
// <copyright file="ClassYamlGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    /// <summary>
    /// Class yaml gateway.
    /// </summary>
    public class ClassGateway : IClassGateway
    {
        /// <summary>
        /// The yaml file holding class data
        /// </summary>
        private const string ClassDataFileType = "class";

        /// <summary>
        /// The classes that are loaded
        /// </summary>
        private IList<Class> classes = new List<Class>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.ClassYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml data.</param>
        public ClassGateway(IObjectStore dataStore)
        {
            this.classes.Add(LoadObjects(dataStore));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.ClassYamlGateway"/> class.
        /// </summary>
        public ClassGateway()
        {
            // Use DatafileLoader to get all class files;
            var yamlNodes = DatafileLoader.Instance.GetDataFiles(ClassDataFileType);
            foreach(var y in yamlNodes) {
                this.classes.Add(LoadObjects(y));
            }
        }

        /// <summary>
        /// Should return all of the classes
        /// </summary>
        /// <returns>Enumerable collection of the classes</returns>
        public IEnumerable<Class> All()
        {
            return this.classes;
        }

        public Class GetByName(string name) 
        {
            return this.classes.First(
                x => string.Equals(name, x.Name, StringComparison.OrdinalIgnoreCase)
            );
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <returns>The from yaml.</returns>
        /// <param name="yaml">Yaml data to load from.</param>
        private IList<Class> LoadObjects(IObjectStore yaml)
        {
            var classes = new List<Class>();

            foreach (var node in yaml.Children)
            {
                var cls = new Class();
                cls.Name = node.GetString("name"); 
                ShortLog.Debug("Loading Class: " + cls.Name);
                cls.SkillPoints = node.GetInteger("skillpoints");
                cls.HitDice = DiceStrings.ParseSides(node.GetString("hitdice"));
                cls.BaseAttackBonusRate = node.GetFloat("baseattackbonus");
                cls.FortitudeSaveRate = node.GetFloat("fortitude");
                cls.ReflexSaveRate = node.GetFloat("reflex");
                cls.WillSaveRate = node.GetFloat("will");
                cls.ClassDevelopmentAge = node.GetEnum<ClassDevelopmentAge>("developedage");

                var armor = node.GetListOptional("armorproficiencies");
                cls.ArmorProficiencies.Add(armor);

                var weapons = node.GetListOptional("weaponproficiencies");
                cls.WeaponProficiencies.Add(weapons);

                // Get the Skills for this class
                var skills = node.GetObject("skills").Children;
                foreach (var s in skills)
                {
                    cls.AddClassSkill(s.Value);
                }

                //Load Levels
                var levels = node.GetObjectOptional("levels");
                if (levels != null)
                {
                    foreach(var l in levels.Children)
                    {
                        var level = new Level(l);
                        cls.Levels.Add(level);
                    }
                }
                classes.Add(cls);
            }

            return classes;
        }
    }
}