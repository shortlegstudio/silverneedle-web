// //-----------------------------------------------------------------------
// // <copyright file="DrawbackGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using System.Collections.Generic;
using SilverNeedle;
using SilverNeedle.Utility;

namespace SilverNeedle.Characters.Background
{
    public class DrawbackGateway : IDrawbackGateway
    {
        private const string DrawbackYamlDataFileType = "drawback";

        private WeightedOptionTable<Drawback> drawbacks = new WeightedOptionTable<Drawback>();

        public DrawbackGateway()
        {
            foreach(var y in DatafileLoader.Instance.GetDataFiles(DrawbackYamlDataFileType))
            {
                ParseYaml(y);
            }
        }

        public DrawbackGateway(IObjectStore yamlNode)
        {
            ParseYaml(yamlNode);
        }

        public WeightedOptionTable<Drawback> GetDrawbacks()
        {
            return drawbacks;
        }

        public Drawback ChooseOne()
        {
            return drawbacks.ChooseRandomly();
        }

        private void ParseYaml(IObjectStore yaml)
        {
            foreach (var node in yaml.Children)
            {
                var drawback = new Drawback();
                drawback.Name = node.GetString("name");
                drawback.Weighting = node.GetInteger("weight");
                drawback.Traits.Add(node.GetListOptional("traits"));
                drawbacks.AddEntry(drawback, drawback.Weighting);
            }
        }
    }
}

