// //-----------------------------------------------------------------------
// // <copyright file="ClassOriginGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Characters.Background
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Utility;

    public class ClassOriginGateway : IClassOriginGateway
    {
        private const string ClassOriginDataFileType = "classorigin";
        private List<ClassOrigin> classOrigins = new List<ClassOrigin>();

        public ClassOriginGateway()
        {
            foreach(var y in DatafileLoader.Instance.GetDataFiles(ClassOriginDataFileType)) 
            {
                this.LoadObjects(y);
            }
        }

        public ClassOriginGateway(IObjectStore dataStore)
        {
            LoadObjects(dataStore);
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

        private void LoadObjects(IObjectStore data)
        {
            foreach (var classNode in data.Children)
            {
                var table = classNode.GetObject("table");
                foreach (var entry in table.Children)
                {
                    var origin = new ClassOrigin();
                    origin.Class = classNode.GetString("class");
                    origin.Name = entry.GetString("name");
                    origin.Weighting = entry.GetInteger("weight");
                    origin.Traits.Add(entry.GetListOptional("traits"));
                    origin.Storylines.Add(entry.GetListOptional("storylines"));
                    classOrigins.Add(origin);
                }
            }
        }
    }
}

