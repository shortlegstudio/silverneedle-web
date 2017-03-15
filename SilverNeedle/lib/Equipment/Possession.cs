// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public class Possession : IGear
    {
        private IGear reference;

        public string Name { get { return reference.Name; } }

        public float Weight { get { return reference.Weight; } }

        public int Value { get { return reference.Value; } }

        public bool GroupSimilar { get { return reference.GroupSimilar; } }

        public int Quantity { get; set; }

        public Possession(IGear gear)
        {
            reference = gear;
            Quantity = 1;
        } 

        public void IncrementQuantity()
        {
            Quantity++;
        }

    }
}