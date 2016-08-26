//-----------------------------------------------------------------------
// <copyright file="WeaponYamlGateway.cs" company="Short Leg Studio, LLC">
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
    /// Weapon yaml gateway.
    /// </summary>
    public class WeaponYamlGateway : IWeaponGateway
    {
        /// <summary>
        /// The weapon yaml file.
        /// </summary>
        private const string WeaponYamlFile = "Data/weapons.yml";

        /// <summary>
        /// The weapons.
        /// </summary>
        private IList<Weapon> weapons;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Gateways.WeaponYamlGateway"/> class.
        /// </summary>
        public WeaponYamlGateway()
        { 
            this.LoadFromYaml(FileHelper.OpenYaml(WeaponYamlFile));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Gateways.WeaponYamlGateway"/> class.
        /// </summary>
        /// <param name="yamlData">Yaml data.</param>
        public WeaponYamlGateway(YamlNodeWrapper yamlData)
        { 
            this.LoadFromYaml(yamlData);
        }

        /// <summary>
        /// All this instance.
        /// </summary>
        /// <returns>All weapons loaded</returns>
        public IEnumerable<Weapon> All()
        {
            return this.weapons;
        }

        /// <summary>
        /// Finds weapons by proficiencies.
        /// </summary>
        /// <returns>The by proficient.</returns>
        /// <param name="proficiencies">Proficiencies to find by.</param>
        public IEnumerable<Weapon> FindByProficient(IEnumerable<WeaponProficiency> proficiencies)
        {
            return this.weapons.Where(x => proficiencies.IsProficient(x));
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <param name="yaml">Yaml data to load.</param>
        private void LoadFromYaml(YamlNodeWrapper yaml)
        {
            this.weapons = new List<Weapon>();

            foreach (var node in yaml.Children())
            {
                ShortLog.DebugFormat("Loading Weapon: {0}", node.GetString("name"));
                var w = new Weapon(
                    node.GetString("name"),
                    node.GetFloat("weight"),
                    node.GetString("damage"),
                    node.GetEnum<DamageTypes>("damage_type"),
                    node.GetIntegerOptional("critical_threat"),
                    node.GetIntegerOptional("critical_modifier"),
                    node.GetIntegerOptional("range"),
                    node.GetEnum<WeaponType>("type"),
                    node.GetEnum<WeaponGroup>("group"),
                    node.GetEnum<WeaponTrainingLevel>("training_level"));
                this.weapons.Add(w);
            }
        }
    }
}