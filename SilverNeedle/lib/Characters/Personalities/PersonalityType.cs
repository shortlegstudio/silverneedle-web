// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    public class PersonalityType
    {
        public PersonalityTypes Attitude { get; private set; }
        public PersonalityTypes InformationProcessing { get; private set; }
        public PersonalityTypes DecisionMaking { get; private set; }
        public PersonalityTypes Structure { get; private set; }
        
        public PersonalityType(string type) {
            Attitude = (PersonalityTypes)type[0];
            InformationProcessing = (PersonalityTypes)type[1];
            DecisionMaking = (PersonalityTypes)type[2];
            Structure = (PersonalityTypes)type[3];
        }
    }
}