// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Appearance
{
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;
    public class FacialHair : TemplateSentenceGenerator
    {
        public FacialHair(IObjectStore data) : base(data)
        {
        }

        public FacialHair(string name) : base(name)
        {
        }
    }
}