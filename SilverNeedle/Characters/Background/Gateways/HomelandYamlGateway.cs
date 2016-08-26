// //-----------------------------------------------------------------------
// // <copyright file="HomelandYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using System.Collections.Generic;
using System.Linq;
using SilverNeedle;
using SilverNeedle.Yaml;

namespace SilverNeedle.Characters.Background
{
    public class HomelandYamlGateway : IHomelandGateway
    {
        private const string HomelandYamlDataFile = "Data/homelands.yml";
        private IList<Homeland> homelands;

        public HomelandYamlGateway() : this(FileHelper.OpenYaml(HomelandYamlDataFile))
        {
            
        }

        public HomelandYamlGateway(YamlNodeWrapper yaml)
        {
            ParseYaml(yaml);
        }

        public WeightedOptionTable<Homeland> GetRacialOptions(string race)
        {
            var table = new WeightedOptionTable<Homeland>();
            var options = homelands.Where(x => string.Equals(x.Race, race, StringComparison.OrdinalIgnoreCase));
            foreach (var opt in options)
            {
                table.AddEntry(opt, opt.Weighting);
            }
            return table;
        }

        private void ParseYaml(YamlNodeWrapper yaml)
        {
            homelands = new List<Homeland>();
            foreach (var node in yaml.Children())
            {
                var table = node.GetNode("table");
                foreach (var entry in table.Children())
                {
                    var homeland = new Homeland();
                    homeland.Race = node.GetString("race");
                    homeland.Location = entry.GetString("location");
                    homeland.Weighting = entry.GetInteger("weight");
                    homeland.Traits.Add(entry.GetCommaStringOptional("traits"));
                    homelands.Add(homeland);
                }
            }
        }
    }
}

