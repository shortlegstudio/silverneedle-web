// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Characters
{
    public class CharacterBuildStrategy
    {
        public CharacterBuildStrategy()
        {
            Classes = new WeightedOptionTable<string>();
            Races = new WeightedOptionTable<string>();
            FavoredSkills = new WeightedOptionTable<string>();
        }
        
        public string Name { get; set; }
        public WeightedOptionTable<string> Classes { get; set; }
        public WeightedOptionTable<string> Races { get; set; }

        public float ClassSkillMultiplier { get; set; }

        public int BaseSkillWeight { get; set; }

        public WeightedOptionTable<string> FavoredSkills { get; set; }
    }
}