// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    public class DomainSpellCasting : SpellCasting
    {

        public DomainSpellCasting(Inventory inventory, ClassLevel sourceClass) : base(inventory, sourceClass)
        {
            
        }
        public override int GetSpellsPerDay(int level)
        {
            if(level <= (this.CasterLevel + 1) / 2)
                return 1;
            return 0;
        }
    }
}