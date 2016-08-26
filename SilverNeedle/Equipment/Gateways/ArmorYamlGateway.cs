//-----------------------------------------------------------------------
// <copyright file="ArmorYamlGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Equipment.Gateways
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Yaml;

    /// <summary>
    /// Armor yaml gateway.
    /// </summary>
    public class ArmorYamlGateway : IArmorGateway
    {
        /// <summary>
        /// The armor yaml file.
        /// </summary>
        private const string ArmorYamlFile = "Data/Armors.yml";

        /// <summary>
        /// The armors that are loaded.
        /// </summary>
        private IList<Armor> armors;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Gateways.ArmorYamlGateway"/> class.
        /// </summary>
        public ArmorYamlGateway()
        {
            this.LoadFromYaml(FileHelper.OpenYaml(ArmorYamlFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Gateways.ArmorYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml to parse</param>
        public ArmorYamlGateway(YamlNodeWrapper yaml)
        {
            this.LoadFromYaml(yaml);
        }

        /// <summary>
        /// All the armors loaded
        /// </summary>
        /// <returns>All armors</returns>
        public IEnumerable<Armor> All()
        {
            return this.armors;
        }

        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <returns>The by name.</returns>
        /// <param name="name">Name of the armor to find.</param>
        public Armor GetByName(string name)
        {
            return this.armors.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Finds the type of the by armor.
        /// </summary>
        /// <returns>The armor by type.</returns>
        /// <param name="type">Type of armor.</param>
        public IEnumerable<Armor> FindByArmorType(ArmorType type)
        {
            return this.armors.Where(x => x.ArmorType == type);
        }

        /// <summary>
        /// Finds the by armor types.
        /// </summary>
        /// <returns>The armors matching types.</returns>
        /// <param name="types">Types of armor.</param>
        public IEnumerable<Armor> FindByArmorTypes(params ArmorType[] types)
        {
            return this.armors.Where(
                x => types.Contains(x.ArmorType));
        }

        /// <summary>
        /// Finds armors by proficiency.
        /// </summary>
        /// <returns>Armors that are valid for proficiency</returns>
        /// <param name="proficiencies">The proficiencies to search by.</param>
        public IEnumerable<Armor> FindByProficiency(IEnumerable<SilverNeedle.Characters.ArmorProficiency> proficiencies)
        {
            return this.armors.Where(
                x => proficiencies.IsProficient(x));
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <param name="yaml">Yaml to parse.</param>
        private void LoadFromYaml(YamlNodeWrapper yaml)
        {
            this.armors = new List<Armor>();

            foreach (var node in yaml.Children())
            {
                ShortLog.DebugFormat("Loading Armor: {0}", node.GetString("name"));
                var armor = new Armor(
                    node.GetString("name"),
                    node.GetInteger("armor_class"),
                    node.GetFloat("weight"),
                    node.GetInteger("maximum_dexterity_bonus"),
                    node.GetInteger("armor_check_penalty"),
                    node.GetInteger("arcane_spell_failure_chance"),
                    node.GetEnum<ArmorType>("armor_type"));

                this.armors.Add(armor);
            }
        }
    }
}