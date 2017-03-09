// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class CharacterCreator : IGatewayObject
    {
        public string Name { get; private set; }
        public IEnumerable<ICreateStep> FirstLevelSteps { get { return _firstLevelSteps; } }

        private IList<ICreateStep> _firstLevelSteps;
        private IList<ILevelUpStep> _levelUpSteps;

        public CharacterCreator(IObjectStore data)
        {
            Name = data.GetString("name");
            ShortLog.DebugFormat("Loading Character Creator: {0}", Name);
            _firstLevelSteps = new List<ICreateStep>();
            _levelUpSteps = new List<ILevelUpStep>();

            foreach(var step in data.GetObject("level-one-steps").Children)
            {                
                var typeName = step.GetString("step");
                ShortLog.DebugFormat("Adding Build Step: {0}", typeName);
                var item = typeName.Instantiate<ICreateStep>();
                _firstLevelSteps.Add(item);
            }

            foreach(var step in data.GetObject("level-up-steps").Children)
            {                
                var typeName = step.GetString("step");
                ShortLog.DebugFormat("Adding Build Step: {0}", typeName);
                var item = typeName.Instantiate<ILevelUpStep>();
                _levelUpSteps.Add(item);
            }
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            foreach(var step in _firstLevelSteps)
            {
                step.Process(character, strategy);
            }
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            foreach(var step in _levelUpSteps)
            {
                step.Process(character, strategy);
            }
        }

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}