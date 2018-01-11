// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Utility;

    public class SummonFamiliar : IAbility, IComponent
    {
        public SummonFamiliar(Familiar familiar) 
        {
            Familiar = familiar;
        }

        public Familiar Familiar { get; set; }

        public void Initialize(ComponentContainer components)
        {
            foreach(var mod in Familiar.Modifiers)
            {
                components.Add(mod);
            }
        }

        public string DisplayString()
        {
            return string.Format("Summon Familiar ({0})", Familiar.Name);
        }
    }
}