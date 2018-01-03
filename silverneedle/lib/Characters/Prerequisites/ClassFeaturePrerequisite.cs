// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;
    public class ClassFeaturePrerequisite : IPrerequisite
    {
        public ClassFeaturePrerequisite(string value)
        {
            this.ClassFeature = value;
        }

        public string ClassFeature { get; set; }

        public bool IsQualified(ComponentContainer components)
        {
            return false;
        }
    }

}