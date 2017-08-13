// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class RaceSelectorTests
    {
        private EntityGateway<Race> raceGateway;
        private EntityGateway<Trait> traitGateway;

        private Race elf;

        private Race human;
        private Trait elfTrait;
        private RaceSelector raceSelectorSubject;

        public RaceSelectorTests()
        {
            // Create a race
            elf = new Race();
            elf.Name = "Elfy";
            elf.Traits.Add("Elfy");
            elf.SizeSetting = CharacterSize.Medium;
            elf.HeightRange = DiceStrings.ParseDice("10d6");
            elf.WeightRange = DiceStrings.ParseDice("20d8");
            elf.KnownLanguages.Add("Common");
            elf.KnownLanguages.Add("Elvish");
            elf.AvailableLanguages.Add("Draconic");
            elf.AvailableLanguages.Add("Celestial");

            human = new Race();
            human.Name = "Human";
            human.SizeSetting = CharacterSize.Medium;
            human.HeightRange = DiceStrings.ParseDice("2d8+30");
            human.WeightRange = DiceStrings.ParseDice("3d6+100");
            human.KnownLanguages.Add("Common");


            var list = new List<Race>();
            list.Add(elf);
            list.Add(human);

            //Set up the trait
            elfTrait = new Trait();
            elfTrait.Name = "Elfy";
            var traitList = new List<Trait>();
            traitList.Add(elfTrait);

            // Configure Gateways
            raceGateway = EntityGateway<Race>.LoadFromList(list);
            traitGateway = EntityGateway<Trait>.LoadFromList(traitList);
            
            raceSelectorSubject = new RaceSelector(raceGateway, traitGateway);
        }

        [Fact]
        public void SettingRaceLoadsTraits()
        {
            var sheet = new CharacterSheet();

            raceSelectorSubject.SetRace(sheet, elf);
            Assert.Equal(elf, sheet.Race);
            Assert.True(sheet.Traits.Any(x => x == elfTrait));
        }

        [Fact]
        public void SettingRaceCalculatesSize()
        {
            var sheet = new CharacterSheet();

            var smallGuy = new Race();
            smallGuy.SizeSetting = CharacterSize.Small;
            smallGuy.HeightRange = DiceStrings.ParseDice("2d4+10");
            smallGuy.WeightRange = DiceStrings.ParseDice("2d4+100");

            var assign = new RaceSelector(raceGateway, traitGateway);
            raceSelectorSubject.SetRace(sheet, smallGuy);
            Assert.Equal(CharacterSize.Small, sheet.Size.Size);
            Assert.True(sheet.Size.Height >= 12);
            Assert.True(sheet.Size.Weight >= 102);
        }


        [Fact]
        public void SettingRaceAssignsMovement()
        {
            var sheet = new CharacterSheet();
            var fastGuy = new Race();
            fastGuy.SizeSetting = CharacterSize.Small;
            fastGuy.HeightRange = DiceStrings.ParseDice("2d4+10");
            fastGuy.WeightRange = DiceStrings.ParseDice("2d4+100");
            fastGuy.BaseMovementSpeed = 45;

            raceSelectorSubject.SetRace(sheet, fastGuy);
            Assert.Equal(45, sheet.Movement.BaseMovement.TotalValue);
        }

        [Fact]
        public void OptionTableLimitsSelectionOfRace()
        {
            var sheet = new CharacterSheet();
            var options = new WeightedOptionTable<string>();
            var strategy = new CharacterBuildStrategy();
            strategy.Races.AddEntry("Human", 12);

            //Run it 1000 times, should always be human
            for (int x = 0; x < 1000; x++)
            {
                raceSelectorSubject.Process(sheet, strategy);
                Assert.Equal(human, sheet.Race);
            }
        }

        [Fact]
        public void IfChoiceListIsEmptyChooseAnyRace() 
        {	
            var sheet = new CharacterSheet();
            var strategy = new CharacterBuildStrategy();

            raceSelectorSubject.Process(sheet, strategy);
            Assert.NotNull(sheet.Race);
        }

        [Fact]
        public void AddLanguagesKnownToStrategy()
        {
            var character = new CharacterSheet();
            var strategy = new CharacterBuildStrategy();
            strategy.Races.AddEntry("Elfy", 1000);
            raceSelectorSubject.Process(character, strategy);
            Assert.Equal(character.Race.Name, "Elfy");
            Assert.NotStrictEqual(strategy.LanguagesKnown, new string[] {"Common", "Elvish"});
            Assert.NotStrictEqual(strategy.LanguageChoices, new string[] {"Draconic", "Celestial"});
        }
    }

    class TestTraitGateway : IEntityGateway<Trait>
    {
        public List<Trait> Traits = new List<Trait>();

        public IEnumerable<Trait> All()
        {
            return Traits;
        }
    }
}

