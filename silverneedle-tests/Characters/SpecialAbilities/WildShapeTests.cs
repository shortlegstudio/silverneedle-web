// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class WildShapeTests
    {
        [Fact]
        public void CanBeUsed1TimeADayAtFourthDruidLevel()
        {
            var wildShape = new WildShape();
            var donna = CharacterTestTemplates.DruidDonna();
            donna.SetLevel(4);
            donna.Add(wildShape);
            Assert.Equal(1, wildShape.UsesPerDay);
        }

        [Fact]
        public void CanBeUsedAnExtraTimeADayEveryTwoDruidLevelsAfterFourth()
        {
            var wildShape = new WildShape();
            var donna = CharacterTestTemplates.DruidDonna();
            donna.Add(wildShape);

            donna.SetLevel(6);
            Assert.Equal(2, wildShape.UsesPerDay);

            donna.SetLevel(8);
            Assert.Equal(3, wildShape.UsesPerDay);
            
            donna.SetLevel(12);
            Assert.Equal(5, wildShape.UsesPerDay);

            donna.SetLevel(13);
            Assert.Equal(5, wildShape.UsesPerDay);

            donna.SetLevel(17);
            Assert.Equal(7, wildShape.UsesPerDay);

            donna.SetLevel(18);
            Assert.Equal(8, wildShape.UsesPerDay);
            Assert.Equal("Wild Shape (8/day)", wildShape.DisplayString());
        }

        [Fact]
        public void CanBeUsedAtWillAtTwentiethDruidLevel()
        {
            var wildShape = new WildShape();
            var donna = CharacterTestTemplates.DruidDonna();
            donna.Add(wildShape);
            donna.SetLevel(20);
            Assert.Equal(int.MaxValue, wildShape.UsesPerDay);
            Assert.Equal("Wild Shape (at will)", wildShape.DisplayString());
        }
    }

}