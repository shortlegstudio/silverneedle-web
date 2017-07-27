using System.Linq;
using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Serialization;

namespace Characters {
    [TestFixture]
    public class PersonalityTypeTests {
        [Fact]
        public void PersonalityTypesProvideASummaryOfCharacterBehaviorsBasedOnMyersBriggs()
        {
            var personality = new PersonalityType("INFP");
            Assert.AreEqual(PersonalityTypes.Attitude.Introverted, personality.Attitude);
            Assert.AreEqual(PersonalityTypes.Information.Intuitive, personality.InformationProcessing);
            Assert.AreEqual(PersonalityTypes.DecisionMaking.Feeling, personality.DecisionMaking);
            Assert.AreEqual(PersonalityTypes.Structure.Perceiving, personality.Structure);            
        }

        [Fact]
        public void CanLoadPersonalityType() {
            var data = new MemoryStore();
            data.SetValue("type", "ISTJ");
            data.SetValue("descriptors", "Quiet, Serious, Focused");
            data.SetValue("weaknesses", "Arrogant, Self-Centered");

            var personality = new PersonalityType(data);
            Assert.AreEqual(PersonalityTypes.Attitude.Introverted, personality.Attitude);
            Assert.AreEqual(PersonalityTypes.Information.Sensing, personality.InformationProcessing);
            Assert.AreEqual(PersonalityTypes.DecisionMaking.Thinking, personality.DecisionMaking);
            Assert.AreEqual(PersonalityTypes.Structure.Judging, personality.Structure);            
            Assert.AreEqual("Quiet", personality.Descriptors.ElementAt(0));
            Assert.AreEqual("Arrogant", personality.Weaknesses.ElementAt(0));
        }
    }
}