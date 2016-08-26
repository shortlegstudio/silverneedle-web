// //-----------------------------------------------------------------------
// // <copyright file="YamlNodeWrapper.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
namespace SilverNeedle.Yaml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using YamlDotNet.RepresentationModel;

    /// <summary>
    /// A wrapper around the YamlNodes that abstracts away complexities of the Yaml format
    /// </summary>
    public class YamlNodeWrapper
    {
        /// <summary>
        /// Has a value if node is a sequence node
        /// </summary>
        private YamlSequenceNode sequenceNode;

        /// <summary>
        /// Has a value if the node is a scalar
        /// </summary>
        private YamlScalarNode scalarNode;

        /// <summary>
        /// Has a value if the mode maps to other nodes
        /// </summary>
        private YamlMappingNode mappingNode;

        /// <summary>
        /// Gets the node for the YAML 
        /// </summary>
        /// <value>The node.</value>
        private YamlNode node;

        /// <summary>
        /// What to parse for true when looking at text in a YAML document
        /// </summary>
        private string booleanTrueString = "yes";

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.YamlNodeWrapper"/> class.
        /// </summary>
        /// <param name="wrap">YamlNode to wrap.</param>
        public YamlNodeWrapper(YamlNode wrap)
        {
            this.node = wrap;
            this.sequenceNode = this.node as YamlSequenceNode;
            this.scalarNode = this.node as YamlScalarNode;
            this.mappingNode = this.node as YamlMappingNode;
        }

        /// <summary>
        /// Gets the current scalar value associated with this node
        /// </summary>
        /// <value>The value as a string</value>
        public string Value
        { 
            get
            {
                if (this.scalarNode != null)
                {
                    return this.scalarNode.Value;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the key for this noe
        /// </summary>
        /// <value>The key of this node</value>
        public string Key
        {
            get
            {
                if (this.scalarNode != null)
                {
                    return this.scalarNode.Tag;
                }

                return null;
            }
        }

        /// <summary>
        /// Determines whether this instance has children.
        /// </summary>
        /// <returns><c>true</c> if this instance has children; otherwise, <c>false</c>.</returns>
        public bool HasChildren()
        {
            return this.sequenceNode != null && this.sequenceNode.Children.Count > 0;
        }

        /// <summary>
        /// Children found in this instance of YAML
        /// </summary>
        /// <returns>Returns the children of this node if there are any</returns>
        public IList<YamlNodeWrapper> Children()
        {
            return this.sequenceNode.Children.Select(x => new YamlNodeWrapper(x)).ToList();
        }

        /// <summary>
        /// Gets a string value from the node based on the key. 
        /// Note: Throws exception if key is not found
        /// </summary>
        /// <returns>The string value of the key</returns>
        /// <param name="key">Key to lookup</param>
        public string GetString(string key)
        {
            var item = this.GetNode(key);
            return item.Value;
        }

        /// <summary>
        /// Gets the string based on a key optionally.
        /// </summary>
        /// <returns>The string of the key or null if key is not found</returns>
        /// <param name="key">Key to lookup in node</param>
        public string GetStringOptional(string key)
        {
            var item = this.GetNodeOptional(key);
            if (item != null)
            {
                return item.Value;
            }

            return null;
        }

        /// <summary>
        /// Translates a key that contains commma separated values into a string array
        /// </summary>
        /// <returns>The string array split and trimmed around commas. 
        /// Returns an empty array if key is not found </returns>
        /// <param name="key">Key to the comma delimited string</param>
        public string[] GetCommaStringOptional(string key)
        {
            var val = this.GetStringOptional(key);
            if (val != null)
            {
                return Regex.Split(val, "\\s*,\\s*");
            }

            return new string[] { };
        }

        /// <summary>
        /// Translates a key that contains commma separated values into a string array
        /// Throws exception if node is not found
        /// </summary>
        /// <returns>The string array split and trimmed around commas. 
        /// Returns an empty array if key is not found </returns>
        /// <param name="key">Key to the comma delimited string</param>
        public string[] GetCommaString(string key)
        {
            var val = this.GetString(key);
            if (val != null)
            {
                return Regex.Split(val, "[\\n ,]+");
            }

            return new string[] { };
        }

        /// <summary>
        /// Gets a boolean value from the YAML document. 
        /// Boolean must match "yes" to be true
        /// </summary>
        /// <returns><c>true</c>, if bool optional was gotten, <c>false</c> otherwise.</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public bool GetBoolOptional(string key)
        {
            return this.GetStringOptional(key) == this.booleanTrueString;
        }

        /// <summary>
        /// Gets the integer optional.
        /// </summary>
        /// <returns>The integer value found, 0 otherwise.</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public int GetIntegerOptional(string key)
        {
            var v = this.GetStringOptional(key);
            if (v == null)
            {
                return 0;
            }

            return int.Parse(v);
        }

        /// <summary>
        /// Gets an integer from the YAML document. Throws exception if not found
        /// </summary>
        /// <returns>The integer value</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public int GetInteger(string key)
        {
            return int.Parse(this.GetString(key));
        }

        /// <summary>
        /// Gets a float from the YAML document. Throws exception if not found
        /// </summary>
        /// <returns>The float value from the key</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public float GetFloat(string key)
        {
            return float.Parse(this.GetString(key));
        }

        /// <summary>
        /// Gets an enum value from the YAML node. Throws exception if not found
        /// Ignores Case
        /// </summary>
        /// <returns>The enum value parsed out</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        /// <typeparam name="T">The enum type to parse</typeparam>
        public T GetEnum<T>(string key)
        {
            return (T)Enum.Parse(typeof(T), this.GetStringOptional(key), true);
        }

        /// <summary>
        /// Gets a wrapper node based on key for traversing trees. Throws exception if not found
        /// </summary>
        /// <returns>The node from the key</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public YamlNodeWrapper GetNode(string key)
        {
            try
            {
                var item = this.mappingNode.Children[new YamlScalarNode(key)];
                return new YamlNodeWrapper(item);
            }
            catch
            {
                ShortLog.ErrorFormat("Yaml Node not found: {0}", key);
                throw;
            }
        }

        /// <summary>
        /// Gets the node optional.
        /// </summary>
        /// <returns>The node optional.</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public YamlNodeWrapper GetNodeOptional(string key)
        {
            try
            {
                var item = this.mappingNode.Children[new YamlScalarNode(key)];
                return new YamlNodeWrapper(item);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Maps children elements into a dictionary.
        /// </summary>
        /// <returns>The dictionary to map elements into</returns>
        public IDictionary<string, string> ChildrenToDictionary()
        {
            var results = new Dictionary<string, string>(); 
            if (this.mappingNode != null)
            {
                foreach (var item in this.mappingNode.Children)
                {
                    results.Add(
                        item.Key.ToString(), 
                        item.Value.ToString());
                }
            }

            return results;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.YamlNodeWrapper"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.YamlNodeWrapper"/>.</returns>
        public override string ToString()
        {
            return string.Format(
                "[YamlNodeWrapper: Node={0}, Value={1}, Key={2}, HasChildren={3}]", 
                this.node, 
                this.Value, 
                this.Key, 
                this.HasChildren());
        }
    }
}