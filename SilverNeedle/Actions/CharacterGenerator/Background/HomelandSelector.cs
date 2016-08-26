// //-----------------------------------------------------------------------
// // <copyright file="HomelandSelector.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters.Background;
using System.Linq;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class HomelandSelector
    {
        private IHomelandGateway homelands;

        public HomelandSelector(IHomelandGateway homelands)
        {
            this.homelands = homelands;
        }

        public Homeland SelectHomelandByRace(string race)
        {
            return this.homelands.GetRacialOptions(race).ChooseRandomly();
        }
    }
}

