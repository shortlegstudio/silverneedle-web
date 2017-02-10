// //-----------------------------------------------------------------------
// // <copyright file="CharacterNamesGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Names
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class CharacterNamesGateway : ICharacterNamesGateway
    {
        private const string CharacterNamesDataFileType = "names";
        private IList<NameInformation> namesDatabase = new List<NameInformation>();

        public CharacterNamesGateway() 
        {
            foreach(var y in DatafileLoader.Instance.GetDataFiles(CharacterNamesDataFileType))
            {
                this.LoadObjects(y);
            }
        }

        public CharacterNamesGateway(IObjectStore data)
        {
            LoadObjects(data);
        }

        public IList<string> GetFirstNames()
        {
            return namesDatabase
                .Where(x => x.Type == NameTypes.First)
                .SelectMany(x => x.Names)
                .ToList();
        }

        public IList<string> GetFirstNames(Gender gender, string race)
        {
            var genderString = gender.ToString();

            return namesDatabase
                .Where(x => x.Type == NameTypes.First && 
                    MatchRace(race, x) && 
                    MatchGender(genderString, x))
                .SelectMany(x => x.Names)
                .ToList();
        }

        public IList<string> GetLastNames()
        {
            return namesDatabase
                .Where(x => x.Type == NameTypes.Last)
                .SelectMany(x => x.Names)
                .ToList();
        }

        public IList<string> GetLastNames(string race)
        {
            return namesDatabase
                .Where(x => x.Type == NameTypes.Last &&
                    MatchRace(race, x))
                .SelectMany(x => x.Names)
                .ToList();
        }

        private bool MatchGender(string gender, NameInformation names)
        {
            return string.Equals(names.Gender, "any", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(names.Gender, gender, StringComparison.OrdinalIgnoreCase);
        }

        private bool MatchRace(string race, NameInformation names)
        {
            return string.Equals(race, names.Race, StringComparison.OrdinalIgnoreCase);
        }

        private void LoadObjects(IObjectStore data)
        {
            foreach (var node in data.Children)
            {
                var name = new NameInformation();
                name.Gender = node.GetString("gender");
                name.Type = node.GetEnum<NameTypes>("category");
                name.Race = node.GetString("race");
                var names = node.GetList("names");

                name.Names.Add(
                    names.Where(x => string.IsNullOrEmpty(x) == false));
                namesDatabase.Add(name);
            }
        }
    }
}

