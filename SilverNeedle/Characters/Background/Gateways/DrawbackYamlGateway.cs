// //-----------------------------------------------------------------------
// // <copyright file="DrawbackYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using System.Collections.Generic;
using SilverNeedle;
using SilverNeedle.Yaml;

namespace SilverNeedle.Characters.Background
{
    public class DrawbackYamlGateway : IDrawbackGateway
    {
        private const string DrawbackYamlDataFile = "Data/drawbacks.yml";

        private WeightedOptionTable<Drawback> drawbacks;

        public DrawbackYamlGateway()
            : this(FileHelper.OpenYaml(DrawbackYamlDataFile))
        {
        }

        public DrawbackYamlGateway(YamlNodeWrapper yamlNode)
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

        private void ParseYaml(YamlNodeWrapper yaml)
        {
            drawbacks = new WeightedOptionTable<Drawback>();
            foreach (var node in yaml.Children())
            {
                var drawback = new Drawback();
                drawback.Name = node.GetString("name");
                drawback.Weighting = node.GetInteger("weight");
                drawback.Traits.Add(node.GetCommaStringOptional("traits"));
                drawbacks.AddEntry(drawback, drawback.Weighting);
            }
        }
    }
}

