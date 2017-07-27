// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Names 
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Names;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;


    public class NameInformationTests
    {
        private EntityGateway<NameInformation> gateway;

        public NameInformationTests()
        {
            gateway = new EntityGateway<NameInformation>(CharacterNamesTestData.ParseYaml().Load<NameInformation>());
        }

        [Fact]
        public void CanLoadABunchOfNames()
        {

            var names = gateway.GetFirstNames();
            Assert.True(names.Count() > 0);
            Assert.True(names.Contains("Steve"));
            Assert.True(names.Contains("Neo"));
        }

        [Fact]
        public void PrunesOutAnyEmptyStrings() 
        {
            var names = gateway.GetFirstNames();
            Assert.False(names.Any(x => string.IsNullOrEmpty(x)));
        }

        [Fact]
        public void CanLoadSomeLastNames()
        {
            var names = gateway.GetLastNames();
            Assert.True(names.Count() > 0);
            Assert.True(names.Contains("Hookum"));
            Assert.True(names.Contains("Fondu"));
        }

        [Fact]
        public void CanFilterNamesBasedOnRaceAndGender()
        {
            var names = gateway.GetFirstNames(Gender.Female, "human");
            Assert.Equal(0, names.Count());
            names = gateway.GetFirstNames(Gender.Female, "dwarf");
            Assert.True(names.Contains("Sheila"));
            Assert.False(names.Contains("Steve"));
        }

        [Fact]
        public void CanFilterLastNamesBasedOnRace()
        {
            var names = gateway.GetLastNames("human");
            Assert.True(names.Contains("Stookum"));
            Assert.False(names.Contains("Roofus"));
        }

        const string CharacterNamesTestData = @"
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