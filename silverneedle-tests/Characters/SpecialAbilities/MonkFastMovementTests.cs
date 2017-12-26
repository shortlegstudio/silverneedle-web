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

    public class MonkFastMovementTests
    {
        [Fact]
        public void MonksMoveFasterAsLevelsIncrease()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            var dataTable = new DataTable("monk abilities");
            dataTable.SetColumns(new string[] { "fast-movement" });
            dataTable.AddRow("1", new string[] { "10" });
            dataTable.AddRow("2", new string[] { "20" });
            var fastMove = new MonkFastMovement(dataTable);
            var oldSpeed = monk.Movement.MovementSpeed;
            monk.Add(fastMove);
            Assert.Equal(oldSpeed + 10, monk.Movement.MovementSpeed);
            monk.SetLevel(2);
            Assert.Equal(oldSpeed + 20, monk.Movement.MovementSpeed);
        }
    }
}