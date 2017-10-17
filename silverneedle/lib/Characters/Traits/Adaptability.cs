// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Traits
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Adaptability : Trait, IComponent
    {
        public Adaptability() : base() { }
        public Adaptability(IObjectStore configure) : base(configure) { }

        public void Initialize(ComponentBag components)
        {
            var token = new FeatToken("Skill Focus");
            components.Add(token);
        }
    }
}