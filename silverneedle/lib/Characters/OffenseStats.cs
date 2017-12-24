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

        private const string OffensiveAbilitiesName = "Offensive";

        public IEnumerable<IStatistic> Statistics
        {
            get 
            {
                return new IStatistic[] { BaseAttackBonus, CombatManeuverDefense, CombatManeuverBonus };
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

        /// <summary>
        /// The inventory for the character.
        /// </summary>
        private Inventory inventory;

        private List<SpecialAbility> offensiveAbilities;

        private List<IWeaponModifier> weaponModifiers;
        private ComponentContainer components;

        public IEnumerable<IWeaponModifier> WeaponModifiers { get { return weaponModifiers; } }

        

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
            this.offensiveAbilities = new List<SpecialAbility>();
            this.weaponModifiers = new List<IWeaponModifier>();
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
                new AbilityStatModifier(this.Strength),
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

        public IEnumerable<SpecialAbility> OffensiveAbilities
        {
            get
            {
                return offensiveAbilities;
            }
        }

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
        public IStatistic MeleeAttackBonus { get; private set; }

        /// <summary>
        /// Calculates the range attack bonus.
        /// </summary>
        /// <returns>The attack bonus.</returns>
        public IStatistic RangeAttackBonus { get; private set; }

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

        public void ProcessSpecialAbilities(IProvidesSpecialAbilities abilities)
        {
            foreach (var ability in abilities.SpecialAbilities)
            {
                switch (ability.Type)
                {
                    case OffensiveAbilitiesName:
                        offensiveAbilities.Add(ability);
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
            this.weaponModifiers.Add(modifier);
        }

        /// <summary>
        /// Calculates what attacks are available to this instance
        /// </summary>
        /// <returns>List of attacks that are available</returns>
        public IList<IAttackStatistic> Attacks()
        {
            var attacks = new List<IAttackStatistic>();
            
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

            attacks.AddRange(GetSpecialAttacks());

            return attacks;
        }

        public WeaponAttack GetAttack(IWeapon weapon)
        {
            return Attacks().OfType<WeaponAttack>().First(x => x.Weapon == weapon);
        }
        public void LevelUp(Class characterClass)
        {
            BaseAttackBonus.AddModifier(new ValueStatModifier(characterClass.BaseAttackBonusRate, string.Format("{0} Level", characterClass.Name)));            
        }

        private IEnumerable<IAttackStatistic> GetSpecialAttacks()
        {
            return this.components.GetAll<IAttackStatistic>();
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