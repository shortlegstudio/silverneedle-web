// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using SilverNeedle.Treasure;
    using SilverNeedle.Serialization;

    /// <summary>
    /// Represents a weapon 
    /// </summary>
    public class Weapon : IWeapon, IGatewayObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Weapon"/> class.
        /// </summary>
        public Weapon()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Weapon"/> class.
        /// </summary>
        /// <param name="name">Name of the weapon.</param>
        /// <param name="weight">Weight of the weapon.</param>
        /// <param name="damage">Damage represented as dice (1d4, 2d6, 1d12).</param>
        /// <param name="damageType">Damage type.</param>
        /// <param name="critThreat">Crit threat range.</param>
        /// <param name="critMod">Crit mod.</param>
        /// <param name="range">Range in feet.</param>
        /// <param name="type">Type of weapon.</param>
        /// <param name="group">Group of weapon.</param>
        /// <param name="level">Level of weapon (martial, simple, exotic).</param>
        public Weapon(
            string name,
            float weight,
            string damage,
            DamageTypes damageType,
            int critThreat,
            int critMod,
            int range,
            WeaponType type,
            WeaponGroup group,
            WeaponTrainingLevel level)
        {
            this.Name = name;
            this.Weight = weight;
            this.Damage = damage;
            this.DamageType = damageType;
            this.CriticalThreat = critThreat == 0 ? 20 : critThreat;
            this.CriticalModifier = critMod == 0 ? 2 : critMod; 
            this.Range = range;
            this.Type = type;
            this.Group = group;
            this.Level = level;            
        }

        public Weapon(IObjectStore data)
        {
            ShortLog.DebugFormat("Loading Weapon: {0}", data.GetString("name"));
            this.Name = data.GetString("name");
            this.Weight = data.GetFloat("weight");
            this.Damage = data.GetString("damage");
            this.DamageType= data.GetEnum<DamageTypes>("damage_type");
            this.CriticalThreat = data.GetIntegerOptional("critical_threat");
            this.CriticalModifier = data.GetIntegerOptional("critical_modifier");
            this.Range = data.GetIntegerOptional("range");
            this.Type = data.GetEnum<WeaponType>("type");
            this.Group = data.GetEnum<WeaponGroup>("group");
            this.Level = data.GetEnum<WeaponTrainingLevel>("training_level");
            this.Value = data.GetStringOptional("cost").ToCoinValue();
            this.CriticalThreat = this.CriticalThreat == 0 ? 20 : this.CriticalThreat;
            this.CriticalModifier = this.CriticalModifier == 0 ? 2 : this.CriticalModifier; 
            

        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the equipment.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight of the equipment.</value>
        public float Weight { get; set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        /// <value>The damage.</value>
        public string Damage { get; set; }

        /// <summary>
        /// Gets or sets the type of the damage.
        /// </summary>
        /// <value>The type of the damage.</value>
        public DamageTypes DamageType { get; set; }

        /// <summary>
        /// Gets or sets the critical threat.
        /// </summary>
        /// <value>The critical threat.</value>
        public int CriticalThreat { get; set; }

        /// <summary>
        /// Gets or sets the critical modifier.
        /// </summary>
        /// <value>The critical modifier.</value>
        public int CriticalModifier { get; set; }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        /// <value>The range.</value>
        public int Range { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public WeaponType Type { get; set; }

        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        public WeaponGroup Group { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public WeaponTrainingLevel Level { get; set; }

        public int Value { get; set; }

        public bool GroupSimilar { get { return false; } }

        public int AttackModifier { get; set; }

        public string ProficiencyName { get { return Name; } }

        /// <summary>
        /// Gets a value indicating whether this instance is ranged.
        /// </summary>
        /// <value><c>true</c> if this instance is ranged; otherwise, <c>false</c>.</value>
        public bool IsRanged
        {
            get { return this.Type == WeaponType.Ranged || this.Range > 0; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is melee.
        /// </summary>
        /// <value><c>true</c> if this instance is melee; otherwise, <c>false</c>.</value>
        public bool IsMelee
        {
            get { return this.Type != WeaponType.Ranged; }
        }

        /// <summary>
        /// Returns either "Melee" or "Ranged"
        /// </summary>
        /// <returns>The basic type.</returns>
        public string GetBasicType()
        {
            return this.Type == WeaponType.Ranged ? "Ranged" : "Melee";
        }

         public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}