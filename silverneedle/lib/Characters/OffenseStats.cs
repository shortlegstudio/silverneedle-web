// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Dice;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    /// <summary>
    /// Offense stats for a character. Keeps track of attacks and abilities
    /// for attacking
    /// </summary>
    public class OffenseStats : IStatTracker, IComponent
    {
        public ComponentContainer Parent { get; set; }
        /// <summary>
        /// The unproficient weapon modifier.
        /// </summary>
        public const int UnproficientWeaponModifier = -4;

        /// <summary>
        /// The name of the combat manuever defense stat.
        /// </summary>
        private const string CombatManeuverDefenseStatName = "CMD";   // TODO: Does this belong in Defense Stats?

        /// <summary>
        /// The name of the combat maneuver bonus stat.
        /// </summary>
        private const string CombatManeuverBonusStatName = "CMB";

        public IEnumerable<IValueStatistic> Statistics
        {
            get 
            {
                return new IValueStatistic[] { BaseAttackBonus, CombatManeuverDefense, CombatManeuverBonus };
            }
        }

        /// <summary>
        /// Gets or sets the CombatManeuverDefense.
        /// </summary>
        public BasicStat CombatManeuverDefense { get; private set; }

        /// <summary>
        /// Gets or sets the combat maneuver bonus.
        /// </summary>
        public BasicStat CombatManeuverBonus { get; private set; }

        public IValueStatistic AttacksOfOpportunity { get { return components.FindStat<IValueStatistic>(StatNames.AttacksOfOpportunity); } }

        /// <summary>
        /// The inventory for the character.
        /// </summary>
        private Inventory inventory;

        private ComponentContainer components;

        public IEnumerable<IWeaponModifier> WeaponModifiers { get { return components.GetAll<IWeaponModifier>(); } }

        

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.OffenseStats"/> class.
        /// </summary>
        /// <param name="size">Size of the character.</param>
        /// <param name="inventory">Inventory and gear of the character.</param>
        public OffenseStats()
        {
            this.BaseAttackBonus = new BaseAttackBonus();
            this.CombatManeuverDefense = new BasicStat(StatNames.CMD, 10);
            this.CombatManeuverBonus = new BasicStat(StatNames.CMB);
        }

        public void Initialize(ComponentContainer components)
        {
            this.components = components;
            var abilities = components.Get<AbilityScores>();
            this.Strength = abilities.GetAbility(AbilityScoreTypes.Strength);
            this.Dexterity = abilities.GetAbility(AbilityScoreTypes.Dexterity);
            var size = components.Get<SizeStats>();
            this.Size = size;
            this.inventory = components.Get<Inventory>();
            this.MeleeAttackBonus = components.Get<MeleeAttackBonus>();
            this.RangeAttackBonus = components.Get<RangeAttackBonus>();

            this.CombatManeuverBonus.AddModifiers(
                new StatisticStatModifier(StatNames.CMB, this.BaseAttackBonus),
                new StatisticStatModifier(StatNames.CMB, this.Strength.ModifierStat),
                size.NegativeSizeModifier
            );

            this.CombatManeuverDefense.AddModifiers(
                new StatisticStatModifier(StatNames.CMB, this.BaseAttackBonus),
                new AbilityStatModifier(this.Strength),
                new AbilityStatModifier(this.Dexterity),
                size.NegativeSizeModifier
            );
        }

        /// <summary>
        /// Gets the weapon proficiencies.
        /// </summary>
        /// <value>The weapon proficiencies for the character.</value>
        public IEnumerable<WeaponProficiency> WeaponProficiencies { get { return components.GetAll<WeaponProficiency>(); } }

        /// <summary>
        /// Gets the base attack bonus.
        /// </summary>
        /// <value>The base attack bonus.</value>
        public BaseAttackBonus BaseAttackBonus { get; private set; }

        /// <summary>
        /// Gets or sets the ability scores.
        /// </summary>
        /// <value>The ability scores.</value>
        private AbilityScore Dexterity { get; set; }
        private AbilityScore Strength { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        private SizeStats Size { get; set; }

        /// <summary>
        /// Calculates the melee attack bonus.
        /// </summary>
        /// <returns>The attack bonus.</returns>
        public IValueStatistic MeleeAttackBonus { get; private set; }

        /// <summary>
        /// Calculates the range attack bonus.
        /// </summary>
        /// <returns>The attack bonus.</returns>
        public IValueStatistic RangeAttackBonus { get; private set; }

        /// <summary>
        /// The implementing class must handle modifiers to stats under its control
        /// </summary>
        /// <param name="modifier">Modifier for stats</param>
        public void ProcessModifier(IModifiesStats modifier)
        {
            foreach (var m in modifier.Modifiers)
            {
                switch (m.StatisticName)
                {
                    case CombatManeuverDefenseStatName:
                        this.CombatManeuverDefense.AddModifier(m);
                        break;
                    case CombatManeuverBonusStatName:
                        this.CombatManeuverBonus.AddModifier(m);
                        break;
                }
            }
        }

        /// <summary>
        /// Adds a weapon proficiencies.
        /// </summary>
        /// <param name="proficiencies">Proficiencies to add</param>
        public void AddWeaponProficiencies(IEnumerable<string> proficiencies)
        {
            foreach (var p in proficiencies)
            {
                this.AddWeaponProficiency(p);
            }
        }

        /// <summary>
        /// Adds the weapon proficiency.
        /// </summary>
        /// <param name="proficiency">Proficiency to add.</param>
        public void AddWeaponProficiency(string proficiency)
        {
            this.components.Add(new WeaponProficiency(proficiency));
        }

        /// <summary>
        /// Determines whether this instance is proficient the specified weapon.
        /// </summary>
        /// <returns><c>true</c> if this instance is proficient in the specified weapon; otherwise, <c>false</c>.</returns>
        /// <param name="weapon">Weapon to check.</param>
        public bool IsProficient(IWeaponAttackStatistics weapon)
        {
            return this.WeaponProficiencies.IsProficient(weapon);
        }

        public void AddWeaponModifier(IWeaponModifier modifier)
        {
            this.components.Add(modifier);
        }

        public IEnumerable<WeaponAttack> GetWeaponAttacks()
        {
            var attacks = new List<WeaponAttack>();
            // Get all the melee weapons
            foreach (var weapon in this.inventory.Weapons.Where(x => x.IsMelee))
            {
                attacks.Add(
                    CreateAttack(AttackTypes.Melee, weapon)
                );
            }

            // Get all the ranged weapons
            foreach (var weapon in this.inventory.Weapons.Where(x => x.IsRanged))
            {
                attacks.Add(
                    CreateAttack(AttackTypes.Ranged, weapon)
                );
            }
            return attacks;
        }

        /// <summary>
        /// Calculates what attacks are available to this instance
        /// </summary>
        /// <returns>List of attacks that are available</returns>
        public IEnumerable<IAttack> Attacks()
        {
            var attacks = new List<IAttack>();
            attacks.AddRange(GetWeaponAttacks());
            attacks.AddRange(GetSpecialAttacks());
            return attacks;
        }

        public WeaponAttack GetAttack(IWeapon weapon)
        {
            return Attacks().OfType<WeaponAttack>().First(x => x.Weapon == weapon);
        }
        public void LevelUp(Class characterClass)
        {
            BaseAttackBonus.AddModifier(new ValueStatModifier(characterClass.BaseAttackBonusRate));            
        }

        private IEnumerable<IAttack> GetSpecialAttacks()
        {
            return this.components.GetAll<IAttack>();
        }
        private WeaponAttack CreateAttack(AttackTypes attackType, IWeapon weapon) 
        {
            WeaponAttack atk = null;
            
            // Figure out to apply damage modifier
            if (attackType == AttackTypes.Melee)
            {
                atk = new MeleeAttack(this, this.Strength, this.Size.Size, weapon);
            }
            else if(attackType == AttackTypes.Ranged)
            {
                atk = new RangeAttack(this, this.Size.Size, weapon);
            }
            
            return atk;
        }
    }
}