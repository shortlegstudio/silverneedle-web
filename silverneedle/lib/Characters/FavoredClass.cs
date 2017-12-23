// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    public class FavoredClass
    {
        public FavoredClass(string className)
        {
            this.ClassName = className;
        }

        public string ClassName { get; private set; }

        public bool Qualifies(Class characterClass)
        {
            return characterClass.Matches(ClassName);
        }

    }
}