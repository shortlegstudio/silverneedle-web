// //-----------------------------------------------------------------------
// // <copyright file="CharacterDrawbackSelector.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters.Background;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class CharacterDrawbackSelector
    {
        IDrawbackGateway drawbacks;

        public CharacterDrawbackSelector(IDrawbackGateway drawbacks)
        {
            this.drawbacks = drawbacks;
        }

        public Drawback SelectDrawback() 
        {
            return drawbacks.ChooseOne();
        }
    }
}

