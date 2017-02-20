// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Utility;


    public class PersonalityType
    {
        public PersonalityTypes.Attitude Attitude { get; private set; }
        public PersonalityTypes.Information InformationProcessing { get; private set; }
        public PersonalityTypes.DecisionMaking DecisionMaking { get; private set; }
        public PersonalityTypes.Structure Structure { get; private set; }
        public IEnumerable<string> Descriptors { get; private set; }
        
        public PersonalityType(string type) {
            ParseType(type);
        }

        public PersonalityType(IObjectStore data) 
        {
            ParseType(data.GetString("type"));         
            Descriptors = data.GetList("descriptors");               
        }

        private void ParseType(string type) {
            Attitude = (PersonalityTypes.Attitude)type[0];
            InformationProcessing = (PersonalityTypes.Information)type[1];
            DecisionMaking = (PersonalityTypes.DecisionMaking)type[2];
            Structure = (PersonalityTypes.Structure)type[3];
        }
    }
}