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

    public class CreatePhysicalFeatures : ICharacterDesignStep
    {
        EntityGateway<PhysicalFeature> physical;
        public int MaximumFeatures = 3;
        public CreatePhysicalFeatures()
        {
            physical = GatewayProvider.Get<PhysicalFeature>();
        }

        public CreatePhysicalFeatures(EntityGateway<PhysicalFeature> physicalFeatures)
        {
            physical = physicalFeatures;
        }

        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var chosenOptions = new List<PhysicalFeature>();
            var paragraph = new ParagraphBuilder();

            while(chosenOptions.Count < MaximumFeatures && physical.All().Exclude(chosenOptions).HasChoices())
            {
                var selected = physical.All().Exclude(chosenOptions).ChooseOne();
                chosenOptions.Add(selected);
                paragraph.AddSentence(CharacterSentenceGenerator.Create(character, selected));
            }
            character.Appearance.PhysicalAppearance = paragraph.GetParagraph();
        }

    }
}

