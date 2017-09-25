// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters.Attacks;
    public class FlurryOfBlowsTests
    {
        [Fact]
        public void FlurryOfBlowsUsesTheMonkAbilitiesTableToDetermineBaseAttackBonus()
        {
            var monk = CharacterTestTemplates.MarkyMonk();

            var table = new DataTable("monk abilities");
            table.SetColumns(new string[] { "flurry-of-blows" });
            table.AddRow("1", new string[] { "-1/-1" });

        }
    }
}