// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;

    /// <summary>
    /// A trait is some basic innate attribute of the character. Usually positive
    /// </summary>
    public class Trait : FeatureAttribute, ITrait
    {
        public Trait(IObjectStore configuration) : base(configuration)
        {
        }
    }
}