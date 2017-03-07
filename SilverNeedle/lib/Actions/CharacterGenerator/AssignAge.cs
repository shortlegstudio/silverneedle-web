// //-----------------------------------------------------------------------
// // <copyright file="AssignAge.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGenerator
{
    public class AssignAge : ICharacterBuildStep
    {
        IRaceMaturityGateway maturityGateway;

        public AssignAge()
        {
            maturityGateway = GatewayProvider.Instance().Maturity;            
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Age = RandomAge(character.Class.ClassDevelopmentAge, maturityGateway.Get(character.Race));
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

