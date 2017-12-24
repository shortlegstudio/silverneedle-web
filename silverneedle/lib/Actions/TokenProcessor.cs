// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions
{
    using System.Linq;
    using SilverNeedle.Characters;
    public abstract class TokenProcessor<T> : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var tokens = character.GetAndRemoveAll<T>();
            foreach(var t in tokens)
            {
                ProcessToken(character, t);
            }
        }

        protected abstract void ProcessToken(CharacterSheet character, T token);

        
    }
}