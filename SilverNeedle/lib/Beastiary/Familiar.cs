// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Beastiary
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;
    public class Familiar : IGatewayObject, IModifiesStats
    {
        public string Name { get; set; }

        public IList<BasicStatModifier> Modifiers { get; private set; }

        public Familiar(IObjectStore data)
        {
            Modifiers = new List<BasicStatModifier>();
            Name = data.GetString("name");
            Modifiers.Load(data.GetObjectOptional("modifiers"), "Familiar");
        } 

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}