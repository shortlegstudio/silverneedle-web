// //-----------------------------------------------------------------------
// // <copyright file="CharacterDrawbackSelector.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using SilverNeedle.Characters.Background;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class CharacterDrawbackSelector : ICharacterBuildStep
    {
        IDrawbackGateway drawbacks;

        public CharacterDrawbackSelector(IDrawbackGateway drawbacks)
        {
            this.drawbacks = drawbacks;
        }

        public CharacterDrawbackSelector()
        {
            this.drawbacks = GatewayProvider.Instance().Drawbacks;
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.History.Drawback = SelectDrawback();
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }

        public Drawback SelectDrawback() 
        {
            return drawbacks.ChooseOne();
        }
    }
}

