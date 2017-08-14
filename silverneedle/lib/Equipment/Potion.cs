// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using SilverNeedle.Spells;

namespace SilverNeedle.Equipment
{
    public class Potion : IPotion
    {
        public Potion(Spell spell, int value)
        {
            this.Spell = spell;
            this.Value = value;
        }
        public Spell Spell { get; private set; }

        public string Name { get { return string.Format("Potion of {0}", Spell.Name); } }

        public float Weight { get { return .1f; } }

        public int Value { get; private set; }

        public bool GroupSimilar { get { return true; } }
    }
}