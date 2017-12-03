// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
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
    }
}