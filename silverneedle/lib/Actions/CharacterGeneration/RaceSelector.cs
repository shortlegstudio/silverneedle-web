// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using System.Linq;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    /// <summary>
    /// Race selector chooses a race for a charactor
    /// </summary>
    public class RaceSelector : ICharacterDesignStep
    {
        private EntityGateway<Race> raceGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Actions.CharacterGeneration.RaceSelector"/> class.
        /// </summary>
        /// <param name="races">Races gateway to load from.</param>
        /// <param name="traitGateway">Trait gateway.</param>
        public RaceSelector(EntityGateway<Race> raceGateway)
        {
            this.raceGateway = raceGateway;
        }

        public RaceSelector()
        {
            this.raceGateway = GatewayProvider.Get<Race>();
        }

        private void ChooseRace(CharacterSheet character, WeightedOptionTable<string> options)
        {
            if (options.IsEmpty) 
            {
                this.SetRace(character, raceGateway.All().ToList().ChooseOne());
                return;
            }

            var choice = options.ChooseRandomly();
            var race = raceGateway.Find(choice);
            this.SetRace(character, race);
        }

        /// <summary>
        /// Sets the race.
        /// </summary>
        /// <param name="character">Character to set the race to.</param>
        /// <param name="race">Race to assign.</param>
        public void SetRace(CharacterSheet character, Race race)
        {
            character.Add(race);

            this.SetSpeedForRace(character, race);
            this.SetSizeForRace(character.Size, race);
        }

        /// <summary>
        /// Sets the speed for race.
        /// </summary>
        /// <param name="character">Character to assign.</param>
        /// <param name="race">Race selected.</param>
        private void SetSpeedForRace(CharacterSheet character, Race race)
        {
            // Update Speed
            character.Movement.SetBaseSpeed(race.BaseMovementSpeed);
        }

        /// <summary>
        /// Sets the size for race.
        /// </summary>
        /// <param name="size">Size to assign to.</param>
        /// <param name="race">Race selected.</param>
        private void SetSizeForRace(SizeStats size, Race race)
        {
            // Update Size
            size.SetSize(race.SizeSetting, race.HeightRange.Roll(), race.WeightRange.Roll());
        }

        public void ExecuteStep(CharacterSheet character)
        {
            var strategy = character.Strategy;
            ChooseRace(character, strategy.Races);
            strategy.AddLanguagesKnown(character.Race.KnownLanguages);
            strategy.AddLanguageChoices(character.Race.AvailableLanguages);
        }

    }
}