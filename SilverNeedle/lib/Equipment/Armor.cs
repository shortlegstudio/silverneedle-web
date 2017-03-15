// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using SilverNeedle.Treasure;
    using SilverNeedle.Utility;

    /// <summary>
    /// Armor is for protecting the character from damage
    /// </summary>
    public class Armor : IInventoryItem, IGatewayObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Armor"/> class.
        /// </summary>
        public Armor()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Equipment.Armor"/> class.
        /// </summary>
        /// <param name="name">Name of armor.</param>
        /// <param name="armorClass">Armor Class.</param>
        /// <param name="weight">Weight in pounds.</param>
        /// <param name="maxDexBonus">Max dexterity bonus.</param>
        /// <param name="armorCheckPenalty">Armor check penalty.</param>
        /// <param name="arcaneSpell">Arcane spell.</param>
        /// <param name="armorType">Armor type.</param>
        public Armor(
            string name,
            int armorClass,
            float weight,
            int maxDexBonus,
            int armorCheckPenalty,
            int arcaneSpell,
            ArmorType armorType)
        {
            this.Name = name;
            this.ArmorClass = armorClass;
            this.Weight = weight;
            this.MaximumDexterityBonus = maxDexBonus;
            this.ArmorCheckPenalty = armorCheckPenalty;
            this.ArcaneSpellFailureChance = arcaneSpell;
            this.ArmorType = armorType;
        }

        public Armor(IObjectStore data)
        {
            ShortLog.DebugFormat("Loading Armor: {0}", data.GetString("name"));
            this.Name = data.GetString("name");
            this.ArmorClass = data.GetInteger("armor_class");
            this.Weight = data.GetFloat("weight");
            this.MaximumDexterityBonus = data.GetInteger("maximum_dexterity_bonus");
            this.ArmorCheckPenalty = data.GetInteger("armor_check_penalty");
            this.ArcaneSpellFailureChance = data.GetInteger("arcane_spell_failure_chance");
            this.ArmorType = data.GetEnum<ArmorType>("armor_type");
            this.Value = data.GetString("cost").ToCoinValue();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the armor.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>The weight of the armor.</value>
        public float Weight { get; set; }

        /// <summary>
        /// Gets or sets the armor class.
        /// </summary>
        /// <value>The armor class of the armor.</value>
        public int ArmorClass { get; set; }

        /// <summary>
        /// Gets or sets the maximum dexterity bonus.
        /// </summary>
        /// <value>The maximum dexterity bonus.</value>
        public int MaximumDexterityBonus { get; set; }

        /// <summary>
        /// Gets or sets the armor check penalty.
        /// </summary>
        /// <value>The armor check penalty.</value>
        public int ArmorCheckPenalty { get; set; }

        /// <summary>
        /// Gets or sets the arcane spell failure chance.
        /// </summary>
        /// <value>The arcane spell failure chance.</value>
        public int ArcaneSpellFailureChance { get; set; }

        /// <summary>
        /// Gets or sets the type of the armor.
        /// </summary>
        /// <value>The type of the armor.</value>
        public ArmorType ArmorType { get; set; }

        public int Value { get; set; }
        
        public int MovementSpeedPenalty {
            get {
                switch(ArmorType) {
                    case ArmorType.Medium:
                    case ArmorType.Heavy:
                        return 10;
                    default: 
                        return 0;
                }
            }
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}