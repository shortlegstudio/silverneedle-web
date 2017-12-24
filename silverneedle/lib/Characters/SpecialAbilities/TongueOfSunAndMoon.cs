// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class TongueOfSunAndMoon : IComponent
    {
        public void Initialize(ComponentContainer components)
        {
            var tongue = new Language("Tongue of Sun and Moon", "Can speak to any living thing");
            components.Add(tongue);
        }
    }
}