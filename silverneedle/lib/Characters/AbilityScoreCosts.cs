// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Linq;
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    public class AbilityScoreCosts : IGatewayObject
    {
        public IDictionary<int, int> PointCosts { get; private set; }
        public string Name { get; private set; }
        public AbilityScoreCosts() 
        {
            this.PointCosts = new Dictionary<int, int>();
        }

        public AbilityScoreCosts(IObjectStore data) : this()
        {
            this.Name = data.GetString("name");
            var scores = data.GetObject("scores");
            foreach(var point in scores.Keys)
            {
                this.PointCosts.Add(int.Parse(point), scores.GetInteger(point));
            }
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public int[] NegativeCosts()
        {
            return PointCosts.Keys.Where(point => PointCosts[point] < 0).ToArray();
        }

        public int[] PositiveCosts()
        {
            return PointCosts.Keys.Where(point => PointCosts[point] > 0).ToArray();
        }

        public int ZeroCost()
        {
            return PointCosts.Keys.First(point => PointCosts[point] == 0);
        }

        public int ClosestValue(int pointsAvailable)
        {
            return PointCosts.Keys.Where(point => PointCosts[point] <= pointsAvailable).Last();
        }
    }
}