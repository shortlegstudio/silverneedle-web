// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;

    public class WizardCasting : SpellbookCasting
    {
        public WizardCasting(IObjectStore configuration) : base(configuration)
        {

        }

        public WizardCasting(IObjectStore configuration, EntityGateway<SpellList> spellLists) : base(configuration, spellLists)
        {

        }

        public IArcaneSchool FocusSchool { get; private set; }
        public IEnumerable<IArcaneSchool> OppositionSchools { get { return oppositionSchools; } } 
        private IList<IArcaneSchool> oppositionSchools = new List<IArcaneSchool>();

        public void SetFocusSchool(IArcaneSchool school)
        {
            this.FocusSchool = school;
        }

        public void SetOppositionSchools(IEnumerable<IArcaneSchool> schools)
        {
            this.oppositionSchools = schools.ToList();
        }
    }
}