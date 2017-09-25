// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    public class ManeuverTrainingTests
    {
        [Fact]
        public void UsesMonkLevelIfMonkLevelForCMDInsteadOfBaseAttackBonusIfBetter()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Offense.BaseAttackBonus.SetValue(5);
            var startCMB = monk.Offense.CombatManeuverBonus.TotalValue;
            monk.Add(new ManeuverTraining());
            monk.SetLevel(10);
            Assert.Equal(startCMB + 5, monk.Offense.CombatManeuverBonus.TotalValue);
        }
    }
}