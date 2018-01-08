// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class AddFreeLanguages : ICharacterDesignStep, ICharacterFeatureCommand
    {
        string[] languages;
        public AddFreeLanguages(IObjectStore data)
        {
            this.languages = data.GetList("languages");
        }

        public void Execute(ComponentContainer components)
        {
            components.Get<CharacterStrategy>().AddLanguagesKnown(this.languages);
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }
    }
}