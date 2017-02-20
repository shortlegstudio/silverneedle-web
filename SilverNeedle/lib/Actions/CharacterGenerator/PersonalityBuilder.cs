// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class PersonalityBuilder
    {
        private EntityGateway<PersonalityType> personalities;

        public PersonalityBuilder()
        {
            personalities = new EntityGateway<PersonalityType>();
        }

        public void Random(CharacterSheet character)
        {
            character.PersonalityType = personalities.All().ToList().ChooseOne();
        }
    }
}