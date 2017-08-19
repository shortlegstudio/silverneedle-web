// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Appearance
{
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class FaceShape : TemplateSentenceGenerator
    {

        public FaceShape(IObjectStore data) : base(data)
        {
        }

        public FaceShape(string name) : base(name)
        {
        }
    }
}