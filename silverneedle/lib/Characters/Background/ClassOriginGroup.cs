// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class ClassOriginGroup : IGatewayObject
    {
        public WeightedOptionTable<ClassOrigin> Origins; 
        public string Name { get; private set; }

        public ClassOriginGroup(IObjectStore data)
        {
            Origins = new WeightedOptionTable<ClassOrigin>();
            Name = data.GetString("class");
            var table = data.GetObject("table");
            foreach (var entry in table.Children)
            {
                var origin = new ClassOrigin(Name, entry);
                Origins.AddEntry(origin, origin.Weighting);                
            }
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}