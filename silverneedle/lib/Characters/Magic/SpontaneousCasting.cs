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
    using SilverNeedle.Utility;

    public class SpontaneousCasting : SpellCasting
    {
        public SpontaneousCasting(IObjectStore configuration) : base(configuration)
        {
        }
        
        public override SpellsKnown SpellsKnown
        {
            get { return SpellsKnown.Spontaneous; }
        }
        
    }
}