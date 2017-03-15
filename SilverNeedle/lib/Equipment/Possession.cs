// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public class Possession : IGear
    {
        public IGear ReferenceObject { get; private set; }

        public string Name { get { return ReferenceObject.Name; } }

        public float Weight { get { return ReferenceObject.Weight; } }

        public int Value { get { return ReferenceObject.Value; } }

        public bool GroupSimilar { get { return ReferenceObject.GroupSimilar; } }

        public bool IsEquipped { get; set; }

        public int Quantity { get; set; }

        public Possession(IGear gear)
        {
            ReferenceObject = gear;
            Quantity = 1;
        } 

        public void IncrementQuantity()
        {
            Quantity++;
        }

    }
}