// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;

    /// <summary>
    /// Represents a feat ability for a character that allows it to perform
    /// special and advanced abilities
    /// </summary>
    public class Feat : FeatureAttribute, IGatewayObject, IGatewayCopy<Feat>
    {

        public Feat(IObjectStore data) : base(data)
        {
            this.Modifiers = new List<IValueStatModifier>();
            this.Prerequisites = new PrerequisiteList();
            this.Tags = new List<string>();
            LoadObject(data);
        }

        protected Feat(Feat copy) : base(copy)
        {
            this.Description = copy.Description;
            this.AllowMultiple = copy.AllowMultiple;
            this.Modifiers = copy.Modifiers;
            this.Prerequisites = copy.Prerequisites;
            this.Tags = copy.Tags;
        }

        /// <summary>
        /// Gets or sets the description of the feat
        /// </summary>
        /// <value>The description of the feat.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the modifiers for the stats that are to be modified.
        /// </summary>
        /// <value>The modifiers for stats effected by this feat.</value>
        public IList<IValueStatModifier> Modifiers { get; private set; }

        /// <summary>
        /// Gets the prerequisites.
        /// </summary>
        /// <value>The prerequisites needed to meet requirements for this feat.</value>
        public PrerequisiteList Prerequisites { get; set; }

        /// <summary>
        /// Gets or sets the tags for categorizing this feat.
        /// </summary>
        /// <value>The tags for this feat.</value>
        public IList<string> Tags { get; set; }
        public bool AllowMultiple { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is combat feat.
        /// </summary>
        /// <value><c>true</c> if this instance is combat feat; otherwise, <c>false</c>.</value>
        public bool IsCombatFeat 
        { 
            get { return this.Tags.Contains("combat"); } 
        }

        /// <summary>
        /// Gets a value indicating whether this instance is critical feat.
        /// </summary>
        /// <value><c>true</c> if this instance is critical feat; otherwise, <c>false</c>.</value>
        public bool IsCriticalFeat 
        { 
            get { return this.Tags.Contains("critical"); } 
        }

        /// <summary>
        /// Gets a value indicating whether this instance is item creation.
        /// </summary>
        /// <value><c>true</c> if this instance is item creation; otherwise, <c>false</c>.</value>
        public bool IsItemCreation 
        { 
            get { return this.Tags.Contains("itemcreation"); } 
        }


        /// <summary>
        /// Figures out if the character is qualified.
        /// </summary>
        /// <param name="character">Character to validate.</param>
        /// <returns>true if the character is qualified</returns>
        public virtual bool IsQualified(CharacterSheet character)
        {
            return Prerequisites.IsQualified(character.Components) &&
                IsQualifiedIgnorePrerequisites(character);
        }

        public bool IsQualifiedIgnorePrerequisites(CharacterSheet character)
        {
            return (!character.Feats.Contains(this) || this.AllowMultiple);
        }

        public override string ToString()
        {
            return string.Format("Feat: {0}", Name);
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        private void LoadObject(IObjectStore data)
        {
            ShortLog.DebugFormat("Loading Feat: {0}", Name);
            Description = data.GetStringOptional("description");
            AllowMultiple = data.GetBoolOptional("allow-multiple");

            // Get any prerequisites
            var prereq = data.GetObjectListOptional("prerequisites");
            if (prereq != null)
            {
                Prerequisites = new PrerequisiteList(prereq);
            }

            Tags = data.GetListOptional("tags").ToList();
        }

        public static Feat Named(string name)
        {
            var emptyStore = new MemoryStore();
            emptyStore.SetValue("name", name);
            var feat = new Feat(emptyStore);
            return feat;
        }

        public virtual Feat Copy()
        {
            return new Feat(this);
        }

        public override bool Equals(object obj)
        {
            if(obj != null && obj is Feat)
            {
                var feat = (Feat)obj;
                return feat.Name == this.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}