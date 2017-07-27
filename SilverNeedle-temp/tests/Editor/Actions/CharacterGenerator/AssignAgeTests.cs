using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Dice;
using SilverNeedle.Actions.CharacterGenerator;

namespace Actions
{
    [TestFixture]
	public class AssignAgeTests {
        [Fact]
        public void AssignsAnAgeToACharacterBasedOnClassAndMaturity()
        {
            var cls = ClassDevelopmentAge.Young;
            var maturity = new Maturity();
            maturity.Adulthood = 15;
            maturity.Young = DiceStrings.ParseDice("1d4");

            var assignAges = new AssignAge();
            var age = assignAges.RandomAge(cls, maturity);
            Assert.GreaterOrEqual(age, 16);
            Assert.LessOrEqual(age, 19);
        }

	}
}

