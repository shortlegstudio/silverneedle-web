// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;

    public class RacePrerequisite : IPrerequisite
    {
        public RacePrerequisite(string value)
        {
            this.Race = value;
        }

        public string Race { get; set; }

        public bool IsQualified(ComponentContainer components)
        {
            var race = components.Get<Race>();
            if(race == null)
                return false;
            
            return race.Matches(this.Race);
        }
    }
}