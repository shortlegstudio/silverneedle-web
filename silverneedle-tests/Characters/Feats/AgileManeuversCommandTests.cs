// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Feats
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Feats;
    using Xunit;

    public class AgileManeuversCommandTests 
    {
        [Fact]
        public void SwitchOutStrengthBonusForCMBToDexterity()
        {
        //Given
            var bob = CharacterTestTemplates.AverageBob();
            bob.AbilityScores.SetScore(AbilityScoreTypes.Strength, 12);  //+1
            bob.AbilityScores.SetScore(AbilityScoreTypes.Dexterity, 16); //+3
            Assert.Equal(1, bob.Offense.CombatManeuverBonus.TotalValue);
        //When
            var agile = new AgileManeuversCommand();
            agile.Execute(bob.Components);
        
        //Then
            Assert.Equal(3, bob.Offense.CombatManeuverBonus.TotalValue);
        }

    }
}