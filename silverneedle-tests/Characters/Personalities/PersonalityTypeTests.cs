using System.Linq;
using Xunit;
using SilverNeedle.Characters;
using SilverNeedle.Serialization;

namespace Tests.Characters {
    
    public class PersonalityTypeTests {
        [Fact]
        public void PersonalityTypesProvideASummaryOfCharacterBehaviorsBasedOnMyersBriggs()
        {
            var personality = new PersonalityType("INFP");
            Assert.Equal(PersonalityTypes.Attitude.Introverted, personality.Attitude);
            Assert.Equal(PersonalityTypes.Information.Intuitive, personality.InformationProcessing);
            Assert.Equal(PersonalityTypes.DecisionMaking.Feeling, personality.DecisionMaking);
            Assert.Equal(PersonalityTypes.Structure.Perceiving, personality.Structure);            
        }

        [Fact]
        public void CanLoadPersonalityType() {
            var data = new MemoryStore();
            data.SetValue("type", "ISTJ");
            data.SetValue("descriptors", "Quiet, Serious, Focused");
            data.SetValue("weaknesses", "Arrogant, Self-Centered");

            var personality = new PersonalityType(data);
            Assert.Equal(PersonalityTypes.Attitude.Introverted, personality.Attitude);
            Assert.Equal(PersonalityTypes.Information.Sensing, personality.InformationProcessing);
            Assert.Equal(PersonalityTypes.DecisionMaking.Thinking, personality.DecisionMaking);
            Assert.Equal(PersonalityTypes.Structure.Judging, personality.Structure);            
            Assert.Equal("Quiet", personality.Descriptors.ElementAt(0));
            Assert.Equal("Arrogant", personality.Weaknesses.ElementAt(0));
        }
    }
}