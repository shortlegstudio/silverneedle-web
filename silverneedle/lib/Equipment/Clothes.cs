// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using SilverNeedle.Serialization;

    public class Clothes : Gear
    {
        public Clothes(IObjectStore data) : base(data)
        {
        }

        public Clothes(string name, int value, float weight) : base(name, value, weight)
        {
        }
    }
}