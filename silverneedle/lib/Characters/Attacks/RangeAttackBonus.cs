// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Attacks
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class RangeAttackBonus : BasicStat, IComponent
    {
        public ComponentContainer Parent { get; set; }

        public RangeAttackBonus(IObjectStore configuration) : base(configuration) { }

        public void Initialize(ComponentContainer components)
        {
            var sizeStats = components.Get<SizeStats>();
            AddModifier(sizeStats.PositiveSizeModifier);
        }
    }
}