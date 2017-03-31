// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    public class SpecialQualities
    {
        private const string SpecialAbilityName = "Ability";

        private const string SightAbilityName = "Sight";

        private List<SpecialAbility> specialAbilities;

        public SpecialQualities()
        {
            specialAbilities = new List<SpecialAbility>();
        }

        public IEnumerable<SpecialAbility> SpecialAbilities
        {
            get
            {
                return specialAbilities;
            }
        }

        public IEnumerable<SpecialAbility> SightAbilities
        {
            get
            {
                return specialAbilities.Where(x => string.Equals(x.Type, SightAbilityName));
            }
        }

        public void Add(SpecialAbility ability)
        {
            specialAbilities.Add(ability);
        }

        public void ProcessSpecialAbilities(IProvidesSpecialAbilities abilities)
        {
            foreach (var abl in abilities.SpecialAbilities)
            {
                Add(abl);
            }
        }
    }
}

