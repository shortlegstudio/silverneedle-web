// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.Background
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class BackgroundStorywriter : ICharacterDesignStep
    {
        private EntityGateway<Descriptor> descriptors;
        public BackgroundStorywriter()
        {
            this.descriptors = GatewayProvider.Get<Descriptor>();
        }
        public BackgroundStorywriter(EntityGateway<Descriptor> descriptors)
        {
            this.descriptors = descriptors;
        }

        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var storyTemplate = descriptors.Find("background-story").Words.ChooseOne();
            var expansion = new PhraseTemplate(storyTemplate);
            var story = new BackgroundStory(expansion.WritePhrase(new CharacterContext(character)));
            character.Add(story);
        }
    }

}