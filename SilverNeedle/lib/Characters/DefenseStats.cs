//-----------------------------------------------------------------------
// <copyright file="DefenseStats.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;

    /// <summary>
    /// Defense stats manage everything a character does to defend herself.
    /// </summary>
    public class DefenseStats : IStatTracker
    {
        /// <summary>
        /// The base armor class.
        /// </summary>
        private const int BaseArmorClass = 10;

        /// <summary>
        /// The good save base value.
        /// </summary>
        private const int GoodSaveBaseValue = 2;

        /// <summary>
        /// The name of the armor class stat.
        /// </summary>
        private const string ArmorClassStatName = "Armor Class";

        /// <summary>
        /// The name of the will save stat.
        /// </summary>
        private const string WillSaveStatName = "Will";

        /// <summary>
        /// The name of the reflex save stat.
        /// </summary>
        private const string ReflexSaveStatName = "Reflex";

        /// <summary>
        /// The name of the fortitude save stat.
        /// </summary>
        private const string FortitudeSaveStatName = "Fortitude";

        private const string ImmunitiesName = "Immunity";

        private const string DefensiveAbilitiesName = "Defensive";

        /// <summary>
        /// The ability scores to base defense stats off of
        /// </summary>
        private AbilityScores abilities;

        /// <summary>
        /// The size stats of the character
        /// </summary>
        private SizeStats size;

        /// <summary>
        /// The armor proficiencies of the character
        /// </summary>
        private List<ArmorProficiency> armorProficiencies = new List<ArmorProficiency>();

        /// <summary>
        /// Gets or sets the fortitude save.
        /// </summary>
        /// <value>The fortitude save value.</value>
        private BasicStat fortitudeSave;

        /// <summary>
        /// Gets or sets the reflex save stat.
        /// </summary>
        /// <value>The reflex save value.</value>
        private BasicStat reflexSave;

        /// <summary>
        /// Gets or sets the will save.
        /// </summary>
        /// <value>The will save vlue.</value>
        private BasicStat willSave;

        /// <summary>
        /// Gets or sets the inventory of the character to find defensive items
        /// </summary>
        /// <value>The inventory of the character.</value>
        private Inventory inventory;

        /// <summary>
        /// Gets or sets the armor being worn
        /// </summary>
        /// <value>The armor of the character.</value>
        private BasicStat armor;

        private IList<SpecialAbility> specialAbilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.DefenseStats"/> class.
        /// </summary>
        /// <param name="abilityScores">Ability scores of the character.</param>
        /// <param name="size">Size of the character.</param>
        /// <param name="inv">Inventory of the character.</param>
        public DefenseStats(AbilityScores abilityScores, SizeStats size, Inventory inv)
        {
            this.abilities = abilityScores; 
            this.size = size;
            this.inventory = inv;

            this.fortitudeSave = new BasicStat();
            this.fortitudeSave.AddModifier(
                new AbilityStatModifier(abilityScores.GetAbility(AbilityScoreTypes.Constitution)));
            
            this.reflexSave = new BasicStat();
            this.reflexSave.AddModifier(
                new AbilityStatModifier(abilityScores.GetAbility(AbilityScoreTypes.Dexterity)));
            
            this.willSave = new BasicStat();
            this.willSave.AddModifier(
                new AbilityStatModifier(abilityScores.GetAbility(AbilityScoreTypes.Wisdom)));
            
            this.armor = new BasicStat(BaseArmorClass);

            this.specialAbilities = new List<SpecialAbility>();
        }

        /// <summary>
        /// Gets the armor proficiencies.
        /// </summary>
        /// <value>The armor proficiencies.</value>
        public IEnumerable<ArmorProficiency> ArmorProficiencies
        { 
            get { return this.armorProficiencies; } 
        }

        /// <summary>
        /// Gets the fortitude save
        /// </summary>
        /// <returns>The fortitude save.</returns>
        public BasicStat FortitudeSave
        {
            get { return this.fortitudeSave; }
        }

        /// <summary>
        /// Gets the reflexs save.
        /// </summary>
        /// <returns>The reflex save.</returns>
        public BasicStat ReflexSave
        {
            get { return this.reflexSave; }
        }

        /// <summary>
        /// Gets the will save.
        /// </summary>
        /// <returns>The will save.</returns>
        public BasicStat WillSave
        {
            get { return this.willSave; }
        }

        public IEnumerable<SpecialAbility> Immunities
        {
            get { return specialAbilities.Where(x => string.Equals(x.Type, ImmunitiesName)); }
        }

        public IEnumerable<SpecialAbility> DefensiveAbilities
        {
            get
            {
                return specialAbilities.Where(x => string.Equals(x.Type, DefensiveAbilitiesName));
            }
        }

        /// <summary>
        /// Get the equipped armor bonus.
        /// </summary>
        /// <returns>The armor bonus.</returns>
        public int EquippedArmorBonus()
        {
            return this.inventory.EquippedItems.OfType<Armor>().Sum(x => x.ArmorClass);
        }

        /// <summary>
        /// Gets the Armors class.
        /// </summary>
        /// <returns>The armor class for the character.</returns>
        public int ArmorClass()
        {
            return this.armor.TotalValue
            + this.abilities.GetModifier(AbilityScoreTypes.Dexterity)
            + this.size.SizeModifier
            + this.EquippedArmorBonus();
        }

        /// <summary>
        /// Return the touch armor class.
        /// </summary>
        /// <returns>The touch armor class.</returns>
        public int TouchArmorClass()
        {
            return BaseArmorClass
            + this.abilities.GetModifier(AbilityScoreTypes.Dexterity)
            + this.size.SizeModifier;
        }

        /// <summary>
        /// The Flat footed armor class.
        /// </summary>
        /// <returns>The flat footed armor class.</returns>
        public int FlatFootedArmorClass()
        {
            return BaseArmorClass
            + this.size.SizeModifier
            + this.EquippedArmorBonus();
        }

        /// <summary>
        /// Sets the fortitude save is a good save.
        /// </summary>
        public void SetFortitudeGoodSave()
        {
            this.fortitudeSave.SetValue(GoodSaveBaseValue);
        }

        /// <summary>
        /// Sets the reflex save is a good save.
        /// </summary>
        public void SetReflexGoodSave()
        {
            this.reflexSave.SetValue(GoodSaveBaseValue);
        }

        /// <summary>
        /// Sets the will save is a good save.
        /// </summary>
        public void SetWillGoodSave()
        {
            this.willSave.SetValue(GoodSaveBaseValue);
        }
            
        /// <summary>
        /// Levels up defense stats.
        /// </summary>
        /// <param name="cls">The class to use for the level up stat.</param>
        public void LevelUpDefenseStats(Class cls)
        {
            // Mark any good saves
            if (cls.IsFortitudeGoodSave)
            {
                this.SetFortitudeGoodSave();
            }

            if (cls.IsReflexGoodSave)
            {
                this.SetReflexGoodSave();
            }

            if (cls.IsWillGoodSave)
            {
                this.SetWillGoodSave();
            }

            var reason = string.Format("LEVEL UP ({0})", cls.Name);

            // Add Adjustment for each level
            this.fortitudeSave.AddModifier(new BasicStatModifier(cls.FortitudeSaveRate, reason));
            this.reflexSave.AddModifier(new BasicStatModifier(cls.ReflexSaveRate, reason));
            this.willSave.AddModifier(new BasicStatModifier(cls.WillSaveRate, reason));
        }

        /// <summary>
        /// The implementing class must handle modifiers to stats under its control
        /// </summary>
        /// <param name="modifier">Modifier for stats</param>
        public void ProcessModifier(IModifiesStats modifier)
        {
            foreach (var s in modifier.Modifiers)
            {
                switch (s.StatisticName)
                {
                    case ArmorClassStatName:
                        this.armor.AddModifier(s);
                        break;
                    case FortitudeSaveStatName:
                        this.fortitudeSave.AddModifier(s);
                        break;
                    case ReflexSaveStatName:
                        this.reflexSave.AddModifier(s);
                        break;
                    case WillSaveStatName:
                        this.willSave.AddModifier(s);
                        break;
                }
            }


        }

        public void ProcessSpecialAbilities(IProvidesSpecialAbilities abilities)
        {
            // Grab any special abilities that are relevant
            foreach (var ability in abilities.SpecialAbilities)
            {
                switch (ability.Type)
                {
                    case ImmunitiesName:
                    case DefensiveAbilitiesName:
                        this.specialAbilities.Add(ability);
                        break;
                    
                }
            }
        }

        /// <summary>
        /// Adds the armor proficiencies.
        /// </summary>
        /// <param name="proficiencies">Proficiencies to add.</param>
        public void AddArmorProficiencies(IEnumerable<string> proficiencies)
        {
            foreach (var a in proficiencies)
            {
                this.AddArmorProficiency(a);
            }
        }

        /// <summary>
        /// Adds an armor proficiency to the defense stats
        /// </summary>
        /// <param name="prof">Proficiency to add.</param>
        public void AddArmorProficiency(string prof)
        {
            this.armorProficiencies.Add(new ArmorProficiency(prof));
        }

        /// <summary>
        /// Determines whether this instance is proficient with the specified armor.
        /// </summary>
        /// <returns><c>true</c> if this instance is proficient the specified armor; otherwise, <c>false</c>.</returns>
        /// <param name="armor">Armor to add proficiency.</param>
        public bool IsProficient(Armor armor)
        {
            return this.ArmorProficiencies.IsProficient(armor);
        }
    }
}
