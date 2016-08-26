using NUnit.Framework;
using System.Linq;
using SilverNeedle;
using SilverNeedle.Names.Gateways;
using SilverNeedle.Characters;
using SilverNeedle.Yaml;

namespace Names {
	[TestFixture]
	public class CharacterNamesYamlGatewayTests
    {
        [Test]
        public void CanLoadABunchOfNames()
        {
            var gateway = new CharacterNamesYamlGateway(CharacterNamesYamlFile.ParseYaml());
            var names = gateway.GetFirstNames();
            Assert.Greater(names.Count(), 0);
            Assert.IsTrue(names.Contains("Steve"));
            Assert.IsTrue(names.Contains("Neo"));
        }

        [Test]
        public void PrunesOutAnyEmptyStrings() 
        {
            var gateway = new CharacterNamesYamlGateway(CharacterNamesYamlFile.ParseYaml());
            var names = gateway.GetFirstNames();
            Assert.IsFalse(names.Any(x => string.IsNullOrEmpty(x)));
        }

        [Test]
        public void CanLoadSomeLastNames()
        {
            var gateway = new CharacterNamesYamlGateway(CharacterNamesYamlFile.ParseYaml());
            var names = gateway.GetLastNames();
            Assert.Greater(names.Count(), 0);
            Assert.IsTrue(names.Contains("Hookum"));
            Assert.IsTrue(names.Contains("Fondu"));
        }

        [Test]
        public void CanFilterNamesBasedOnRaceAndGender()
        {
            var gateway = new CharacterNamesYamlGateway(CharacterNamesYamlFile.ParseYaml());
            var names = gateway.GetFirstNames(Gender.Female, "human");
            Assert.AreEqual(0, names.Count());
            names = gateway.GetFirstNames(Gender.Female, "dwarf");
            Assert.IsTrue(names.Contains("Sheila"));
            Assert.IsFalse(names.Contains("Steve"));
        }

        [Test]
        public void CanFilterLastNamesBasedOnRace()
        {
            var gateway = new CharacterNamesYamlGateway(CharacterNamesYamlFile.ParseYaml());
            var names = gateway.GetLastNames("human");
            Assert.IsTrue(names.Contains("Stookum"));
            Assert.IsFalse(names.Contains("Roofus"));
        }

        const string CharacterNamesYamlFile = @"
- gender: male
  race: human
  category: first
  names: | 
    Steve, John, Charles
    Neo,,,Foobar
- gender: any
  race: human
  category: last
  names: |
    Smith, Johnson, Fondu
    ,,Hookum,Stookum,
- gender: female
  race: dwarf
  category: first
  names: |
    Arletta, Sheila, Sara
- gender: any
  race: dwarf
  category: last
  names: |
    Rockhammer, Biggut, Roofus
";
	}
}