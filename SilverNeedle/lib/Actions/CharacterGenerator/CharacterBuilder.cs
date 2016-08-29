//-----------------------------------------------------------------------
// <copyright file="CharacterGenerator.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator
{
    // TODO: This class design is kind of all over the place. Is it trying to do everything or is it driven by an outside source?
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    using SilverNeedle.Actions.CharacterGenerator.Appearance;
    using SilverNeedle.Actions.CharacterGenerator.Background;
    using SilverNeedle.Actions.NamingThings;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Equipment;
    
    /// <summary>
    /// Character generator generates a new character. By specifying different generators different character
    /// creation schemes are possible
    /// </summary>
    public class CharacterBuilder
    {
        /// <summary>
        /// The ability generator handles the ability score creation
        /// </summary>
        private IAbilityScoreGenerator abilityGenerator;

        /// <summary>
        /// The language selector selects what languages the character can speak
        /// </summary>
        private LanguageSelector languageSelector;

        /// <summary>
        /// The race selector chooses which race the character will be
        /// </summary>
        private RaceAssigner raceAssigner;

        /// <summary>
        /// The name generator selects the name for the character
        /// </summary>
        private INameCharacter nameGenerator;

        /// <summary>
        /// The armor gateway provides access to all armor
        /// </summary>
        private IArmorGateway armorGateway;

        /// <summary>
        /// The weapon gateway provides access to all weapons
        /// </summary>
        private IWeaponGateway weaponGateway;

        /// <summary>
        /// The skill gateway provides access to all skills
        /// </summary>
        private IEntityGateway<Skill> skillGateway;

        /// <summary>
        /// The class gateway provides access to classes
        /// </summary>
        private IEntityGateway<Class> classGateway;

        private IRaceMaturityGateway maturityGateway;

        private IRaceGateway raceGateway;
       

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SilverNeedle.Actions.CharacterGenerator.CharacterBuilder"/> class.
        /// </summary>
        /// <param name="abilities">Ability score generator to use.</param>
        /// <param name="langs">Language selector to use.</param>
        /// <param name="races">Race selector to use.</param>
        /// <param name="names">Name selector to use.</param>
        public CharacterBuilder(
            IAbilityScoreGenerator abilities,
            LanguageSelector langs,
            RaceAssigner races,
            INameCharacter names)
        {
            this.abilityGenerator = abilities;
            this.languageSelector = langs;
            this.raceAssigner = races;
            this.nameGenerator = names;

            this.armorGateway = new ArmorYamlGateway();
            this.weaponGateway = new WeaponYamlGateway();
            this.skillGateway = new SkillYamlGateway();
            this.classGateway = new ClassYamlGateway();
            this.maturityGateway = new RaceMaturityYamlGateway();
            this.raceGateway = new RaceYamlGateway();
        }

        /// <summary>
        /// Creates a Level 0 character. A level 0 character has no class but has the basic attributes selected
        /// Think of this as a young character before identifying their professions
        /// </summary>
        /// <returns>The level0.</returns>
        public CharacterSheet CreateLevel0()
        {
            var character = new CharacterSheet(this.skillGateway.All());

            character.Gender = EnumHelpers.ChooseOne<Gender>();
            character.Alignment = EnumHelpers.ChooseOne<CharacterAlignment>();
            this.abilityGenerator.AssignAbilities(character.AbilityScores);
            this.raceAssigner.SetRace(character, raceGateway.All().ToList().ChooseOne());

            character.Languages.Add(
                this.languageSelector.PickLanguages(
                    character.Race, 
                    character.AbilityScores.GetModifier(AbilityScoreTypes.Intelligence)));

            // Assign Age to adult
            character.Age = maturityGateway.Get(character.Race).Adulthood;

            //Generate a facial description
            var facials = new CreateFacialFeatures();
            character.FacialDescription = facials.CreateFace(character.Gender);

            // Names come last
            character.Name = this.nameGenerator.CreateFullName(character.Gender, character.Race.Name);

            character.History = GenerateHistory(character);

            return character;
        }

        /// <summary>
        /// Selects the class for the character
        /// </summary>
        /// <returns>The class that was selected.</returns>
        /// <param name="character">Character to assign class to.</param>
        public CharacterSheet SelectClass(CharacterSheet character)
        {
            character.SetClass(this.classGateway.All().ToList().ChooseOne());
            var hp = new HitPointGenerator();
            character.SetHitPoints(hp.RollHitPoints(character));

            // Assign Age based on class
            var assignAge = new AssignAge();
            character.Age = assignAge.RandomAge(character.Class.ClassDevelopmentAge, maturityGateway.Get(character.Race));

            // Figure out how this class came about
            var classOrigin = new ClassOriginStoryCreator(new ClassOriginYamlGateway());
            character.History.ClassOriginStory = classOrigin.CreateStory(character.Class.Name);

            return character;
        }

        /// <summary>
        /// Generates the random character.
        /// </summary>
        /// <returns>The random character.</returns>
        public CharacterSheet GenerateRandomCharacter()
        {
            var skillGen = new SkillPointGenerator();

            var character = this.CreateLevel0();
            this.SelectClass(character);
            character.AddFeat(Feat.GetQualifyingFeats(character).ToList().ChooseOne());

            // Assign Skill Points
            skillGen.AssignSkillPointsRandomly(character);

            // Get some gear!
            var equip = new EquipMeleeAndRangedWeapon(this.weaponGateway);
            equip.AssignWeapons(character.Inventory, character.Offense.WeaponProficiencies);

            var equipArmor = new PurchaseInitialArmor(this.armorGateway);
            equipArmor.PurchaseArmorAndShield(character.Inventory, character.Defense.ArmorProficiencies);

            return character;
        }


        private History GenerateHistory(CharacterSheet character)
        {
            var history = new History();

            //Homeland
            var homelandSelector = new HomelandSelector(new HomelandYamlGateway());
            history.Homeland = homelandSelector.SelectHomelandByRace(character.Race.Name);

            // Family
            var familyHistory = new FamilyHistoryCreator(this.nameGenerator);
            history.FamilyTree = familyHistory.CreateFamilyTree(character.Race.Name);

            // Drawback
            var drawback = new CharacterDrawbackSelector(new DrawbackYamlGateway());
            history.Drawback = drawback.SelectDrawback();

            return history;
        }
    }
}