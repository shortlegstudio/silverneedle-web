// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Beastiary;

    public class SummonFamiliar : LevelAbility
    {
        public SummonFamiliar(Familiar familiar) 
        {
            this.Name = string.Format("Summon Familiar ({0})", familiar.Name);
            Familiar = familiar;
        }

        public Familiar Familiar { get; set; }
    }
}