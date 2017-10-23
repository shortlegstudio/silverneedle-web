// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;

    public class BaseAttackBonusPrerequisiteTests
    {
        [Fact]
        public void ChecksAgainstCharactersToDetermineIfHasEnoughAttackBonus()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var prereq = new BaseAttackBonusPrerequisite("2");
            Assert.False(prereq.IsQualified(bob));
            bob.Offense.BaseAttackBonus.SetValue(2);
            Assert.True(prereq.IsQualified(bob));
        }
    }
}