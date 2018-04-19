// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using YamlDotNet.RepresentationModel;
    using SilverNeedle.Utility;

    /// <summary>
    /// A wrapper around the YamlNodes that abstracts away complexities of the Yaml format
    /// </summary>
    public class YamlObjectStore : IObjectStore
    {
        /// <summary>
        /// Has a value if node is a sequence node
        /// </summary>
        private YamlSequenceNode sequenceNode;

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
        /// Initializes a new instance of the <see cref="SilverNeedle.YamlNodeWrapper"/> class.
        /// </summary>
        /// <param name="wrap">YamlNode to wrap.</param>
        public YamlObjectStore(YamlNode wrap)
        {
            this.node = wrap;
            this.sequenceNode = this.node as YamlSequenceNode;
            this.mappingNode = this.node as YamlMappingNode;
        }

        public YamlObjectStore()
        {
            this.mappingNode = new YamlMappingNode();
            this.node = this.mappingNode;
        }

        /// <summary>
        /// Determines whether this instance has children.
        /// </summary>
        /// <returns><c>true</c> if this instance has children; otherwise, <c>false</c>.</returns>
        public bool HasChildren
        {
            get
            {
                return Children.Count() > 0;
            }
        }

        /// <summary>
        /// Children found in this instance of YAML
        /// </summary>
        /// <returns>Returns the children of this node if there are any</returns>
        public IEnumerable<IObjectStore> Children 
        {            
            get
            {
                if(this.sequenceNode == null)
                    return new List<IObjectStore>();
                return this.sequenceNode.Children.Select(x => new YamlObjectStore(x)).ToList();
            }
        }

        public bool HasKey(string node) 
        {
            return Keys.Any(x => x == node);
        }

        /// <summary>
        /// Gets a string value from the node based on the key. 
        /// Note: Throws exception if key is not found
        /// </summary>
        /// <returns>The string value of the key</returns>
        /// <param name="key">Key to lookup</param>
        public string GetString(string key)
        {
            var item = this.GetScalarNode(key);
            return item.Value;
        }


        private YamlScalarNode GetScalarNode(string key)
        {
            var item =  GetScalarNodeOptional(key);
            if(item == null)
                throw new KeyNotFoundException(key);
            return item;
        }

        /// <summary>
        /// Gets the string based on a key optionally.
        /// </summary>
        /// <returns>The string of the key or null if key is not found</returns>
        /// <param name="key">Key to lookup in node</param>
        public string GetStringOptional(string key, string defaultValue = null)
        {
            var item = this.GetScalarNodeOptional(key);
            if (item != null)
            {
                return item.Value;
            }

            return defaultValue;
        }

        private YamlScalarNode GetScalarNodeOptional(string key)
        {
            var keyNode = new YamlScalarNode(key);
            if(mappingNode.Children.ContainsKey(keyNode))
            {
                return mappingNode.Children[keyNode] as YamlScalarNode;
            }
            return null;
        }

        /// <summary>
        /// Translates a key that contains commma separated values into a string array
        /// </summary>
        /// <returns>The string array split and trimmed around commas. 
        /// Returns an empty array if key is not found </returns>
        /// <param name="key">Key to the comma delimited string</param>
        public string[] GetListOptional(string key)
        {
            if(this.HasKey(key))
                return GetList(key);

            return new string[] { };
        }

        public string[] GetList(string key)
        {
            var sequence = mappingNode.Children[new YamlScalarNode(key)] as YamlSequenceNode;
            return sequence.Children.OfType<YamlScalarNode>().Select(x => x.Value).ToArray();
        }

        public IEnumerable<IObjectStore> GetObjectList(string key)
        {
            var sequence = mappingNode.Children[new YamlScalarNode(key)] as YamlSequenceNode;
            return sequence.Children.Select(x => new YamlObjectStore(x));
        }

        public IEnumerable<IObjectStore> GetObjectListOptional(string key)
        {
            if(HasKey(key))
                return GetObjectList(key);
            return new List<IObjectStore>();
        }

        public bool GetBool(string key)
        {
            return Boolean.Parse(this.GetString(key));
        }

        /// <summary>
        /// Gets a boolean value from the YAML document. 
        /// Boolean must match "yes" to be true
        /// </summary>
        /// <returns><c>true</c>, if bool optional was gotten, <c>false</c> otherwise.</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public bool GetBoolOptional(string key, bool defaultValue = false)
        {
            if(HasKey(key))
                return GetBool(key);

            return defaultValue;
        }

        /// <summary>
        /// Gets the integer optional.
        /// </summary>
        /// <returns>The integer value found, 0 otherwise.</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public int GetIntegerOptional(string key, int defaultValue = 0)
        {
            var v = this.GetStringOptional(key);
            if (v == null)
            {
                return defaultValue;
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

        public float GetFloatOptional(string key, float defaultValue = 0)
        {
            var v = GetStringOptional(key);
            if (v == null)
            {
                return defaultValue;
            }
            return float.Parse(v);
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

        public IEnumerable<string> Keys 
        {
            get
            {
                return this.mappingNode.Children.Select(x => x.Key.ToString());
            }
        }

        /// <summary>
        /// Gets a wrapper node based on key for traversing trees. Throws exception if not found
        /// </summary>
        /// <returns>The node from the key</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public IObjectStore GetObject(string key)
        {
            try
            {
                var item = this.mappingNode.Children[new YamlScalarNode(key)];
                return new YamlObjectStore(item);
            }
            catch(Exception ex)
            {
                throw new ObjectStoreKeyNotFoundException(this, key, ex);
            }
        }

        /// <summary>
        /// Gets the node optional.
        /// </summary>
        /// <returns>The node optional.</returns>
        /// <param name="key">Key to lookup in YAML node</param>
        public IObjectStore GetObjectOptional(string key)
        {
            if (!HasKey(key))
                return null;
                
            var item = this.mappingNode.Children[new YamlScalarNode(key)];
            return new YamlObjectStore(item);        
        }

        public string GetTag()
        {
            return this.node.Tag;
        }

        public IList<T> Load<T>()
        {
            var type = typeof(T);
            var list = new List<T>();
            foreach(var obj in Children)
            {
                list.Add(type.Instantiate<T>(obj));
                
            }
            return list;
        }

        public void SetValue(string key, string val)
        {
            this.mappingNode.Children[new YamlScalarNode(key)] = new YamlScalarNode(val);
        }

        public void SetValue(string key, int val)
        {
            this.SetValue(key, val.ToString());
        }

        public void SetValue(string key, IObjectStore val)
        {
            this.mappingNode.Children[new YamlScalarNode(key)] = ((YamlObjectStore)val).MappingNode;
        }

        public void SetValue(string key, IEnumerable<string> vals)
        {
            var sequenceNode = new YamlSequenceNode();
            var nodes = vals.Select(x => new YamlScalarNode(x));
            sequenceNode.Children.Add(nodes);
            this.mappingNode.Children[new YamlScalarNode(key)] = sequenceNode;
        }

        public void SetValue(string key, IEnumerable<IObjectStore> vals)
        {
            var sequenceNode = new YamlSequenceNode();
            var nodes = vals.Cast<YamlObjectStore>().Select(x => x.MappingNode);
            sequenceNode.Children.Add(nodes);
            this.mappingNode.Children[new YamlScalarNode(key)] = sequenceNode;
        }

        public void SetValue(string key, bool v)
        {
            SetValue(key, v.ToString());
        }

        public void SetValue(string key, float v)
        {
            SetValue(key, v.ToString());
        }

        public YamlMappingNode MappingNode { get { return this.mappingNode; } }

    }
}