// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    public class ClassFeature : FeatureAttribute
    {
        public ClassFeature(IObjectStore configuration) : base(configuration)
        {
        }
    }
}