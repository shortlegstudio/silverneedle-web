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
using SilverNeedle.Yaml;



namespace SilverNeedle.Characters
{
    public class RaceMaturityYamlGateway : IRaceMaturityGateway
    {
        private const string MaturityDataFileType = "maturity";
        private List<Maturity> maturities = new List<Maturity>();

        public RaceMaturityYamlGateway()
        {
            var yamlFiles = DatafileLoader.Instance.GetYamlFiles(MaturityDataFileType);
            foreach(var y in yamlFiles) 
            {
                ParseYaml(y);
            }
        }

        public RaceMaturityYamlGateway(YamlNodeWrapper yaml)
        {
            ParseYaml(yaml);
        }

        public System.Collections.Generic.IEnumerable<Maturity> All()
        {
            return maturities;
        }

        public Maturity Get(Race race)
        {
            return All().FirstOrDefault(x => string.Compare(x.Name, race.Name, true) == 0);
        }

        private void ParseYaml(YamlNodeWrapper yaml) 
        {
            foreach (var mat_node in yaml.Children())
            {
                var node = mat_node.GetNode("maturity");
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

