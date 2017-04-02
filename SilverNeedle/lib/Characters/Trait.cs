// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    /// <summary>
    /// A trait is some basic innate attribute of the character. Usually positive
    /// </summary>
    public class Trait : IModifiesStats, IProvidesSpecialAbilities, IGatewayObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Trait"/> class.
        /// </summary>
        public Trait()
        {
            this.Modifiers = new List<BasicStatModifier>();
            this.SpecialAbilities = new List<SpecialAbility>();
            this.Tags = new List<string>();
        }

        public Trait(IObjectStore data) : this()
        {
            LoadObject(data);
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the modifiers for the stats that are to be modified.
        /// </summary>
        /// <value>The modifiers for stats effected by this class.</value>
        public IList<BasicStatModifier> Modifiers { get; private set; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public IList<string> Tags { get; private set; }


        public IList<SpecialAbility> SpecialAbilities { get; private set; }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        private void LoadObject(IObjectStore data)
        {
            Name = data.GetString("name"); 
            ShortLog.Debug("Loading Trait: " + Name);
            Description = data.GetString("description");
            Tags.Add(data.GetListOptional("tags"));

            // Get Any skill Modifiers if they exist
            var modifiers = data.GetObjectOptional("modifiers");
            if (modifiers != null)
            {
                var mods = ParseStatModifiersYaml.ParseYaml(modifiers, string.Format("{0} (trait)", Name));
                foreach (var m in mods)
                {
                    Modifiers.Add(m);
                }
            }

            // Get any special abilities
            var abilities = data.GetObjectOptional("special");
            if (abilities != null)
            {
                foreach (var spec in abilities.Children)
                {
                    var specialAbility = new SpecialAbility(
                        spec.GetString("condition"),
                        spec.GetString("type"));
                    SpecialAbilities.Add(specialAbility);
                }
            }
        }
    }
}