// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class AddToCharacter : ICharacterDesignStep
    {
        private IObjectStore configuration;
        public AddToCharacter(IObjectStore configuration)
        {
            this.configuration = configuration;
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var typeName = this.configuration.GetString("type");
            character.Add(
                typeName.Instantiate<object>(this.configuration)
            );
        }
    }
}