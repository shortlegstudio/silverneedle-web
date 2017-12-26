// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.SpecialAbilities;
using SilverNeedle.Serialization;

    public class FlurryOfBlowsTests : RequiresDataFiles
    {
        private CharacterSheet monk;
        public FlurryOfBlowsTests()
        {
            monk = CharacterTestTemplates.MarkyMonk();
            var strike = new MonkUnarmedStrike();
            monk.Add(strike);
        }
        [Fact]
        public void FlurryOfBlowsUsesTheMonkAbilitiesTableToDetermineBaseAttackBonus()
        {
            var table = new DataTable("monk abilities");
            table.SetColumns(new string[] { "flurry-of-blows" });
            table.AddRow("1", new string[] { "-1/-1" });
            table.AddRow("3", new string[] { "1/1/-1" });
            var flurry = new FlurryOfBlows(table);
            monk.Add(flurry);
            Assert.Equal(2, flurry.NumberOfAttacks);
            Assert.Contains("-1/-1", flurry.DisplayString());
            monk.SetLevel(3);
            Assert.Equal(3, flurry.NumberOfAttacks);
            Assert.Contains("+1/+1/-1", flurry.DisplayString());

        }

        [Fact]
        public void FlurryOfBlowsModifiesAttackAndDamageByStrength()
        {
            var table = new DataTable("monk abilities");
            table.SetColumns(new string[] { "flurry-of-blows" });
            table.AddRow("1", new string[] { "-1/-1" });
            var flurry = new FlurryOfBlows(table);
            monk.Add(flurry);
            monk.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            Assert.Contains("+2/+2", flurry.DisplayString());
        }

        [Fact]
        public void AccessibleFromOffenseStats()
        {
            var table = new DataTable("monk abilities");
            table.SetColumns(new string[] { "flurry-of-blows" });
            table.AddRow("1", new string[] { "-1/-1" });
            var flurry = new FlurryOfBlows(table);
            monk.Add(flurry);
            Assert.Contains(flurry, monk.Offense.Attacks());
        }

    }
}