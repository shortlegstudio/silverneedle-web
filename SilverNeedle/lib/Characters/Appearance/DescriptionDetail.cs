// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Appearance
{
    using SilverNeedle.Utility;
    public abstract class DescriptionDetail : IGatewayObject
    {
        public DescriptionDetail(IObjectStore data)
        {
            Name = data.GetString("name");
        }

        public DescriptionDetail(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public virtual bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}