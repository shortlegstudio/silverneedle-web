// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System.Collections.Generic;
using System.Linq;
using Xunit;
using SilverNeedle.Characters;
using SilverNeedle.Serialization;
using SilverNeedle.Utility;

namespace Tests.Characters
{


    public class RaceTests
    {
        Race dwarf;
        Race elf;
        Race halfling;
        Race human;

        public RaceTests()
        {
            var data = RaceYamlFile.ParseYaml();
            var races = new List<Race>();
            foreach (var r in data.Children)
            {
                races.Add(new Race(r));
            }

            dwarf = races.First(x => x.Name == "Dwarf");
            elf = races.First(x => x.Name == "Elf");
            halfling = races.First(x => x.Name == "Halfling");
            human = races.First(x => x.Name == "Human");
        }

        [Fact]
        public void LoadRaceYamlFile()
        {
            Assert.NotNull(dwarf);
            Assert.NotNull(elf);
            Assert.NotNull(halfling);
            Assert.NotNull(human);
        }

        [Fact]
        public void RacesHaveTraitsThatTheCanPullFrom()
        {
            Assert.Contains("Hardy", dwarf.Traits);
            Assert.Contains("Halfling Luck", halfling.Traits);
        }

        [Fact]
        public void RacesHaveSizeInformation()
        {
            Assert.Equal(CharacterSize.Medium, dwarf.SizeSetting);
            Assert.Equal(CharacterSize.Small, halfling.SizeSetting);

            //Should have a dice cup for making height rolls
            var cup = dwarf.HeightRange;
            Assert.Equal(cup.Dice.Count, 2);
            Assert.Equal(cup.Modifier, 45);

            cup = human.WeightRange;
            Assert.Equal(cup.Dice.Count, 10);
            Assert.Equal(cup.Modifier, 120);
        }

        [Fact]
        public void KnownLanguagesAreAssigned()
        {
            Assert.True(dwarf.KnownLanguages.Any(x => x == "Common"));
            Assert.True(dwarf.KnownLanguages.Any(x => x == "Dwarven"));

            Assert.True(dwarf.AvailableLanguages.Any(x => x == "Giant"));
            Assert.True(dwarf.AvailableLanguages.Any(x => x == "Gnome"));
            Assert.True(dwarf.AvailableLanguages.Any(x => x == "Terran"));
            Assert.True(dwarf.AvailableLanguages.Any(x => x == "Undercommon"));
        }

        [Fact]
        public void RacesHaveABaseMovementSpeed()
        {
            Assert.Equal(20, dwarf.BaseMovementSpeed);
            Assert.Equal(30, human.BaseMovementSpeed);
            Assert.Equal(100, elf.BaseMovementSpeed);
            Assert.Equal(25, halfling.BaseMovementSpeed);
        }

        [Fact]
        public void HasAttributesLoadedThatSpecifySpecialAbilities()
        {
            AssertExtensions.Contains("SilverNeedle.Character.Senses.Darkvision",
                dwarf.Attributes.Select(x => x.TypeName)
            );
        }

        private const string RaceYamlFile = @"--- 
- race: 
  name: Dwarf
  attributes:
    - type: SilverNeedle.Character.Senses.Darkvision
      range: 60
  size: Medium
  height: 2d4+45
  weight: 14d4+120
  traits:
    - Hardy
  languages: 
    known: [Common, Dwarven]
    available: [Giant, Gnome, Goblin, Orc, Terran, Undercommon]
  basemovementspeed: 20
- race: 
  name: Elf
  size: Medium
  height: 64+2d8
  weight: 14d4+120
  traits:
    - Elfy Stuff
    - Smart Guys
  languages: 
    known: [Common, Dwarven]
    available: [Giant, Gnome, Goblin, Orc, Terrain, Undercommon]
  basemovementspeed: 100
- race:
  name: Human
  size: Medium
  height: 2d10+58
  weight: 10d10+120
  traits:
    - Boring Stuff
    - Extra Skill Point
  languages: 
    known: [ Common ]
    available: [ ALL ]
  basemovementspeed: 30
- race: 
  name: Halfling
  size: Small
  height: 2d4+32
  weight: 14d4+120
  traits:
    - Halfling Luck
    - Foobar
  languages: 
    known: [Common, Halfling]
    available: [ Gnome ]
  basemovementspeed: 25
...";
    }
}