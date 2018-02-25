// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using SilverNeedle;
  using SilverNeedle.Dice;
  using SilverNeedle.Serialization;
  using SilverNeedle.Utility;

  /// <summary>
  /// Represents a character's Class or profession
  /// </summary>
    public class Class : CharacterFeature, IGatewayObject
    {
        public static Class CreateForTesting() 
        { 
            return CreateForTesting("Unknown", DiceSides.d4);
        }
        public static Class CreateForTesting(string name, DiceSides hd)
        {
            return new Class(name, hd);
        } 

        /// <summary>
        /// The good save rate. TODO: This should be configurable or moved
        /// somewhere closer to saves
        /// </summary>
        public const float GoodSaveRate = 0.667f;

        /// <summary>
        /// The poor save rate. TODO: This should be configurable or moved
        /// somewhere closer to saves
        /// </summary>
        public const float PoorSaveRate = 0.334f;

        public Class(IObjectStore data) : base(data)
        {
            this.Levels = new List<Level>();
            LoadFromObjectStore(data);
        }

        private Class(string name, DiceSides hd) 
        {
            this.Levels = new List<Level>();
            this.Name = name;
            this.HitDice = hd;
            this.HitDicePerLevel = new DiceClassLevelModifier(new Cup(new Die(HitDice)), StatNames.HitDice, this.Name, 1);
        }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>The class name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the skill points per level
        /// </summary>
        /// <value>The skill points.</value>
        public int SkillPoints { get; set; }

        /// <summary>
        /// Gets or sets the hit dice for each level
        /// </summary>
        /// <value>The hit dice die sides.</value>
        public DiceSides HitDice { get; set; }

        /// <summary>
        /// Gets or sets the base attack bonus rate per level.
        /// </summary>
        /// <value>The base attack bonus rate.</value>
        public float BaseAttackBonusRate { get; set; }

        /// <summary>
        /// Gets or sets the fortitude save rate.
        /// </summary>
        /// <value>The fortitude save rate.</value>
        public float FortitudeSaveRate { get; set; }

        /// <summary>
        /// Gets or sets the reflex save rate.
        /// </summary>
        /// <value>The reflex save rate.</value>
        public float ReflexSaveRate { get; set; }

        /// <summary>
        /// Gets or sets the will save rate.
        /// </summary>
        /// <value>The will save rate.</value>
        public float WillSaveRate { get; set; }

        public ClassDevelopmentAge ClassDevelopmentAge { get; set; }

        public IList<Level> Levels { get; set; }

        public Dice.Cup StartingWealthDice { get; set; }

        public string CustomBuildStep { get; set; }


        /// <summary>
        /// Gets a value indicating whether this class has a good fortitude save.
        /// </summary>
        /// <value><c>true</c> if this instance is fortitude good save; otherwise, <c>false</c>.</value>
        public bool IsFortitudeGoodSave
        { 
            get { return this.FortitudeSaveRate == GoodSaveRate; }
        }

        /// <summary>
        /// Gets a value indicating whether this class has a good reflex save.
        /// </summary>
        /// <value><c>true</c> if this instance is reflex good save; otherwise, <c>false</c>.</value>
        public bool IsReflexGoodSave
        {
            get { return this.ReflexSaveRate == GoodSaveRate; }
        }

        /// <summary>
        /// Gets a value indicating whether this class has a good will save.
        /// </summary>
        /// <value><c>true</c> if this instance is will good save; otherwise, <c>false</c>.</value>
        public bool IsWillGoodSave
        {
            get { return this.WillSaveRate == GoodSaveRate; }
        }

        public Level GetLevel(int levelNumber)
        {
            var l = Levels.FirstOrDefault(x => x.Number == levelNumber);
            if (l == null) {
                l = new Level(levelNumber);
            }
            return l;
        }

        [AddToContainer]
        public DiceClassLevelModifier HitDicePerLevel { get; private set; }

        private void LoadFromObjectStore(IObjectStore data)
        {
            Name = data.GetString("name"); 
            ShortLog.Debug("Loading Class: " + Name);
            SkillPoints = data.GetInteger("skillpoints");
            HitDice = DiceStrings.ParseSides(data.GetString("hitdice"));
            BaseAttackBonusRate = data.GetFloat("baseattackbonus");
            FortitudeSaveRate = data.GetFloat("fortitude");
            ReflexSaveRate = data.GetFloat("reflex");
            WillSaveRate = data.GetFloat("will");
            ClassDevelopmentAge = data.GetEnum<ClassDevelopmentAge>("developedage");
            CustomBuildStep = data.GetStringOptional("custom-build-step");

            //Load Levels
            var levels = data.GetObjectOptional("levels");
            if (levels != null)
            {
                foreach(var l in levels.Children)
                {
                    var level = new Level(l);
                    Levels.Add(level);
                }
            }

            var dice = data.GetStringOptional("startingwealth");
            if(dice != null)
            {
                StartingWealthDice = DiceStrings.ParseDice(dice);
            }

            this.HitDicePerLevel = new DiceClassLevelModifier(new Cup(new Die(HitDice)), StatNames.HitDice, this.Name, 1);
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}