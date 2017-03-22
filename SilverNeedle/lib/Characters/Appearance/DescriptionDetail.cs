// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Appearance
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Utility;
    public abstract class DescriptionDetail : IGatewayObject
    {
        public DescriptionDetail()
        {
            Descriptors = new List<string[]>();
        }
        public DescriptionDetail(IObjectStore data) : this()
        {
            Name = data.GetString("name");
            var descs = data.GetObjectOptional("descriptors");
            if(descs != null)
            {
                foreach(var d in descs.Children) 
                {
                    var m = d.GetList("descriptor");
                    Descriptors.Add(m);
                }
            }
        }

        public DescriptionDetail(string name) : this()
        {
            Name = name;
        }

        public string Name { get; set; }
        
        public IList<string[]> Descriptors { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public virtual string CreateDescription()
        {
            var result = "";
            foreach(var d in Descriptors)
            {
                result += string.Format("{0}, ", d.ChooseOne());
            }

            if(result.Length > 0)
            {
                result = result.Substring(0, result.Length - 2);
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