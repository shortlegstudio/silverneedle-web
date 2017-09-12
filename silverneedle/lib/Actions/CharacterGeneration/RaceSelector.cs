﻿//-----------------------------------------------------------------------
// <copyright file="RaceSelector.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
        /// <summary>
        /// The trait gateway provides access to all traits
        /// </summary>
        private EntityGateway<Trait> traitGateway;

        private EntityGateway<Race> raceGateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Actions.CharacterGeneration.RaceSelector"/> class.
        /// </summary>
        /// <param name="races">Races gateway to load from.</param>
        /// <param name="traitGateway">Trait gateway.</param>
        public RaceSelector(EntityGateway<Race> raceGateway, EntityGateway<Trait> traitGateway)
        {
            this.traitGateway = traitGateway;
            this.raceGateway = raceGateway;
        }

        public RaceSelector()
        {
            this.traitGateway = GatewayProvider.Get<Trait>();
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
            character.SetRace(race);

            this.SetSpeedForRace(character, race);
            this.SetTraitsForRace(character, race);
            this.SetAbilityScoresForRace(character, race);
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
        /// Sets the traits for race.
        /// </summary>
        /// <param name="character">Charactet to assign traits to.</param>
        /// <param name="race">Race selected.</param>
        private void SetTraitsForRace(CharacterSheet character, Race race)
        {
            // Add Traits
            foreach (var trait in race.Traits)
            {
                var t = this.traitGateway.All().First(x => x.Name == trait);
                character.Add(t);
            }
        }

        /// <summary>
        /// Sets the ability scores for race.
        /// </summary>
        /// <param name="abilities">Abilities for adjustments.</param>
        /// <param name="race">Race selected.</param>
        private void SetAbilityScoresForRace(CharacterSheet character, Race race)
        {
            // Add Ability Modifiers
            foreach (var adj in race.AbilityModifiers)
            {
                if (adj.ChooseAny)
                {
                    character.Add(new AbilityScoreToken((int)adj.Modifier, adj.Reason));
                }
                else
                {
                    var a = character.AbilityScores.GetAbility(adj.AbilityName);
                    a.AddModifier(adj);
                }
            }
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

        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            ChooseRace(character, strategy.Races);
            strategy.AddLanguagesKnown(character.Race.KnownLanguages);
            strategy.AddLanguageChoices(character.Race.AvailableLanguages);
        }

    }
}