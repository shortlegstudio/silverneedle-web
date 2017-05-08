// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    public class ClassLevel
    {
        public Class Class { get; private set; }
        public int Level { get; set; }

        public ClassLevel(Class cls)
        {
            this.Class = cls;
            this.Level = 1;
        }
    }
}