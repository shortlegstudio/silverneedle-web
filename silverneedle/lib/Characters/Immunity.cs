// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    public class Immunity  : IResistance
    {
        public string DamageType { get; private set; }
        public bool IsImmune { get { return true; } }
        public Immunity(string immunity)
        {
            DamageType = immunity;
        }

        public override string ToString()
        {
            return "{0} Immunity".Formatted(DamageType);
        }
    }
}