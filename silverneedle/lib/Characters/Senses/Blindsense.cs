// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Senses
{
    using SilverNeedle.Utility;
    public class Blindsense : ISense, INameByType
    {
        public int Range { get; private set; }
        public Blindsense(int rangeInFeet) 
        {
            this.Range = rangeInFeet;
        }
        public string DisplayString()
        {
            return "{0} {1}ft".Formatted(this.Name(), Range); 
        }
    }
}