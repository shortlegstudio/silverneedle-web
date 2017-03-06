// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class CharacterCreator : ICharacterBuildStep
    {
        public string Name { get; private set; }
        public IEnumerable<ICharacterBuildStep> FirstLevelSteps { get { return _firstLevelSteps; } }

        private IList<ICharacterBuildStep> _firstLevelSteps;
        private IList<ICharacterBuildStep> _levelUpSteps;

        public CharacterCreator(IObjectStore data)
        {
            Name = data.GetString("name");
            _firstLevelSteps = new List<ICharacterBuildStep>();
            _levelUpSteps = new List<ICharacterBuildStep>();

            foreach(var step in data.GetObject("level-one-steps").Children)
            {                
                var typeName = step.Value;
                ShortLog.DebugFormat("Adding Build Step: {0}", typeName);
                var item = typeName.Instantiate<ICharacterBuildStep>();
                _firstLevelSteps.Add(item);
            }

            foreach(var step in data.GetObject("level-up-steps").Children)
            {                
                var typeName = step.Value;
                ShortLog.DebugFormat("Adding Build Step: {0}", typeName);
                var item = typeName.Instantiate<ICharacterBuildStep>();
                _levelUpSteps.Add(item);
            }
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            foreach(var step in _firstLevelSteps)
            {
                step.ProcessFirstLevel(character, strategy);
            }
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            foreach(var step in _levelUpSteps)
            {
                step.ProcessLevelUp(character, strategy);
            }
        }
    }
}