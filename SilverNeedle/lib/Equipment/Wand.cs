// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;

namespace SilverNeedle.Equipment
{
    using SilverNeedle.Spells;
    public class Wand : IWand
    {
        public string Name { get; private set; }

        public float Weight { get; private set; }

        public int Value { get; private set; }

        public bool GroupSimilar { get { return false; } }

        public int Charges { get; set; }
        public Spell Spell { get; private set; }

        public Wand(Spell spell, int charges, int value)
        {
            this.Spell = spell;
            this.Charges = charges;
            this.Value = value;
            this.Name = string.Format("Wand of {0}", spell.Name);
        }
    }
}