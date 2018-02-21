// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using SilverNeedle.Serialization;
    using Xunit;
    using SilverNeedle.Characters;
    
    public class BonusAndPenaltyStatisticTests
    {
        [Fact]
        public void TracksTwoStatsForThePenaltyAndTheBonus()
        {
            var yaml = @"
name: Power Attack
bonus: Damage
bonus-base-value: 2
penalty: Attack
penalty-base-value: -1";
            var stat = new BonusAndPenaltyStatistic(yaml.ParseYaml());
            Assert.Equal("Damage", stat.BonusStatistic);
            Assert.Equal(2, stat.Bonus.TotalValue);
            Assert.Equal("Power Attack Bonus", stat.Bonus.Name);
            Assert.Equal("Attack", stat.PenaltyStatistic);
            Assert.Equal(-1, stat.Penalty.TotalValue);
            Assert.Equal("Power Attack Penalty", stat.Penalty.Name);
            Assert.Equal("Power Attack ( -1 Attack / +2 Damage)", stat.DisplayString());

            var bob = CharacterTestTemplates.AverageBob();
            bob.Add(stat);
            Assert.NotNull(bob.FindStat("Power Attack Bonus"));
            Assert.NotNull(bob.FindStat("Power Attack Penalty"));
           
        }
    }
}