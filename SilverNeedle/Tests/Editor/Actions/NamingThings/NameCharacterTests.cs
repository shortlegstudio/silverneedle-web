using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Mechanics.CharacterGenerator;
using System.Linq;
using System.Collections.Generic;
using SilverNeedle;
using SilverNeedle.Actions.NamingThings;
using SilverNeedle.Names.Gateways;
using System;


namespace Actions {

	[TestFixture]
    public class NameCharacterTests {
        [Test]
        public void AllowConfiguringWhatKindOfNameToGet() 
        {
            // Set up test with a name
            var nameGateway = new TestNamesGateway();
            var namer = new NameCharacter(nameGateway);

            nameGateway.FirstName = "John";
            nameGateway.LastName = "Smith";
            Assert.AreEqual("(Male-Orc)John OrcSmith", namer.CreateFullName(Gender.Male, "Orc"));
        }

        private class TestNamesGateway : ICharacterNamesGateway 
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public IList<string> GetFirstNames() 
            {
                return new List<string>(new string[] { FirstName });
            }

            public IList<string> GetFirstNames(Gender gender, string race)
            {
                return new List<string>(new string[] { string.Format("({0}-{1}){2}", gender, race, FirstName) });
            }


            public IList<string> GetLastNames() 
            {
                return new List<string>(new string[] { LastName });
            }

            public IList<string> GetLastNames(string race) 
            {
                return new List<string>(new string[] { race + LastName });
            } 
        }
	}
}