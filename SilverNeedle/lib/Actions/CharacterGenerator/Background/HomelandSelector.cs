// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using SilverNeedle.Characters.Background;
using SilverNeedle.Characters;
using SilverNeedle.Utility;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class HomelandSelector : ICharacterBuildStep
    {
        private EntityGateway<HomelandGroup> homelands;

        public HomelandSelector(EntityGateway<HomelandGroup> homelands)
        {
            this.homelands = homelands;
        }

        public HomelandSelector()
        {
            this.homelands = GatewayProvider.Get<HomelandGroup>();
        }

        public Homeland SelectHomelandByRace(string race)
        {
            return this.homelands.Find(race).Homelands.ChooseRandomly();
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

