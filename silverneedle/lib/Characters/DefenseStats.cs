// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    /// <summary>
    /// Defense stats manage everything a character does to defend herself.
    /// </summary>
    public class DefenseStats : IStatTracker, IComponent
    {
        public IEnumerable<IStatistic> Statistics 
        { 
            get 
            { 
                return new IStatistic[] { 
                    BaseArmorClass,
                    ArmorClass, 
                    TouchArmorClass, 
                    FlatFootedArmorClass, 
                    FortitudeSave, 
                    ReflexSave, 
                    WillSave, 
                    MaxDexterityBonus,
                    SpellResistance
                }; 
            } 
        }

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


        private ComponentContainer components;

        /// <summary>
        /// The armor proficiencies of the character
        /// </summary>
        private List<ArmorProficiency> armorProficiencies = new List<ArmorProficiency>();

        private Inventory inventory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.DefenseStats"/> class.
        /// </summary>
        /// <param name="abilityScores">Ability scores of the character.</param>
        /// <param name="size">Size of the character.</param>
        /// <param name="inv">Inventory of the character.</param>
        public DefenseStats()
        {
            this.FortitudeSave = new BasicStat(StatNames.FortitudeSave);
            this.ReflexSave = new BasicStat(StatNames.ReflexSave);
            this.WillSave = new BasicStat(StatNames.WillSave);
            this.BaseArmorClass = new BasicStat(StatNames.BaseArmorClass, 10);
            this.ArmorClass = new BasicStat(StatNames.ArmorClass);
            this.ArmorClass.UseModifierString = false;
            this.TouchArmorClass = new BasicStat(StatNames.TouchArmorClass);
            this.TouchArmorClass.UseModifierString = false;
            this.FlatFootedArmorClass = new BasicStat(StatNames.FlatFootedArmorClass);
            this.FlatFootedArmorClass.UseModifierString = false;
            this.MaxDexterityBonus = new BasicStat(StatNames.MaxDexterityBonus);
            this.SpellResistance = new BasicStat(StatNames.SpellResistance);
        }

        public void Initialize(ComponentContainer components)
        {
            this.components = components;
            var abilities = components.Get<AbilityScores>(); 
            var size = components.Get<SizeStats>();
            this.inventory = components.Get<Inventory>();

            this.FortitudeSave.AddModifier(
                new AbilityStatModifier(abilities.GetAbility(AbilityScoreTypes.Constitution)));
            
            this.ReflexSave.AddModifier(
                new AbilityStatModifier(abilities.GetAbility(AbilityScoreTypes.Dexterity)));
            
            this.WillSave.AddModifier(
                new AbilityStatModifier(abilities.GetAbility(AbilityScoreTypes.Wisdom)));

            this.MaxDexterityBonus.AddModifier(
                new EquippedArmorMaxDexBonuxModifier(components)
            );

            this.ArmorClass.AddModifiers(
                new StatisticStatModifier(StatNames.ArmorClass, this.BaseArmorClass),
                new LimitStatModifier(abilities.GetAbility(AbilityScoreTypes.Dexterity), this.MaxDexterityBonus),
                size.PositiveSizeModifier,
                new EquippedArmorClassModifier(components)
            );

            this.TouchArmorClass.AddModifiers(
                new StatisticStatModifier(StatNames.TouchArmorClass, this.BaseArmorClass),
                new LimitStatModifier(abilities.GetAbility(AbilityScoreTypes.Dexterity), this.MaxDexterityBonus),
                size.PositiveSizeModifier
            );

            this.FlatFootedArmorClass.AddModifiers(
                new StatisticStatModifier(StatNames.FlatFootedArmorClass, this.BaseArmorClass),
                size.PositiveSizeModifier, 
                new EquippedArmorClassModifier(components)
            );
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
        public BasicStat FortitudeSave { get; private set; }

        /// <summary>
        /// Gets the reflexs save.
        /// </summary>
        /// <returns>The reflex save.</returns>
        public BasicStat ReflexSave { get; private set; }

        /// <summary>
        /// Gets the will save.
        /// </summary>
        /// <returns>The will save.</returns>
        public BasicStat WillSave { get; private set; }
        public BasicStat MaxDexterityBonus { get; private set; }

        public IStatistic SpellResistance { get; private set; }

        public IEnumerable<IResistance> Immunities
        {
            get 
            { 
                return components.GetAll<IResistance>().Where(x => x.IsImmune);
            }
        }

        public IEnumerable<EnergyResistance> EnergyResistance 
        { 
            get 
            { 
                return components.GetAll<EnergyResistance>().Where(x => !x.IsImmune);
            } 
        }

        public IEnumerable<DamageReduction> DamageReduction
        {
            get
            {
                return components.GetAll<DamageReduction>();
            }
        }

        public BasicStat BaseArmorClass { get; private set; }

        /// <summary>
        /// Gets the Armors class.
        /// </summary>
        /// <returns>The armor class for the character.</returns>
        public BasicStat ArmorClass { get; private set; }

        /// <summary>
        /// Return the touch armor class.
        /// </summary>
        /// <returns>The touch armor class.</returns>
        public BasicStat TouchArmorClass { get; private set; }

        /// <summary>
        /// The Flat footed armor class.
        /// </summary>
        /// <returns>The flat footed armor class.</returns>
        public BasicStat FlatFootedArmorClass { get; private set; }

        /// <summary>
        /// Sets the fortitude save is a good save.
        /// </summary>
        public void SetFortitudeGoodSave()
        {
            this.FortitudeSave.SetValue(GoodSaveBaseValue);
        }

        /// <summary>
        /// Sets the reflex save is a good save.
        /// </summary>
        public void SetReflexGoodSave()
        {
            this.ReflexSave.SetValue(GoodSaveBaseValue);
        }

        /// <summary>
        /// Sets the will save is a good save.
        /// </summary>
        public void SetWillGoodSave()
        {
            this.WillSave.SetValue(GoodSaveBaseValue);
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
            this.FortitudeSave.AddModifier(new ValueStatModifier(cls.FortitudeSaveRate, reason));
            this.ReflexSave.AddModifier(new ValueStatModifier(cls.ReflexSaveRate, reason));
            this.WillSave.AddModifier(new ValueStatModifier(cls.WillSaveRate, reason));
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
                        this.ArmorClass.AddModifier(s);
                        break;
                    case FortitudeSaveStatName:
                        this.FortitudeSave.AddModifier(s);
                        break;
                    case ReflexSaveStatName:
                        this.ReflexSave.AddModifier(s);
                        break;
                    case WillSaveStatName:
                        this.WillSave.AddModifier(s);
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

        public void AddDamageResistance(EnergyResistance dr)
        {
            var current = this.EnergyResistance.FirstOrDefault(x => x.DamageType == dr.DamageType);
            if (current == null)
            {
                this.components.Add(dr);
            }
            else
            {
                current.Amount += dr.Amount;
            }
        
        }

        /// <summary>
        /// Determines whether this instance is proficient with the specified armor.
        /// </summary>
        /// <returns><c>true</c> if this instance is proficient the specified armor; otherwise, <c>false</c>.</returns>
        /// <param name="armor">Armor to add proficiency.</param>
        public bool IsProficient(IArmor armor)
        {
            return this.ArmorProficiencies.IsProficient(armor);
        }

        public void AddImmunity(string immunity)
        {
            this.components.Add(new Immunity(immunity));
        }
    }
}
