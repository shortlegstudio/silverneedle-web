// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    public class PersonalityType : IGatewayObject
    {
        public PersonalityTypes.Attitude Attitude { get; private set; }
        public PersonalityTypes.Information InformationProcessing { get; private set; }
        public PersonalityTypes.DecisionMaking DecisionMaking { get; private set; }
        public PersonalityTypes.Structure Structure { get; private set; }
        public IEnumerable<string> Descriptors { get; private set; }

        public IEnumerable<string> Weaknesses { get; private set; }

        public string Type { get; private set; }
        
        public PersonalityType(string type) {
            ParseType(type);
        }

        public PersonalityType(IObjectStore data) 
        {
            ParseType(data.GetString("type"));         
            Descriptors = data.GetList("descriptors");     
            Weaknesses = data.GetList("weaknesses");          
        }

        private void ParseType(string type) {
            Attitude = (PersonalityTypes.Attitude)type[0];
            InformationProcessing = (PersonalityTypes.Information)type[1];
            DecisionMaking = (PersonalityTypes.DecisionMaking)type[2];
            Structure = (PersonalityTypes.Structure)type[3];
        }

        public bool Matches(string name)
        {
            return Type.Equals(name);
        }
    }
}