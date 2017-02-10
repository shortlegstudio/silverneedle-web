//-----------------------------------------------------------------------
// <copyright file="WeaponYamlGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Equipment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    /// <summary>
    /// Weapon yaml gateway.
    /// </summary>
    public class WeaponGateway : IWeaponGateway
    {
        /// <summary>
        /// The weapon yaml file.
        /// </summary>
        private const string WeaponDataType = "weapon";

        /// <summary>
        /// The weapons.
        /// </summary>
        private IList<Weapon> weapons = new List<Weapon>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Gateways.WeaponYamlGateway"/> class.
        /// </summary>
        public WeaponGateway()
        { 
            foreach(var y in DatafileLoader.Instance.GetDataFiles(WeaponDataType))
            {
                this.LoadObjects(y);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Gateways.WeaponYamlGateway"/> class.
        /// </summary>
        /// <param name="yamlData">Yaml data.</param>
        public WeaponGateway(IObjectStore data)
        { 
            this.LoadObjects(data);
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
        /// Loads from Data Store.
        /// </summary>
        /// <param name="dataStore">Data to load.</param>
        private void LoadObjects(IObjectStore dataStore)
        {
            foreach (var node in dataStore.Children)
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