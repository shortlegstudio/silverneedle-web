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
    public class CharacterDrawbackSelector : ICreateStep
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

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.History.Drawback = SelectDrawback();
        }

        public Drawback SelectDrawback() 
        {
            var table = new WeightedOptionTable<Drawback>(drawbacks.All());
            return table.ChooseRandomly();
        }
    }
}

