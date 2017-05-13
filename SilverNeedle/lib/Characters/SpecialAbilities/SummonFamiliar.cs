// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Beastiary;

    public class SummonFamiliar : SpecialAbility, IModifiesStats
    {
        public SummonFamiliar(Familiar familiar) 
        {
            this.Name = string.Format("Summon Familiar ({0})", familiar.Name);
            Familiar = familiar;
        }

        public Familiar Familiar { get; set; }

        public IList<IStatModifier> Modifiers { get { return Familiar.Modifiers; } }
    }
}