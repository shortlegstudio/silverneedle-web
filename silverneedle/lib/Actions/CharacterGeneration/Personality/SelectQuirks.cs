// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.Personality
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SelectQuirks : ICharacterDesignStep
    {
        EntityGateway<QuirkTemplate> quirkGateway;
        public SelectQuirks()
        {
            this.quirkGateway= GatewayProvider.Get<QuirkTemplate>();
        }

        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var selected = quirkGateway.Choose(strategy.QuirkCount);
            var quirks = new Quirks();
            foreach(var q in selected)
            {
            
                quirks.Items.Add(CharacterSentenceGenerator.Create(character, q));
            }
            character.Add(quirks);
        }
    }
}