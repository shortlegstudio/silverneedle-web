// //-----------------------------------------------------------------------
// // <copyright file="NameCharacter.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Actions.NamingThings
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Names;
    using SilverNeedle.Utility;

    public class NameCharacter : ICharacterBuildStep
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

        public string CreateFullName(Gender gender, string race)
        {
            var firstName = namesGateway.GetFirstNames(gender, race).ChooseOne();
            var lastName = namesGateway.GetLastNames(race).ChooseOne();

            return string.Format("{0} {1}", firstName, lastName);
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Name = CreateFullName(character.Gender, character.Race.Name);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}

