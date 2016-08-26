// //-----------------------------------------------------------------------
// // <copyright file="SpecialAbilities.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;

namespace SilverNeedle.Characters
{
    public class SpecialAbility
    {
        public SpecialAbility()
        {
        }

        public SpecialAbility(string condition, string type)
        {
            this.Condition = condition;
            this.Type = type;
        }

        public string Condition { get; set; }
        public string Type { get; set; }
    }
}

