// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    
    public class DomainSpellCasting : DivineCasting
    {

        public DomainSpellCasting(ClassLevel sourceClass) : base(sourceClass, "domain")
        {
            this.SpellsKnown = SilverNeedle.Spells.SpellsKnown.Domain;
        }
        public override int GetSpellsPerDay(int level)
        {
            if(level == 0) return 0;
            if(level <= MaxLevel)
                return 1;
            return 0;
        }

        public override IEnumerable<string> GetAvailableSpells(int level)
        {
            if(GetSpellsPerDay(level) > 0)
                return base.GetAvailableSpells(level); 

            return new string[] { };
        }

        public override int MaxLevel
        {
            get { return System.Math.Min(9, (this.CasterLevel + 1) / 2); }
        }
    }
}