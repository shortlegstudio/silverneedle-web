// //-----------------------------------------------------------------------
// // <copyright file="ClassOriginYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using SilverNeedle.Yaml;

namespace SilverNeedle.Characters.Background
{
    using SilverNeedle;

    public class ClassOriginYamlGateway : IClassOriginYamlGateway
    {
        private const string ClassOriginYamlDataFile = "Data/classorigins.yml";
        private List<ClassOrigin> classOrigins;

        public ClassOriginYamlGateway()
            : this(FileHelper.OpenYaml(ClassOriginYamlDataFile))
        {
        }

        public ClassOriginYamlGateway(YamlNodeWrapper yaml)
        {
            ParseYaml(yaml);
        }

        public ClassOrigin ChooseOne(string cls)
        {
            return GetClassOriginOptions(cls).ChooseRandomly();
        }

        public WeightedOptionTable<ClassOrigin> GetClassOriginOptions(string cls)
        {
            var table = new WeightedOptionTable<ClassOrigin>();
            var origins = classOrigins.Where(x => string.Equals(x.Class, cls, StringComparison.OrdinalIgnoreCase));
            foreach (var origin in origins)
            {
                table.AddEntry(origin, origin.Weighting);
            }
            return table;
        }

        private void ParseYaml(YamlNodeWrapper yaml)
        {
            classOrigins = new List<ClassOrigin>();
            foreach (var classNode in yaml.Children())
            {
                var table = classNode.GetNode("table");
                foreach (var entry in table.Children())
                {
                    var origin = new ClassOrigin();
                    origin.Class = classNode.GetString("class");
                    origin.Name = entry.GetString("name");
                    origin.Weighting = entry.GetInteger("weight");
                    origin.Traits.Add(entry.GetCommaStringOptional("traits"));
                    origin.Storylines.Add(entry.GetCommaStringOptional("storylines"));
                    classOrigins.Add(origin);
                }
            }
        }
    }
}

