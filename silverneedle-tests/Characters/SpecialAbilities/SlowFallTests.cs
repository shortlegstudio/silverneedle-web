// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    public class SlowFallTests
    {
        [Fact]
        public void SlowFallImprovesWithLevels()
        {
            var dataTable = new DataTable("monk abilities");
            dataTable.SetColumns(new string[] { "slow-fall" });
            dataTable.AddRow("1", new string[] { "10" });
            dataTable.AddRow("2", new string[] { "25" });

            var slowFall = new SlowFall(dataTable);

            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Add(slowFall);

            Assert.Equal("Slow Fall (10 ft)", slowFall.DisplayString());
            monk.SetLevel(2);
            Assert.Equal("Slow Fall (25 ft)", slowFall.DisplayString());
        }

        [Fact]
        public void SlowFallCanBeSetToUnlimited()
        {

            var dataTable = new DataTable("monk abilities");
            dataTable.SetColumns(new string[] { "slow-fall" });
            dataTable.AddRow("1", new string[] { "Unlimited" });
            var slowFall = new SlowFall(dataTable);
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Add(slowFall);
            Assert.Equal("Slow Fall (Unlimited ft)", slowFall.DisplayString());
        }
    }
}