// //-----------------------------------------------------------------------
// // <copyright file="AssignAge.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    public class AssignAge : ICharacterDesignStep
    {
        EntityGateway<Maturity> maturityGateway;

        public AssignAge()
        {
            maturityGateway = GatewayProvider.Get<Maturity>();            
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Age = RandomAge(character.Class.ClassDevelopmentAge, maturityGateway.Find(character.Race.Name));
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

