// //-----------------------------------------------------------------------
// // <copyright file="RaceMaturityYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle;
using System.Collections.Generic;
using SilverNeedle.Dice;
using System.Linq;
using SilverNeedle.Utility;



namespace SilverNeedle.Characters
{
    public class RaceMaturityGateway : IRaceMaturityGateway
    {
        private const string MaturityDataFileType = "maturity";
        private List<Maturity> maturities = new List<Maturity>();

        public RaceMaturityGateway()
        {
            var dataFiles = DatafileLoader.Instance.GetDataFiles(MaturityDataFileType);
            foreach(var y in dataFiles) 
            {
                LoadObjects(y);
            }
        }

        public RaceMaturityGateway(IObjectStore dataStore)
        {
            LoadObjects(dataStore);
        }

        public System.Collections.Generic.IEnumerable<Maturity> All()
        {
            return maturities;
        }

        public Maturity Get(Race race)
        {
            return All().FirstOrDefault(x => string.Compare(x.Name, race.Name, true) == 0);
        }

        private void LoadObjects(IObjectStore dataStore) 
        {
            foreach (var mat_node in dataStore.Children)
            {
                var node = mat_node.GetObject("maturity");
                var maturity = new Maturity();
                maturity.Name = node.GetString("race");
                maturity.Adulthood = node.GetInteger("adulthood");
                maturity.Young = DiceStrings.ParseDice(node.GetString("young"));
                maturity.Trained = DiceStrings.ParseDice(node.GetString("trained"));
                maturity.Studied = DiceStrings.ParseDice(node.GetString("studied"));
                maturities.Add(maturity);
            }
        }
    }
}

