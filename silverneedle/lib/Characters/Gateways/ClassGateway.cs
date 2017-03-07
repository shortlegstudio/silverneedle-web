//-----------------------------------------------------------------------
// <copyright file="ClassYamlGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    /// <summary>
    /// Class yaml gateway.
    /// </summary>
    public class ClassGateway : IClassGateway
    {
        /// <summary>
        /// The yaml file holding class data
        /// </summary>
        private const string ClassDataFileType = "class";

        /// <summary>
        /// The classes that are loaded
        /// </summary>
        private IList<Class> classes = new List<Class>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.ClassYamlGateway"/> class.
        /// </summary>
        /// <param name="yaml">Yaml data.</param>
        public ClassGateway(IObjectStore dataStore)
        {
            this.classes.Add(LoadObjects(dataStore));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.Gateways.ClassYamlGateway"/> class.
        /// </summary>
        public ClassGateway()
        {
            // Use DatafileLoader to get all class files;
            var yamlNodes = DatafileLoader.Instance.GetDataFiles(ClassDataFileType);
            foreach(var y in yamlNodes) {
                this.classes.Add(LoadObjects(y));
            }
        }

        /// <summary>
        /// Should return all of the classes
        /// </summary>
        /// <returns>Enumerable collection of the classes</returns>
        public IEnumerable<Class> All()
        {
            return this.classes;
        }

        public Class GetByName(string name) 
        {
            return this.classes.First(
                x => string.Equals(name, x.Name, StringComparison.OrdinalIgnoreCase)
            );
        }

        /// <summary>
        /// Loads from yaml.
        /// </summary>
        /// <returns>The from yaml.</returns>
        /// <param name="yaml">Yaml data to load from.</param>
        private IList<Class> LoadObjects(IObjectStore yaml)
        {
            var classes = new List<Class>();

            foreach (var node in yaml.Children)
            {
                var cls = new Class(node);
                classes.Add(cls);
            }

            return classes;
        }
    }
}