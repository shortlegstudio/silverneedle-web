// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using SilverNeedle.Characters;
using SilverNeedle.Serialization;
using System.Collections.Generic;

namespace SilverNeedle.Actions.CharacterGeneration
{
    public class AddValuesToBuildStrategy : ICharacterDesignStep
    {
        IDictionary<string, IEnumerable<string>> values; 
        public AddValuesToBuildStrategy(IObjectStore configuration)
        {
            values = new Dictionary<string, IEnumerable<string>>();
            foreach(var key in configuration.Keys)
            {
                values[key] = configuration.GetList(key);
            }
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var strategy = character.Strategy;
            foreach(var item in values)
            {
                foreach(var v in item.Value)
                {
                    strategy.AddCustomValue(item.Key, v, 1);
                }
            }
        }
    }
}