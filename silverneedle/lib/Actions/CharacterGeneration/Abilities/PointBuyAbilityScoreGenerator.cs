// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.Abilities
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public abstract class PointBuyAbilityScoreGenerator : IAbilityScoreGenerator
    {
        public AbilityScoreCosts PointCosts { get; private set; }
        public abstract int Points { get; }
        public int MaxDumpStats { get; set; }

        public PointBuyAbilityScoreGenerator()
        {
            MaxDumpStats = 3;
            PointCosts = GatewayProvider.Find<AbilityScoreCosts>("standard");
        }

        public List<int> GetScores()
        {
            var availablePoints = this.Points;
            var divider = 0.6f;

            List<int> scores = new List<int>();
            // First Identify whether to have zero, one or two stats as dumps
            int dumps = Randomly.Range(1, this.MaxDumpStats);
            for(int i = 0; i < dumps; i++)
            {
                var select = this.PointCosts.NegativeCosts().ChooseOne();
                scores.Add(select);
                availablePoints -= this.PointCosts.PointCosts[select]; 
            }

            // use 1/2 of points available at each buy point rounded up
            while(availablePoints > 0)
            {
                var spendPoints = availablePoints * divider;
                var closest = this.PointCosts.ClosestValue(spendPoints.Ceiling());
                var cost = this.PointCosts.PointCosts[closest];
                availablePoints -= cost;
                scores.Add(closest);
            }


            //Set Rest of values to Zero Cost
            while(scores.Count < 6)
            {
                scores.Add(PointCosts.ZeroCost());
            }

            
            
            return scores;
        }
        
    }

    public class TenPointBuy : PointBuyAbilityScoreGenerator
    {
        public override int Points { get { return 10; } }
    }
    public class FifteenPointBuy : PointBuyAbilityScoreGenerator
    {
        public override int Points { get { return 15; } }
    }
    public class TwentyPointBuy : PointBuyAbilityScoreGenerator
    {
        public TwentyPointBuy() : base()
        {
            MaxDumpStats = 1;
        }
        public override int Points { get { return 20; } }
    }
}