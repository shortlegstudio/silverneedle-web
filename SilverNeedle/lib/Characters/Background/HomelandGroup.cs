// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class HomelandGroup : IGatewayObject
    {
        public WeightedOptionTable<Homeland> Homelands; 
        public string Name { get; private set; }

        public HomelandGroup(IObjectStore data)
        {
            Homelands = new WeightedOptionTable<Homeland>();
            Name = data.GetString("race");
            var table = data.GetObject("table");
            foreach (var entry in table.Children)
            {
                var homeland = new Homeland(Name, entry);
                Homelands.AddEntry(homeland, homeland.Weighting);                
            }
        }


        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}