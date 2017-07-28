// //-----------------------------------------------------------------------
// // <copyright file="HomelandGatewayTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using Xunit;
using SilverNeedle;
using SilverNeedle.Characters.Background;
using System.Linq;
using SilverNeedle.Utility;

namespace Tests.Characters.Background
{
    
    public class ClassOriginGroupTests
    {
        [Fact]
        public void LoadsUpClassOriginFromYamlFileWithExpectedAttributes()
        {
            var list = ClassOriginYamlFile.ParseYaml().Load<ClassOriginGroup>();
            var bardOriginTable = list.First(x => x.Name.EqualsIgnoreCase("bard")).Origins;
            var entry = bardOriginTable.All.First().Option;
            Assert.Equal("Celebrity", entry.Name);
            Assert.Equal(10, entry.Weighting);
            Assert.True(entry.Traits.Contains("Influence"));
        }

        private const string ClassOriginYamlFile = @"--- 
- background:
  class: barbarian
  table:
    - name: Vengeance
      weight: 10
      traits: Axe to Grind
      storylines: Foeslayer, Vengeance
    - name: Champion of a God
      weight: 10
      traits: Inspired
      storylines: Champion
- background:
  class: bard
  table:
    - name: Celebrity
      weight: 10
      traits: Charming, Influence
    - name: Cultural Mandate
      weight: 10
      traits: Fast Talker
";
    }
}

