// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGeneration
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    public class AddBonusFeatToken : ICharacterDesignStep
    {
        private IEnumerable<string> options;
        private bool ignorePrequisites;
        public AddBonusFeatToken(IObjectStore configuration)
        {
            options = configuration.GetList("options");
            ignorePrequisites = configuration.GetBoolOptional("ignore-prerequisites");
        }

        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var token = new FeatToken(options, ignorePrequisites);
            character.Add(token);
        }
    }
}