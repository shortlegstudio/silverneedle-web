// //-----------------------------------------------------------------------
// // <copyright file="CharacterDrawbackSelector.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using SilverNeedle.Characters.Background;
using SilverNeedle.Utility;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class CharacterDrawbackSelector : ICharacterBuildStep
    {
        EntityGateway<Drawback> drawbacks;

        public CharacterDrawbackSelector(EntityGateway<Drawback> drawbacks)
        {
            this.drawbacks = drawbacks;
        }

        public CharacterDrawbackSelector()
        {
            this.drawbacks = GatewayProvider.Get<Drawback>();
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
            var table = new WeightedOptionTable<Drawback>(drawbacks.All());
            return table.ChooseRandomly();
        }
    }
}

