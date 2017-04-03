// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.General
{
    using SilverNeedle.Serialization;

    public class Color : IGatewayObject
    {
        [ObjectStore("id")]
        public string ID { get; set; }

        [ObjectStore("name")]
        public string Name { get; set; }

        [ObjectStore("hex")]
        public string Hex { get; set; }

        [ObjectStore("red")]
        public int Red { get; set; }

        [ObjectStore("green")]
        public int Green { get; set; }
        
        [ObjectStore("blue")]
        public int Blue { get; set; }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}