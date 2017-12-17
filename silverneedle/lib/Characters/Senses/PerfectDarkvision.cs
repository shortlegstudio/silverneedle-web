// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Senses
{
    public class PerfectDarkvision : INameByType, ISense
    {
        public int Range { get; private set; }
        public PerfectDarkvision(int rangeInFeet) 
        {
            this.Range = rangeInFeet;
        }
        public string DisplayString()
        {
            return "{0} {1}ft".Formatted(this.Name(), Range); 
        }
    }
}