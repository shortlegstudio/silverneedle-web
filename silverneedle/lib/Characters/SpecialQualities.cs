// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    public class SpecialQualities : IComponent
    {
        private const string SpecialAbilityName = "Ability";

        private const string SightAbilityName = "Sight";

        private ComponentBag components;

        public IEnumerable<SpecialAbility> SpecialAbilities
        {
            get
            {
                return components.GetAll<SpecialAbility>();
            }
        }

        public IEnumerable<SpecialAbility> SightAbilities
        {
            get
            {
                return this.SpecialAbilities.Where(x => string.Equals(x.Type, SightAbilityName));
            }
        }

        public void Initialize(ComponentBag components)
        {
            this.components = components;
        }
    }
}

