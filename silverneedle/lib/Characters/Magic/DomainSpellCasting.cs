// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    public class DomainSpellCasting : SpellCasting
    {

        public DomainSpellCasting(Inventory inventory, ClassLevel sourceClass) : base(inventory, sourceClass, "domain")
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

        public override string[] GetAvailableSpells(int level)
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