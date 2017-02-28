// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Groups
{
    using SilverNeedle.Utility;

    public class SettlementType
    {
        public string Name { get; private set; }
        public int MinimumPopulation { get; private set; }
        public int MaximumPopulation { get; private set; }

        public SettlementType(IObjectStore data)
        {
            Name = data.GetString("name");
            MinimumPopulation = data.GetInteger("minimum");
            MaximumPopulation = data.GetInteger("maximum");
        }
    }
}