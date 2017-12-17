// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Senses
{
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class Darkvision : ISense, INameByType
    {
        public int Range { get; private set; }
        public Darkvision(IObjectStore configuration)
        {
            Range = configuration.GetInteger("range");
        }

        public string DisplayString()
        {
            return "{0} {1}ft".Formatted(this.Name(), Range); 
        }
    }
}