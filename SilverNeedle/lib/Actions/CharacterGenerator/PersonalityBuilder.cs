// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class PersonalityBuilder : ICharacterDesignStep
    {
        private EntityGateway<PersonalityType> personalities;
        private EntityGateway<Ideal> ideals;

        public PersonalityBuilder()
        {
            personalities = GatewayProvider.Get<PersonalityType>();
            ideals = GatewayProvider.Get<Ideal>();
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            Random(character);
        }

        public void Random(CharacterSheet character)
        {
            character.PersonalityType = personalities.ChooseOne();         
            character.Ideal = ideals.ChooseOne();
        }
    }
}