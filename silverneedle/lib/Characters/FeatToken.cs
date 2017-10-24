// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an option to select a feat. Allows for setting specific restrictions on 
    /// what kind of feats are available to be chosen.
    /// </summary>
    public class FeatToken
    {
        private IList<string> tags = new List<string>();
        public FeatToken(string tag)
        {
            if(!string.IsNullOrEmpty(tag)) {
                tags.Add(tag);
            }
        }

        public FeatToken(IEnumerable<string> options)
        {
            this.tags.Add(options);
        }

        public FeatToken(IEnumerable<string> options, bool ignorePrerequisites) : this(options)
        {
            this.IgnorePrerequisites = ignorePrerequisites;
        }

        public FeatToken() { }
        public IEnumerable<string> Tags { get { return this.tags; } }
        public bool IgnorePrerequisites { get; private set; }

        public bool Qualifies(Feat feat) 
        {
            //Empty list always is true
            if (tags.Count == 0)
                return true;

                            
            return tags.Any(x => feat.Tags.Contains(x) || feat.Name.EqualsIgnoreCase(x));
        }

        public override string ToString()
        {
            return string.Format("FeatToken({0})", string.Join(",", tags));
        }
    }
}