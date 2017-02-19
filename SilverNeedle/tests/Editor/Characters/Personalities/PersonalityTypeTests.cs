using NUnit.Framework;
using SilverNeedle.Characters;

namespace Characters {
    [TestFixture]
    public class PersonalityTypeTests {
        [Test]
        public void PersonalityTypesProvideASummaryOfCharacterBehaviorsBasedOnMyersBriggs()
        {
            var personality = new PersonalityType("INFP");
            Assert.AreEqual(PersonalityTypes.Introverted, personality.Attitude);
            Assert.AreEqual(PersonalityTypes.Intuitive, personality.InformationProcessing);
            Assert.AreEqual(PersonalityTypes.Feeling, personality.DecisionMaking);
            Assert.AreEqual(PersonalityTypes.Perceiving, personality.Structure);            
        }
    }
}