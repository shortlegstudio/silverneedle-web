// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Prerequisites
{
    using System.Linq;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Utility;

    public class CasterLevelPrerequisite : IPrerequisite
    {
        public CasterLevelPrerequisite(string casterLevel)
        {
            this.CasterLevel = casterLevel.ToInteger();
        }

        public int CasterLevel { get; private set; }

        public bool IsQualified(ComponentContainer components)
        {
            var spellcasting = components.GetAll<ISpellCasting>();
            if(spellcasting.Empty())
                return false;

            return spellcasting.Any(x => x.CasterLevel >= this.CasterLevel);
        }
    }

}