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
    public class DefenseStats : IComponent
    {
        public ComponentContainer Parent { get; set; }

        private const int GoodSaveBaseValue = 2;

        private const string ArmorClassStatName = "Armor Class";

        private const string WillSaveStatName = "Will";

        private const string ReflexSaveStatName = "Reflex";

        private const string FortitudeSaveStatName = "Fortitude";

        public DefenseStats() { }

        public void Initialize(ComponentContainer components)
        {
            var abilities = components.Get<AbilityScores>(); 
            var size = components.Get<SizeStats>();

            this.MaxDexterityBonus.AddModifier( new EquippedArmorMaxDexBonuxModifier(components));

            this.ArmorClass.AddModifiers( new LimitStatModifier(StatNames.ArmorClass, abilities.GetAbility(AbilityScoreTypes.Dexterity), this.MaxDexterityBonus));

            this.TouchArmorClass.AddModifiers( new LimitStatModifier(StatNames.TouchArmorClass, abilities.GetAbility(AbilityScoreTypes.Dexterity), this.MaxDexterityBonus));
        }

        /// <summary>
        /// Gets the armor proficiencies.
        /// </summary>
        /// <value>The armor proficiencies.</value>
        public IEnumerable<ArmorProficiency> ArmorProficiencies
        { 
            get { return this.Parent.GetAll<ArmorProficiency>(); } 
        }

        /// <summary>
        /// Gets the fortitude save
        /// </summary>
        /// <returns>The fortitude save.</returns>
        public BasicStat FortitudeSave { get { return Parent.FindStat<BasicStat>(StatNames.FortitudeSave); } }

        /// <summary>
        /// Gets the reflexs save.
        /// </summary>
        /// <returns>The reflex save.</returns>
        public BasicStat ReflexSave { get { return Parent.FindStat<BasicStat>(StatNames.ReflexSave); } }

        /// <summary>
        /// Gets the will save.
        /// </summary>
        /// <returns>The will save.</returns>
        public BasicStat WillSave { get { return Parent.FindStat<BasicStat>(StatNames.WillSave); } }
        public BasicStat MaxDexterityBonus { get { return Parent.FindStat<BasicStat>(StatNames.MaxDexterityBonus); } }

        public IValueStatistic SpellResistance { get { return Parent.FindStat<IValueStatistic>(StatNames.SpellResistance); } }

        public IEnumerable<IResistance> Immunities
        {
            get 
            { 
                return Parent.GetAll<IResistance>().Where(x => x.IsImmune);
            }
        }

        public IEnumerable<EnergyResistance> EnergyResistance 
        { 
            get 
            { 
                return Parent.GetAll<EnergyResistance>().Where(x => !x.IsImmune);
            } 
        }

        public IEnumerable<DamageReduction> DamageReduction
        {
            get
            {
                return Parent.GetAll<DamageReduction>();
            }
        }

        public BasicStat BaseArmorClass { get; private set; }

        /// <summary>
        /// Gets the Armors class.
        /// </summary>
        /// <returns>The armor class for the character.</returns>
        public IValueStatistic ArmorClass { get { return Parent.FindStat<IValueStatistic>(StatNames.ArmorClass); } }

        /// <summary>
        /// Return the touch armor class.
        /// </summary>
        /// <returns>The touch armor class.</returns>
        public IValueStatistic TouchArmorClass { get { return Parent.FindStat<IValueStatistic>(StatNames.TouchArmorClass); } }

        /// <summary>
        /// The Flat footed armor class.
        /// </summary>
        /// <returns>The flat footed armor class.</returns>
        public IValueStatistic FlatFootedArmorClass { get { return Parent.FindStat<IValueStatistic>(StatNames.FlatFootedArmorClass); } } 

        public void SetFortitudeGoodSave()
        {
            this.FortitudeSave.SetValue(2);
        }

        public void SetReflexGoodSave()
        {
            this.ReflexSave.SetValue(2);
        }

        public void SetWillGoodSave()
        {
            this.WillSave.SetValue(2);
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

            // Add Adjustment for each level
            this.FortitudeSave.AddModifier(new ValueStatModifier(cls.FortitudeSaveRate));
            this.ReflexSave.AddModifier(new ValueStatModifier(cls.ReflexSaveRate));
            this.WillSave.AddModifier(new ValueStatModifier(cls.WillSaveRate));
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
            this.Parent.Add(new ArmorProficiency(prof));
        }

        public void AddDamageResistance(EnergyResistance dr)
        {
            var current = this.EnergyResistance.FirstOrDefault(x => x.DamageType == dr.DamageType);
            if (current == null)
            {
                this.Parent.Add(dr);
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
            this.Parent.Add(new Immunity(immunity));
        }
    }
}
