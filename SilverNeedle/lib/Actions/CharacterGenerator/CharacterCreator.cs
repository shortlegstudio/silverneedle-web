// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Collections.Generic;
    using SilverNeedle.Utility;

    public class CharacterCreator
    {
        public string Name { get; private set; }
        public IEnumerable<IBuildStep> FirstLevelSteps { get { return _firstLevelSteps; } }

        private IList<IBuildStep> _firstLevelSteps;

        public CharacterCreator(IObjectStore data)
        {
            Name = data.GetString("name");
            _firstLevelSteps = new List<IBuildStep>();
            foreach(var step in data.GetObject("level-one-steps").Children)
            {                
                var typeName = step.Value;
                ShortLog.DebugFormat("Adding Build Step: {0}", typeName);
                var item = typeName.Instantiate<IBuildStep>();
                _firstLevelSteps.Add(item);
            }
        }
    }
}