// //-----------------------------------------------------------------------
// // <copyright file="AssignAge.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using SilverNeedle.Utility;

namespace SilverNeedle.Actions.CharacterGenerator
{
    public class AssignAge : ICharacterBuildStep
    {
        EntityGateway<Maturity> maturityGateway;

        public AssignAge()
        {
            maturityGateway = GatewayProvider.Get<Maturity>();            
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Age = RandomAge(character.Class.ClassDevelopmentAge, maturityGateway.Find(character.Race.Name));
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public int RandomAge(ClassDevelopmentAge classDevAge, Maturity maturity)
        {
            switch (classDevAge)
            {
                case ClassDevelopmentAge.Young:
                    return maturity.Adulthood + maturity.Young.Roll();
                case ClassDevelopmentAge.Trained:
                    return maturity.Adulthood + maturity.Trained.Roll();
                case ClassDevelopmentAge.Studied:
                    return maturity.Adulthood + maturity.Studied.Roll();
            }
            return 0;
        }
    }
}

