// Copyright (c) 2017 Trevor Redfern
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
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    /// <summary>
    /// A character sheet is the object that ties everything together.
    /// </summary>
    /// <remarks>This should only delegate to proper objects to perform actions. There should not be specific rule logic here</remarks>
    public class CharacterSheet 
    {
        private CharacterSheet() { }
        public CharacterSheet(CharacterStrategy strategy) 
        {
            this.Components = new ComponentContainer();
            this.Components.Add(strategy);
        }

        [ObjectStoreOptional("components")]
        public ComponentContainer Components { get; private set; }

        public CharacterStrategy Strategy { get { return this.Get<CharacterStrategy>(); } }

        [ObjectStoreOptional("first-name")]
        public string FirstName { get; set; }
        [ObjectStoreOptional("last-name")]
        public string LastName { get; set; }
        public string Name { get { return string.Format("{0} {1}", FirstName, LastName).Trim(); } }

        [ObjectStoreOptional("age")]
        public int Age { get; set; }

        [ObjectStoreOptional("alignment")]
        public CharacterAlignment Alignment 
        {
            get { return Get<CharacterAlignment>(); }
            set { Replace<CharacterAlignment>(value); }
        }

        [ObjectStoreOptional("gender")]
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
        public IValueStatistic Initiative { get { return Components.FindStat<IValueStatistic>("initiative"); } }

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
        public IValueStatistic HitPoints { get { return this.Components.FindStat<IValueStatistic>(StatNames.HitPoints); } }

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
            this.Components.Add(new ClassLevel(cls));
            this.Components.Add(cls);
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
            return Class.SkillPoints + Components.FindStat<IValueStatistic>(StatNames.SkillPoints).TotalValue;
        }

        private void InitializeComponent(object obj)
        {
            var component = obj as IComponent;
            if (component != null)
            {
                component.Initialize(this.Components);
            }
        }

        public void AddRange(IEnumerable<object> features)
        {
            foreach(var f in features)
                Add(f);
        }

        public void Add<T>(T feature)
        {
            this.Components.Add(feature);
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
            //TODO: This seems fishy to ToList and return IEnumerable
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
            return this.Components.FindStat(name);
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

        public void Save(IObjectStore objectStore)
        {
            objectStore.Serialize(this);
        }

        public static CharacterSheet Load(IObjectStore objectStore)
        {
            var sheet = new CharacterSheet();
            objectStore.Deserialize(sheet);
            return sheet;
        }
    }
}