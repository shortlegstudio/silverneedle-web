//-----------------------------------------------------------------------
// <copyright file="ClassYamlGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle;
    using SilverNeedle.Dice;
    using SilverNeedle.Characters;
    using SilverNeedle.Yaml;

    /// <summary>
    /// Class yaml gateway.
    /// </summary>
    public class ClassYamlGateway : IEntityGateway<Class>
    {
        /// <summary>
        /// The yaml file holding class data
        /// </summary>
        private const string ClassDataFile = "Data/classes.yml";

        /// <summary>
        /// The classes that are loaded
        /// </summary>
        private IList<Class> classes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.ClassYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml data.</param>
        public ClassYamlGateway(YamlNodeWrapper yaml)
        {
            this.classes = LoadFromYaml(yaml);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.ClassYamlGateway"/> class.
        /// </summary>
        public ClassYamlGateway()
        {
            this.classes = LoadFromYaml(FileHelper.OpenYaml(ClassDataFile));
        }

        /// <summary>
        /// Should return all of the classes
        /// </summary>
        /// <returns>Enumerable collection of the classes</returns>
        public IEnumerable<Class> All()
        {
            return this.classes;
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <returns>The from yaml.</returns>
        /// <param name="yaml">Yaml data to load from.</param>
        private static IList<Class> LoadFromYaml(YamlNodeWrapper yaml)
        {
            var classes = new List<Class>();

            foreach (var node in yaml.Children())
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

                var armor = node.GetCommaStringOptional("armorproficiencies");
                cls.ArmorProficiencies.Add(armor);

                var weapons = node.GetCommaStringOptional("weaponproficiencies");
                cls.WeaponProficiencies.Add(weapons);

                // Get the Skills for this class
                var skills = node.GetNode("skills").Children();
                foreach (var s in skills)
                {
                    cls.AddClassSkill(s.Value);
                }

                classes.Add(cls);
            }

            return classes;
        }
    }
}