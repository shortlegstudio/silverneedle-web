// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using SilverNeedle.Utility;
    public class Ideal : IGatewayObject
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Ideal(IObjectStore data)
        {
            Name = data.GetString("name");
            Description = data.GetString("description");
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}