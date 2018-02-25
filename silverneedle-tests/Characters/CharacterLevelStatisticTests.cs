// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;

    public class CharacterLevelStatisticTests
    {
        [Fact]
        public void ReturnsTheCharacterLevelByAddingUpAllTheClassLevels()
        {
            var characterSheet = CharacterTestTemplates.AverageBob();
            var stat = new CharacterLevelStatistic();
            var wzrdLevel = new ClassLevel(Class.CreateForTesting("wizard", DiceSides.d6));
            var fgtrLevel = new ClassLevel(Class.CreateForTesting("fighter", DiceSides.d10));
            characterSheet.Add(stat);
            characterSheet.Add(wzrdLevel);
            characterSheet.Add(fgtrLevel);
            wzrdLevel.Level = 6;
            fgtrLevel.Level = 3;

            Assert.Equal(9, stat.TotalValue);

        }
    }
}