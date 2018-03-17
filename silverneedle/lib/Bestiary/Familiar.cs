// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Bestiary
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    public class Familiar : IGatewayObject, IModifiesStats
    {
        public string Name { get; set; }

        public IList<IValueStatModifier> Modifiers { get; private set; }

        public Familiar()
        {
            Modifiers = new List<IValueStatModifier>();
        }

        public Familiar(string name) : this()
        {
            this.Name = name;
        }

        public Familiar(IObjectStore data) : this()
        {
            Name = data.GetString("name");
            Modifiers.Load(data.GetObjectListOptional("modifiers"), "Familiar");
        } 

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}