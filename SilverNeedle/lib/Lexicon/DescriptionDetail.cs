// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;
    public abstract class DescriptionDetail : IGatewayObject
    {
        public string[] Templates { get; set; }
        public DescriptionDetail()
        {
            Descriptors = new Dictionary<string, string[]>();
        }
        public DescriptionDetail(IObjectStore data) : this()
        {
            Name = data.GetString("name");
            var descs = data.GetObjectOptional("descriptors");
            if(descs != null)
            {
                foreach(var d in descs.Children) 
                {
                    var keyName = d.Keys.First();
                    ShortLog.DebugFormat("KeyName: {0}", keyName);
                    var m = d.GetList(keyName);
                    if(Descriptors.ContainsKey(keyName)) {
                        keyName = string.Format("{0}-{1}", keyName, Descriptors.Count);    
                    }
                    Descriptors.Add(keyName, m);
                }
            }

            var temps = data.GetObjectOptional("templates");
            if(temps != null)
            {
                Templates = temps.Children.Select(x => x.GetString("template")).ToArray();
            }
        }

        public DescriptionDetail(string name) : this()
        {
            Name = name;
        }

        public string Name { get; set; }
        
        public IDictionary<string, string[]> Descriptors { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public virtual string CreateDescription()
        {
            var result = "";
            foreach(var d in Descriptors.Values)
            {
                result += string.Format("{0} ", d.ChooseOne());
            }

            if(result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }

            result = string.Format("{0} {1}", result, Name);

            return result.Trim();
        }

        public virtual bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}