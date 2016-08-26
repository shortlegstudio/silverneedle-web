//-----------------------------------------------------------------------
// <copyright file="CharacterSheet.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using SilverNeedle.Characters.Appearance;
using SilverNeedle.Characters.Background;


namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;

    /// <summary>
    /// A character sheet is the object that ties everything together.
    /// </summary>
    /// <remarks>This should only delegate to proper objects to perform actions. There should not be specific rule logic here</remarks>
    public class CharacterSheet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.CharacterSheet"/> class.
        /// </summary>
        /// <param name="skillList">Skill list of available skills</param>
        public CharacterSheet(IEnumerable<Skill> skillList)
        {
            // TODO: Figure out if skillList is a logical dependency for the character sheet
            this.AbilityScores = new AbilityScores();
            this.Size = new SizeStats();
            this.Inventory = new Inventory();
            this.Initiative = new Initiative(this.AbilityScores);
            this.Offense = new OffenseStats(this.AbilityScores, this.Size, this.Inventory);
            this.Defense = new DefenseStats(this.AbilityScores, this.Size, this.Inventory);
            this.Movement = new MovementStats();
            this.Languages = new List<Language>();
            this.History = new History();
           

            this.SkillRanks = new SkillRanks(skillList, this.AbilityScores);
            this.SkillRanks.ProcessModifier(this.Size);

            this.Traits = new List<Trait>();
            this.Feats = new List<Feat>();
            this.SpecialQualities = new SpecialQualities();

            this.Level = 1;
        }

        /// <summary>
        /// Occurs when modified.
        /// </summary>
        public event EventHandler<CharacterSheetEventArgs> Modified;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the character.</value>
        public string Name { get; set; }

        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>The character's alignment.</value>
        public CharacterAlignment Alignment { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The character's gender.</value>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The character's size information.</value>
        public SizeStats Size { get; set; }

        /// <summary>
        /// Gets or sets the race.
        /// </summary>
        /// <value>The character's race.</value>
        public Race Race { get; protected set; }

        /// <summary>
        /// Gets or sets the class. TODO: Handle multiclassing
        /// </summary>
        /// <value>The character's class.</value>
        public Class Class { get; set; }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>The character's level.</value>
        public int Level { get; private set; }

        /// <summary>
        /// Gets the ability scores
        /// </summary>
        /// <value>The character's ability scores.</value>
        public AbilityScores AbilityScores { get; private set; }

        /// <summary>
        /// Gets the skill ranks.
        /// </summary>
        /// <value>The character's skill ranks.</value>
        public SkillRanks SkillRanks { get; private set; }

        /// <summary>
        /// Gets the traits.
        /// </summary>
        /// <value>The character's traits.</value>
        public IList<Trait> Traits { get; private set; }

        /// <summary>
        /// Gets the feats.
        /// </summary>
        /// <value>The character's feats.</value>
        public IList<Feat> Feats { get; private set; }

        /// <summary>
        /// Gets the initiative modifier.
        /// </summary>
        /// <value>The characters initiative modifier.</value>
        public Initiative Initiative { get; private set; }

        /// <summary>
        /// Gets the inventory.
        /// </summary>
        /// <value>The character's inventory.</value>
        public Inventory Inventory { get; private set; }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The character's languages.</value>
        public IList<Language> Languages { get; private set; }

        /// <summary>
        /// Gets or sets the max hit points.
        /// </summary>
        /// <value>The character's maximum hit points.</value>
        public int MaxHitPoints { get; set; }

        /// <summary>
        /// Gets or sets the current hit points.
        /// </summary>
        /// <value>The character's current hit points.</value>
        public int CurrentHitPoints { get; set; }

        /// <summary>
        /// Gets the offense stats.
        /// </summary>
        /// <value>The character's offensive abilities.</value>
        public OffenseStats Offense { get; private set; }

        /// <summary>
        /// Gets the defense stats.
        /// </summary>
        /// <value>The character's defense abilities.</value>
        public DefenseStats Defense { get; private set; }

        /// <summary>
        /// Gets the movement speeds.
        /// </summary>
        /// <value>The characters movement abilities.</value>
        public MovementStats Movement { get; private set; }

        public FacialDescription FacialDescription { get; set; }

        public History History { get; set; }

        public SpecialQualities SpecialQualities { get; private set; }

        /// <summary>
        /// Sets this character to Level 1 in specified class
        /// </summary>
        /// <param name="cls">Class of the character</param>
        public void SetClass(Class cls)
        {
            // TODO: Offense and defense have very different behaviors
            this.Class = cls;
            this.Offense.BaseAttackBonus.SetValue(this.CalculateCurrentBaseAttackBonus());

            // Add Weapon Proficiencies
            this.Offense.AddWeaponProficiencies(cls.WeaponProficiencies);

            // Add Armor Proficiencies
            this.Defense.AddArmorProficiencies(cls.ArmorProficiencies);

            this.Defense.LevelUpDefenseStats(cls);
        }

        /// <summary>
        /// Sets the race.
        /// </summary>
        /// <param name="race">The race for the character.</param>
        public void SetRace(Race race)
        {
            Race = race;
        }

        /// <summary>
        /// Sets the level of the character
        /// </summary>
        /// <param name="level">Character's level</param>
        public void SetLevel(int level)
        {
            this.Level = level;
        }

        /// <summary>
        /// Adds a trait to the chraacter
        /// </summary>
        /// <param name="trait">Trait to add.</param>
        /// <param name="notify">If set to <c>true</c> notify.</param>
        public void AddTrait(Trait trait, bool notify = true)
        {
            this.Traits.Add(trait);
            this.ProcessStatModifier(trait);
            this.ProcessSpecialAbilities(trait);

            if (notify)
            {
                this.OnModified();
            }
        }

        /// <summary>
        /// Adds a feat to the character.
        /// </summary>
        /// <param name="feat">Feat to add.</param>
        /// <param name="notify">If set to <c>true</c> notify.</param>
        public void AddFeat(Feat feat, bool notify = true)
        {
            this.Feats.Add(feat);

            // TODO: This is very similar to traits but slightly different. Should be able to standardize the behavior
            this.ProcessStatModifier(feat);
            this.ProcessSpecialAbilities(feat);

            if (notify)
            {
                this.OnModified();
            }
        }

        /// <summary>
        /// Gets a character skill.
        /// </summary>
        /// <returns>The character's ability with the skill found.</returns>
        /// <param name="skill">Skill to lookup.</param>
        public CharacterSkill GetSkill(Skill skill)
        {
            return SkillRanks.GetSkill(skill.Name);
        }

        /// <summary>
        /// Gets the skill value. 
        /// </summary>
        /// <returns>The skill numeric value.</returns>
        /// <param name="name">Name of skill to find.</param>
        public int GetSkillValue(string name)
        {
            return SkillRanks.GetScore(name);
        }

        /// <summary>
        /// Gets the skill points per level. 
        /// </summary>
        /// <returns>The skill points per level.</returns>
        public int GetSkillPointsPerLevel()
        {
            return Class.SkillPoints + AbilityScores.GetModifier(AbilityScoreTypes.Intelligence);
        }

        /// <summary>
        /// Sets the hit points for the character. Sets both maximum and current
        /// </summary>
        /// <param name="hitPoints">new max and current hit points</param>
        public void SetHitPoints(int hitPoints)
        {
            this.MaxHitPoints = hitPoints;
            this.CurrentHitPoints = hitPoints;
        }

        /// <summary>
        /// Raises the modified event. TODO: Should be private
        /// </summary>
        public void OnModified()
        {
            if (this.Modified != null)
            { 
                var args = new CharacterSheetEventArgs();
                args.Sheet = this;
                this.Modified(this, args);
            }
        }
            
        /// <summary>
        /// Processes the stat modifier. This takes anything that modifies stats and relays it to interested classes that might want to monitor for it
        /// TODO: Better mechanism would be a call back whenever a stat modifier is sent to the character sheet
        /// </summary>
        /// <param name="modifier">Modifier that can change stats.</param>
        private void ProcessStatModifier(IModifiesStats modifier)
        {
            SkillRanks.ProcessModifier(modifier);
            this.Defense.ProcessModifier(modifier);
            this.Offense.ProcessModifier(modifier);
        }

        private void ProcessSpecialAbilities(IProvidesSpecialAbilities abilities)
        {
            this.Defense.ProcessSpecialAbilities(abilities);
            this.Offense.ProcessSpecialAbilities(abilities);
            this.SpecialQualities.ProcessSpecialAbilities(abilities);
        }

        /// <summary>
        /// Gets the current base attack bonus. TODO: This should move to OffenseStats or to a mechanic
        /// </summary>
        /// <returns>The current base attack bonus.</returns>
        private int CalculateCurrentBaseAttackBonus()
        {
            return (int)Class.BaseAttackBonusRate * this.Level;
        }
    }
}