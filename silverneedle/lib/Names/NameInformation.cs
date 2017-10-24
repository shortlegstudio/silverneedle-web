// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Names
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class NameInformation : IGatewayObject
    {
        public IList<string> Names { get; set; }
        public string Race { get; set; }
        public NameTypes Type { get; set; }
        public string Gender { get; set; }

        public bool Matches(string name)
        {
            return this.Race.EqualsIgnoreCase(name);
        }

        public bool IsFirstName
        {
            get { return Type == NameTypes.First; }
        }

        public bool IsLastName
        {
            get { return Type == NameTypes.Last; }
        }

        public bool IsFemale
        {
            get { return MatchGender("Female"); }
        }

        public bool IsMale
        {
            get { return MatchGender("Male"); }
        }

        public NameInformation()
        {
            Names = new List<string>();
        }

        public NameInformation(IObjectStore data) : this()
        {
            this.Gender = data.GetString("gender");
            this.Type = data.GetEnum<NameTypes>("category");
            this.Race = data.GetString("race");

            var names = data.GetList("names");
            this.Names.Add(names.Where(x => string.IsNullOrEmpty(x) == false));    
        }

        public bool MatchesRace(string race)
        {
            return Race.EqualsIgnoreCase(race);
        }

        public bool MatchesGender(Gender gender)
        {
            return MatchGender(gender.ToString());
        }

        private bool MatchGender(string gender)
        {
            return this.Gender.EqualsIgnoreCase("any") ||
                this.Gender.EqualsIgnoreCase(gender);
        }

        
    }
}

