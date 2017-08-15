// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.Personality
{
    using System;
    using HandlebarsDotNet;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SelectFears : ICharacterDesignStep
    {
        EntityGateway<FearTemplate> fearGateway;
        public SelectFears()
        {
            this.fearGateway= GatewayProvider.Get<FearTemplate>();
        }

        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var selected = fearGateway.Choose(strategy.FearCount);
            var fears = new Fears();
            foreach(var q in selected)
            {
            
                fears.Add(CharacterSentenceGenerator.Create(character, q));
            }
            character.Add(fears);
        }
    }
}