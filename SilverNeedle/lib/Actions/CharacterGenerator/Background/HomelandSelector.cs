// //-----------------------------------------------------------------------
// // <copyright file="HomelandSelector.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters.Background;
using System.Linq;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class HomelandSelector : ICharacterBuildStep
    {
        private IHomelandGateway homelands;

        public HomelandSelector(IHomelandGateway homelands)
        {
            this.homelands = homelands;
        }

        public HomelandSelector()
        {
            this.homelands = GatewayProvider.Instance().Homelands;
        }

        public Homeland SelectHomelandByRace(string race)
        {
            return this.homelands.GetRacialOptions(race).ChooseRandomly();
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.History.Homeland = SelectHomelandByRace(character.Race.Name);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}

