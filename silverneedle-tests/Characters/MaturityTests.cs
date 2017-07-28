// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using Xunit;
    using SilverNeedle.Characters;
    using System.Linq;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;

    
    public class MaturityTests {
        Maturity human;
        Maturity dwarf;
        
        public MaturityTests() {
            var list = MaturityDataFile.ParseYaml().Load<Maturity>();
            human = list.First(x => x.Name == "human");
            dwarf = list.First(x => x.Name == "dwarf");
        }

        [Fact]
        public void MaturitiesHaveDifferentAttributesForDifferentProfessions()
        {
            Assert.Equal(15, human.Adulthood);
            Assert.IsType<Cup>(human.Young);
            Assert.Equal(40, dwarf.Adulthood);
        }

        private const string MaturityDataFile = @"--- 
- maturity:
  race: human
  adulthood: 15
  young: 1d4
  trained: 1d6
  studied: 2d6
- maturity:
  race: dwarf
  adulthood: 40
  young: 3d6
  trained: 5d6
  studied: 7d6
...";
    }
}