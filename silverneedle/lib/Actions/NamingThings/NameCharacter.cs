// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.NamingThings
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Names;
    using SilverNeedle.Serialization;

    public class NameCharacter : ICharacterDesignStep
    {
        private EntityGateway<NameInformation> namesGateway;

        public NameCharacter(EntityGateway<NameInformation> namesGateway)
        {
            this.namesGateway = namesGateway;    
        }

        public NameCharacter()
        {
            this.namesGateway = GatewayProvider.Get<NameInformation>();
        }

        public string GetFirstName(Gender gender, string race)
        {
            return namesGateway.GetFirstNames(gender, race).ChooseOne();;
        }

        public string GetLastName(string race)
        {
            return namesGateway.GetLastNames(race).ChooseOne();
        }

        public string CreateFullName(Gender gender, string race)
        {
            return GetFirstName(gender, race) + " " + GetLastName(race);
        }
        public string CreateFullName(Gender gender, string race, string lastName)
        {
            return GetFirstName(gender, race) + " " + lastName;
        }

        public void ExecuteStep(CharacterSheet character)
        {
            character.FirstName = GetFirstName(character.Gender, character.Race.Name);
            character.LastName = GetLastName(character.Race.Name);
        }
    }
}

