// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;
    public class ClassLevelPrerequisite : IPrerequisite
    {
        public ClassLevelPrerequisite(string value)
        {
            var vals = value.Split(' ');
            this.Class = vals[0];
            this.Minimum = int.Parse(vals[1]);
        }

        public string Class { get; set; }

        public int Minimum { get; set; }

        public bool IsQualified(ComponentContainer components)
        {
            var cls = components.Get<ClassLevel>();
            if(cls == null)
                return false;

            return cls.Class.Matches(this.Class) &&
                cls.Level >= this.Minimum;
        }
    }
}