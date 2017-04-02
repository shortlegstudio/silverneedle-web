// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Beastiary
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    public class Familiar : IGatewayObject, IModifiesStats
    {
        public string Name { get; set; }

        public IList<BasicStatModifier> Modifiers { get; private set; }

        public Familiar()
        {
            Modifiers = new List<BasicStatModifier>();
        }

        public Familiar(string name) : this()
        {
            this.Name = name;
        }

        public Familiar(IObjectStore data) : this()
        {
            Name = data.GetString("name");
            Modifiers.Load(data.GetObjectOptional("modifiers"), "Familiar");
        } 

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}