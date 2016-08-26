// //-----------------------------------------------------------------------
// // <copyright file="SpecialQualities.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace SilverNeedle.Characters
{
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

        public void ProcessSpecialAbilities(IProvidesSpecialAbilities abilities)
        {
            foreach (var abl in abilities.SpecialAbilities)
            {
                switch (abl.Type)
                {
                    case SpecialAbilityName:
                    case SightAbilityName:
                        specialAbilities.Add(abl);
                        break;
                }
            }
        }
    }
}

