using Xunit;
using SilverNeedle.Characters;
using SilverNeedle.Dice;
using SilverNeedle.Actions.CharacterGeneration;

namespace Tests.Actions.CharacterGeneration
{
    
	public class AssignAgeTests : RequiresDataFiles
    {
        [Fact]
        public void AssignsAnAgeToACharacterBasedOnClassAndMaturity()
        {
            var cls = ClassDevelopmentAge.Young;
            var maturity = new Maturity();
            maturity.Adulthood = 15;
            maturity.Young = DiceStrings.ParseDice("1d4");

            var assignAges = new AssignAge();
            var age = assignAges.RandomAge(cls, maturity);
            Assert.True(age >= 16);
            Assert.True(age <= 19);
        }

	}
}

