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
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Utility;

    
    /// <summary>
    /// A character sheet is the object that ties everything together.
    /// </summary>
    /// <remarks>This should only delegate to proper objects to perform actions. There should not be specific rule logic here</remarks>
    public class CharacterSheet : IHitPointTracker
    {
        public CharacterSheet() : this(new List<Skill>())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.CharacterSheet"/> class.
        /// </summary>
        /// <param name="skillList">Skill list of available skills</param>
        public CharacterSheet(IEnumerable<Skill> skillList)
        {
            this.Components = new ComponentBag();
            this.Components.Add(new AbilityScores());
            this.Components.Add(new SizeStats());
            this.Components.Add(new Inventory());
            this.Components.Add(new List<Language>());
            this.Components.Add(new SpecialQualities());
            this.Components.Add(new History());
            this.Components.Add(new PersonalityType("ESTJ"));
            this.Components.Add(new Queue<AbilityScoreToken>());
            this.Components.Add(new List<FeatToken>());
            this.Components.Add(new OffenseStats(this.AbilityScores, this.Size, this.Inventory));
            this.Components.Add(new DefenseStats());
            this.Components.Add(new MovementStats());
            this.Components.Add(new CharacterAppearance());
            this.Components.Add(new SkillRanks(skillList, this.AbilityScores));
            this.Components.Add(new Initiative(this.AbilityScores));
            this.Components.Add(new SpellCasting(this.Inventory));
            this.Level = 1;
        }

        public void InitializeComponents()
        {
            var components = this.Components.GetAll<IComponent>();
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

        public ComponentBag Components { get; private set; }

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

        public PersonalityType PersonalityType { 
            get { return this.Components.Get<PersonalityType>(); }
            set { this.Components.Replace<PersonalityType>(value); }
        }

        public Ideal Ideal { 
            get { return this.Components.Get<Ideal>(); }
            set { this.Components.Replace<Ideal>(value); }
        }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The character's size information.</value>
        public SizeStats Size 
        { 
            get { return this.Components.Get<SizeStats>(); }
        }

        /// <summary>
        /// Gets or sets the race.
        /// </summary>
        /// <value>The character's race.</value>
        public Race Race 
        { 
            get { return this.Components.Get<Race>(); }
        }

        /// <summary>
        /// Gets or sets the class. TODO: Handle multiclassing
        /// </summary>
        /// <value>The character's class.</value>
        public Class Class { 
            get 
            { 
                return this.Components.Get<Class>();
            } 
        }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>The character's level.</value>
        public int Level { get; private set; }

        /// <summary>
        /// Gets the ability scores
        /// </summary>
        /// <value>The character's ability scores.</value>
        public AbilityScores AbilityScores { 
            get { return this.Components.Get<AbilityScores>(); } 
        }

        public Queue<AbilityScoreToken> AbilityScoreTokens 
        { 
            get { return this.Components.Get<Queue<AbilityScoreToken>>(); }
        }

        /// <summary>
        /// Gets the skill ranks.
        /// </summary>
        /// <value>The character's skill ranks.</value>
        public SkillRanks SkillRanks { get { return this.Components.Get<SkillRanks>(); } }

        /// <summary>
        /// Gets the traits.
        /// </summary>
        /// <value>The character's traits.</value>
        public IEnumerable<Trait> Traits { get { return this.Components.GetAll<Trait>(); } }

        /// <summary>
        /// Gets the feats.
        /// </summary>
        /// <value>The character's feats.</value>
        public IEnumerable<Feat> Feats { get { return this.Components.GetAll<Feat>(); } }

        /// <summary>
        /// Represents an available feat chose the generator may make. Could be triggered
        /// by a racial trait, level up, or class bonus for example
        /// </summary>
        /// <returns>Any feat tokens available</returns>
        public IList<FeatToken> FeatTokens { get { return this.Components.Get<List<FeatToken>>(); } }

        /// <summary>
        /// Gets the initiative modifier.
        /// </summary>
        /// <value>The characters initiative modifier.</value>
        public Initiative Initiative { get { return this.Components.Get<Initiative>(); } }

        /// <summary>
        /// Gets the inventory.
        /// </summary>
        /// <value>The character's inventory.</value>
        public Inventory Inventory { 
            get { return this.Components.Get<Inventory>(); }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The character's languages.</value>
        public IEnumerable<Language> Languages { get { return this.Components.GetAll<Language>(); } }

        /// <summary>
        /// Gets or sets the max hit points.
        /// </summary>
        /// <value>The character's maximum hit points.</value>
        public int MaxHitPoints { get; private set; }

        /// <summary>
        /// Gets or sets the current hit points.
        /// </summary>
        /// <value>The character's current hit points.</value>
        public int CurrentHitPoints { get; private set; }

        /// <summary>
        /// Gets the offense stats.
        /// </summary>
        /// <value>The character's offensive abilities.</value>
        public OffenseStats Offense { get { return this.Components.Get<OffenseStats>(); } }

        /// <summary>
        /// Gets the defense stats.
        /// </summary>
        /// <value>The character's defense abilities.</value>
        public DefenseStats Defense { get { return this.Components.Get<DefenseStats>(); } }

        /// <summary>
        /// Gets the movement speeds.
        /// </summary>
        /// <value>The characters movement abilities.</value>
        public MovementStats Movement { get { return this.Components.Get<MovementStats>(); } }

        public CharacterAppearance Appearance { get { return this.Components.Get<CharacterAppearance>(); } }

        public History History { get { return this.Components.Get<History>(); } }

        public SpecialQualities SpecialQualities { get { return this.Components.Get<SpecialQualities>(); } }

        public SpellCasting SpellCasting { get { return this.Components.Get<SpellCasting>(); } }

        /// <summary>
        /// Sets this character to Level 1 in specified class
        /// </summary>
        /// <param name="cls">Class of the character</param>
        public void SetClass(Class cls)
        {
            // TODO: Offense and defense have very different behaviors
            this.Components.Add(cls);
            
            // Add Weapon Proficiencies
            this.Offense.AddWeaponProficiencies(cls.WeaponProficiencies);

            this.Offense.LevelUp(cls);

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
            this.Components.Add(race);
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
        public void AddTrait(Trait trait)
        {
            this.Components.Add(trait);
            this.ProcessStatModifier(trait);
            this.ProcessSpecialAbilities(trait);
        }

        /// <summary>
        /// Adds a feat to the character.
        /// </summary>
        /// <param name="feat">Feat to add.</param>
        /// <param name="notify">If set to <c>true</c> notify.</param>
        public void AddFeat(Feat feat)
        {
            this.Components.Add(feat);

            // TODO: This is very similar to traits but slightly different. Should be able to standardize the behavior
            this.ProcessStatModifier(feat);
            this.ProcessSpecialAbilities(feat);
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

        public CharacterSkill GetSkill(string skillName)
        {
            return SkillRanks.GetSkill(skillName);
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
            return Class.SkillPoints + SkillRanks.BonusSkillPointsPerLevel();
        }

        /// <summary>
        /// Sets the hit points for the character. Sets both maximum and current
        /// </summary>
        /// <param name="hitPoints">new max and current hit points</param>
        public void SetMaxHitPoints(int hitPoints)
        {
            this.MaxHitPoints = hitPoints;
        }

        public void SetCurrentHitPoints(int hp)
        {
            this.CurrentHitPoints = hp;
        }

        public void IncreaseHitPoints(int hp)
        {
            this.MaxHitPoints += hp;
            this.CurrentHitPoints += hp;
        }

        public void AddAbility(SpecialAbility ability)
        {
            this.Components.Add(ability);
            this.SpecialQualities.Add(ability);

            var component = ability as IComponent;
            if(component != null)
            {
                component.Initialize(this.Components);
            }

        }

        public void ProcessLevel(Level level)
        {
            this.Components.ApplyStatModifiers(level.Modifiers);
            this.FeatTokens.Add(level.FeatTokens);
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

        public void ProcessSpecialAbilities(IProvidesSpecialAbilities abilities)
        {
            this.Defense.ProcessSpecialAbilities(abilities);
            this.Offense.ProcessSpecialAbilities(abilities);
            this.SpecialQualities.ProcessSpecialAbilities(abilities);


            foreach(var mod in abilities.SpecialAbilities) {
                if(mod.Type == "Feat Token")
                {
                    var token = new FeatToken(mod.Condition);
                    this.FeatTokens.Add(token);
                }
            }
        }

        public void Add<T>(T feature)
        {
            this.Components.Add(feature);
        }

        public BasicStat FindStat(string name)
        {
            return GetAllStats().First(x => x.Name.EqualsIgnoreCase(name));
        }

        public IEnumerable<BasicStat> GetAllStats()
        {
            return this.Components.GetAllStats();
        }
    }
}