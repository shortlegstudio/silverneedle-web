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

    public class OffenseStats : IComponent
    {
        public const int UnproficientWeaponModifier = -4;
        public ComponentContainer Parent { get; set; }

        public IValueStatistic CombatManeuverDefense { get { return Parent.FindStat<IValueStatistic>(StatNames.CMD); } }

        public IValueStatistic CombatManeuverBonus { get { return Parent.FindStat<IValueStatistic>(StatNames.CMB); } }

        private Inventory inventory;

        public IEnumerable<IWeaponModifier> WeaponModifiers { get { return Parent.GetAll<IWeaponModifier>(); } }

        public OffenseStats() { }

        public void Initialize(ComponentContainer components)
        {
            var abilities = components.Get<AbilityScores>();
            this.Strength = abilities.GetAbility(AbilityScoreTypes.Strength);
            this.Dexterity = abilities.GetAbility(AbilityScoreTypes.Dexterity);
            var size = components.Get<SizeStats>();
            this.Size = size;
            this.inventory = components.Get<Inventory>();

            this.CombatManeuverBonus.AddModifier(size.NegativeSizeModifier);
            this.CombatManeuverDefense.AddModifier(size.NegativeSizeModifier);
        }

        /// <summary>
        /// Gets the weapon proficiencies.
        /// </summary>
        /// <value>The weapon proficiencies for the character.</value>
        public IEnumerable<WeaponProficiency> WeaponProficiencies { get { return Parent.GetAll<WeaponProficiency>(); } }

        /// <summary>
        /// Gets the base attack bonus.
        /// </summary>
        /// <value>The base attack bonus.</value>
        public BaseAttackBonus BaseAttackBonus { get { return Parent.FindStat<BaseAttackBonus>(StatNames.BaseAttackBonus); } }

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
        public IValueStatistic MeleeAttackBonus { get { return Parent.Get<MeleeAttackBonus>(); } }

        /// <summary>
        /// Calculates the range attack bonus.
        /// </summary>
        /// <returns>The attack bonus.</returns>
        public IValueStatistic RangeAttackBonus { get { return Parent.Get<RangeAttackBonus>(); } }

        public void AddWeaponProficiencies(IEnumerable<string> proficiencies)
        {
            foreach (var p in proficiencies)
            {
                this.AddWeaponProficiency(p);
            }
        }

        public void AddWeaponProficiency(string proficiency)
        {
            this.Parent.Add(new WeaponProficiency(proficiency));
        }

        public bool IsProficient(IWeaponAttackStatistics weapon)
        {
            return this.WeaponProficiencies.IsProficient(weapon);
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
            return this.Parent.GetAll<IAttack>();
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