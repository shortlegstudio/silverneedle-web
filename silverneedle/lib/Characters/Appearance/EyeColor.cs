// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Appearance
{
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;
    public class EyeColor : TemplateSentenceGenerator
    {
        public EyeColor(IObjectStore data) : base(data)
        {
        }

        public EyeColor(string name) : base(name)
        {
        }
    }
}