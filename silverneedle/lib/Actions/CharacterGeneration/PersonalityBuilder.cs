// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class PersonalityBuilder : ICharacterDesignStep
    {
        private EntityGateway<PersonalityType> personalities;
        private EntityGateway<Ideal> ideals;

        public PersonalityBuilder()
        {
            personalities = GatewayProvider.Get<PersonalityType>();
            ideals = GatewayProvider.Get<Ideal>();
        }

        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
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