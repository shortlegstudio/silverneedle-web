// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.Appearance
{
    using System.Collections.Generic;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class CreateCharacterDescription : ICharacterDesignStep
    {
        private EntityGateway<Descriptor> descriptors;

        public CreateCharacterDescription() 
        {
            descriptors = GatewayProvider.Get<Descriptor>();
        }

        public CreateCharacterDescription(EntityGateway<Descriptor> descriptors)
        {
            this.descriptors = descriptors;
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var storyTemplate = descriptors.Find("character-appearance").Words.ChooseOne();
            var expansion = new PhraseTemplate(storyTemplate);
            character.Appearance.Description = expansion.WritePhrase(new CharacterContext(character));
        }
    }

}