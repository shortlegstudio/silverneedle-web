// //-----------------------------------------------------------------------
// // <copyright file="HomelandGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using System.Collections.Generic;
using System.Linq;
using SilverNeedle;
using SilverNeedle.Utility;

namespace SilverNeedle.Characters.Background
{
    public class HomelandGateway : IHomelandGateway
    {
        private const string HomelandYamlDataFileType = "homeland";
        private IList<Homeland> homelands = new List<Homeland>();

        public HomelandGateway()
        {
            foreach(var y in DatafileLoader.Instance.GetDataFiles(HomelandYamlDataFileType))
            {
                this.LoadObjects(y);
            }
        }

        public HomelandGateway(IObjectStore yaml)
        {
            LoadObjects(yaml);
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

        private void LoadObjects(IObjectStore yaml)
        {
            foreach (var node in yaml.Children)
            {
                var table = node.GetObject("table");
                foreach (var entry in table.Children)
                {
                    var homeland = new Homeland();
                    homeland.Race = node.GetString("race");
                    homeland.Location = entry.GetString("location");
                    homeland.Weighting = entry.GetInteger("weight");
                    homeland.Traits.Add(entry.GetListOptional("traits"));
                    homelands.Add(homeland);
                }
            }
        }
    }
}

