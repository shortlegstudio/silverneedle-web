// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Traits
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class BonusFeat : Trait, IComponent
    {
        public BonusFeat() : base() { }
        public BonusFeat(IObjectStore configure) : base(configure) { }

        public void Initialize(ComponentBag components)
        {
            components.Add(
                new FeatToken()
            );
        }
    }
}