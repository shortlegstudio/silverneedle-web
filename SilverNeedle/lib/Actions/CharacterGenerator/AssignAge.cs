// //-----------------------------------------------------------------------
// // <copyright file="AssignAge.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGenerator
{
    public class AssignAge
    {
        public AssignAge()
        {
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

