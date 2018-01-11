﻿// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Utility;

    
    /// <summary>
    /// A character sheet is the object that ties everything together.
    /// </summary>
    /// <remarks>This should only delegate to proper objects to perform actions. There should not be specific rule logic here</remarks>
    public class CharacterSheet 
    {
        public CharacterSheet(CharacterStrategy strategy) 
        {
            this.Components = new ComponentContainer();
            var abilityScores = new AbilityScores();
            this.Components.AddNoInitialize(new object[] { 
                strategy,
                abilityScores,
                new SizeStats(),
                new Inventory(),
                new List<Language>(),
                new History(),
                new OffenseStats(),
                new MeleeAttackBonus(),
                new RangeAttackBonus(),
                new DefenseStats(), 
                new MovementStats(),
                new CharacterAppearance(),
                new SkillRanks(abilityScores),
                new Initiative(abilityScores),
                new PersonalityType("ESTJ"),
                new Likes()
            });
        }

        public void InitializeComponents()
        {
            var components = this.Components.GetAll<IComponent>().ToList();
            foreach(var c in components)
            {
                c.Initialize(this.Components);
            }
            // TODO: This is interesting...
            this.SkillRanks.ProcessModifier(this.Size);
        }

        /// <summary>
        /// Occurs when modified.
        /// </summary>

        public ComponentContainer Components { get; private set; }

        public CharacterStrategy Strategy { get { return this.Get<CharacterStrategy>(); } }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the character.</value>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get { return string.Format("{0} {1}", FirstName, LastName).Trim(); } }

        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>The character's alignment.</value>
        public CharacterAlignment Alignment 
        {
            get { return Get<CharacterAlignment>(); }
            set { Replace<CharacterAlignment>(value); }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The character's gender.</value>
        public Gender Gender { get; set; }

        public PersonalityType PersonalityType { 
            get { return this.Get<PersonalityType>(); }
            set { this.Replace<PersonalityType>(value); }
        }

        public Ideal Ideal { 
            get { return this.Get<Ideal>(); }
            set { this.Replace<Ideal>(value); }
        }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The character's size information.</value>
        public SizeStats Size 
        { 
            get { return this.Get<SizeStats>(); }
        }

        /// <summary>
        /// Gets or sets the race.
        /// </summary>
        /// <value>The character's race.</value>
        public Race Race 
        { 
            get { return this.Get<Race>(); }
        }

        /// <summary>
        /// Gets or sets the class. TODO: Handle multiclassing
        /// </summary>
        /// <value>The character's class.</value>
        public Class Class { 
            get 
            { 
                return this.Get<Class>();
            } 
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>The character's level.</value>
        public int Level { 
            get 
            { 
                var classLevel = this.Get<ClassLevel>();
                if(classLevel == null)
                    return 0;
                
                return classLevel.Level;
            }
        }

        /// <summary>
        /// Gets the ability scores
        /// </summary>
        /// <value>The character's ability scores.</value>
        public AbilityScores AbilityScores { 
            get { return this.Get<AbilityScores>(); } 
        }

        /// <summary>
        /// Gets the skill ranks.
        /// </summary>
        /// <value>The character's skill ranks.</value>
        public SkillRanks SkillRanks { get { return this.Get<SkillRanks>(); } }

        /// <summary>
        /// Gets the traits.
        /// </summary>
        /// <value>The character's traits.</value>
        public IEnumerable<ITrait> Traits { get { return this.GetAll<ITrait>(); } }

        /// <summary>
        /// Gets the feats.
        /// </summary>
        /// <value>The character's feats.</value>
        public IEnumerable<Feat> Feats { get { return this.GetAll<Feat>(); } }

        /// <summary>
        /// Represents an available feat chose the generator may make. Could be triggered
        /// by a racial trait, level up, or class bonus for example
        /// </summary>
        /// <returns>Any feat tokens available</returns>
        public IEnumerable<FeatToken> FeatTokens { get { return this.GetAll<FeatToken>(); } }

        /// <summary>
        /// Gets the initiative modifier.
        /// </summary>
        /// <value>The characters initiative modifier.</value>
        public Initiative Initiative { get { return this.Get<Initiative>(); } }

        /// <summary>
        /// Gets the inventory.
        /// </summary>
        /// <value>The character's inventory.</value>
        public Inventory Inventory { 
            get { return this.Get<Inventory>(); }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The character's languages.</value>
        public IEnumerable<Language> Languages { get { return this.GetAll<Language>(); } }

        /// <summary>
        /// Gets or sets the max hit points.
        /// </summary>
        /// <value>The character's maximum hit points.</value>
        public IStatistic HitPoints { get { return this.FindStat(StatNames.HitPoints); } }

        /// <summary>
        /// Gets the offense stats.
        /// </summary>
        /// <value>The character's offensive abilities.</value>
        public OffenseStats Offense { get { return this.Get<OffenseStats>(); } }

        /// <summary>
        /// Gets the defense stats.
        /// </summary>
        /// <value>The character's defense abilities.</value>
        public DefenseStats Defense { get { return this.Get<DefenseStats>(); } }

        /// <summary>
        /// Gets the movement speeds.
        /// </summary>
        /// <value>The characters movement abilities.</value>
        public MovementStats Movement { get { return this.Get<MovementStats>(); } }

        public CharacterAppearance Appearance { get { return this.Get<CharacterAppearance>(); } }

        public History History { get { return this.Get<History>(); } }

        public IEnumerable<IAbility> Abilities { get { return this.GetAll<IAbility>(); } }

        /// <summary>
        /// Sets this character to Level 1 in specified class
        /// </summary>
        /// <param name="cls">Class of the character</param>
        public void SetClass(Class cls)
        {
            this.Components.Add(cls);
            this.Components.Add(new ClassLevel(cls));
            this.Offense.LevelUp(cls);
            this.Defense.LevelUpDefenseStats(cls);
        }

        /// <summary>
        /// Sets the level of the character
        /// </summary>
        /// <param name="level">Character's level</param>
        public void SetLevel(int level)
        {
            var classLevel = this.Get<ClassLevel>();
            if(classLevel == null)
                throw new MissingCharacterClassException();
             classLevel.Level = level;
        }

        /// <summary>
        /// Gets the skill points per level. 
        /// </summary>
        /// <returns>The skill points per level.</returns>
        public int GetSkillPointsPerLevel()
        {
            return Class.SkillPoints + SkillRanks.BonusSkillPointsPerLevel();
        }

        private void InitializeComponent(object obj)
        {
            var component = obj as IComponent;
            if (component != null)
            {
                component.Initialize(this.Components);
            }
        }

        /// <summary>
        /// Processes the stat modifier. This takes anything that modifies stats and relays it to interested classes that might want to monitor for it
        /// TODO: Better mechanism would be a call back whenever a stat modifier is sent to the character sheet
        /// </summary>
        /// <param name="modifier">Modifier that can change stats.</param>
        public void ProcessStatModifier(IModifiesStats modifier)
        {
            SkillRanks.ProcessModifier(modifier);
            this.Defense.ProcessModifier(modifier);
            this.Offense.ProcessModifier(modifier);
        }

        public void AddRange(IEnumerable<object> features)
        {
            foreach(var f in features)
                Add(f);
        }

        public void Add<T>(T feature)
        {
            ShortLog.DebugFormat("Adding component: {0}", feature.ToString());
            this.Components.Add(feature);
            var statMod = feature as IModifiesStats;
            if(statMod != null)
                this.ProcessStatModifier(statMod);
        }

        public T Get<T>()
        {
            return this.Components.Get<T>();
        }

        public T GetOrDefault<T>(T defaultIfMissing)
        {
            var item = Get<T>();
            if(item == null)
                return defaultIfMissing;
            
            return item;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.Components.GetAll<T>().ToList();
        }

        public void Replace<T>(T value)
        {
            this.Components.Replace<T>(value);
        }

        public bool Contains<T>()
        {
            return this.Components.Contains<T>();
        }

        public IStatistic FindStat(string name)
        {
            return GetAllStats().First(x => x.Name.EqualsIgnoreCase(name));
        }

        public IEnumerable<IStatistic> GetAllStats()
        {
            return this.Components.GetAllStats();
        }

        public IEnumerable<T> GetAndRemoveAll<T>()
        {
            var list = this.GetAll<T>();
            this.Components.Remove<T>();
            return list;
        }
    }
}