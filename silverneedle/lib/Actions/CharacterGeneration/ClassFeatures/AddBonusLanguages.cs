// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class AddBonusLanguages : ICharacterDesignStep
    {
        string[] languages;
        public AddBonusLanguages(IObjectStore data)
        {
            this.languages = data.GetList("languages");
        }
        public void ExecuteStep(CharacterSheet character)
        {
            character.Strategy.AddLanguageChoices(this.languages);
        }
    }
}