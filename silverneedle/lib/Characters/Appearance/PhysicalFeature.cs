// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Appearance
{
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class PhysicalFeature : TemplateSentenceGenerator
    {
        public PhysicalFeature(IObjectStore data) : base(data)
        {
            var defaultTemplate = "{{pronoun}} has a {{description}}.";

            if(Templates == null)
            {
                Templates = new string[] {
                    defaultTemplate
                };
            }
        }
    }
}