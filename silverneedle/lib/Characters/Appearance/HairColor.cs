// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Appearance
{
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class HairColor : TemplateSentenceGenerator
    {

        public HairColor(IObjectStore data) : base(data)
        {
        }

        public HairColor(string name) : base(name)
        {
        }
    }
}