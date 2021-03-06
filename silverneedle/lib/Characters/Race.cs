﻿// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Dice;
    
    /// <summary>
    /// Represents a race for a character. This is selected once
    /// </summary>
    public class Race : Feature, IGatewayObject
    {
        public static Race None { get { return new Race(); } }
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Race"/> class.
        /// </summary>
        public Race()
        {
            this.AvailableLanguages = new List<string>();
            this.KnownLanguages = new List<string>();
        }

        public Race(IObjectStore data) : base(data)
        {
            this.AvailableLanguages = new List<string>();
            this.KnownLanguages = new List<string>();
            LoadObject(data);
        }

        /// <summary>
        /// Gets the known languages that a character of this race will always know
        /// </summary>
        /// <value>The known languages.</value>
        public IList<string> KnownLanguages { get; private set; }

        /// <summary>
        /// Gets the available languages that are available for intelligent characters
        /// </summary>
        /// <value>The available languages.</value>
        public IList<string> AvailableLanguages { get; private set; }

        /// <summary>
        /// Gets or sets the size setting.
        /// </summary>
        /// <value>The size setting.</value>
        public CharacterSize SizeSetting { get; set; }

        /// <summary>
        /// Gets or sets the height range dice.
        /// </summary>
        /// <value>The height range.</value>
        public Cup HeightRange { get; set; }

        /// <summary>
        /// Gets or sets the weight range dice.
        /// </summary>
        /// <value>The weight range.</value>
        public Cup WeightRange { get; set; }

        /// <summary>
        /// Gets or sets the base movement speed.
        /// </summary>
        /// <value>The base movement speed.</value>
        public int BaseMovementSpeed { get; set; }

        public Maturity Maturity { get; set; }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        private void LoadObject(IObjectStore data)
        {
            Name = data.GetString("name"); 
            ShortLog.Debug("Loading Race: " + Name);
            SizeSetting = (CharacterSize)System.Enum.Parse(typeof(CharacterSize), data.GetString("size"));
            HeightRange = DiceStrings.ParseDice(data.GetString("height"));
            WeightRange = DiceStrings.ParseDice(data.GetString("weight"));
            BaseMovementSpeed = data.GetInteger("basemovementspeed");

            var languages = data.GetObject("languages");
            KnownLanguages.Add(languages.GetListOptional("known"));
            AvailableLanguages.Add(languages.GetListOptional("available"));
        }

        public static Race Named(string name)
        {
            var race = new Race();
            race.Name = name;
            return race;
        }
    }
}