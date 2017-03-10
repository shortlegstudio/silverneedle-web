// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class CharacterDesigner : IGatewayObject, ICharacterDesignStep
    {
        public string Name { get; private set; }
        public IEnumerable<ICharacterDesignStep> Steps { get { return designSteps; } }

        private IList<ICharacterDesignStep> designSteps;
        
        public CharacterDesigner(IObjectStore data)
        {
            Name = data.GetString("name");
            ShortLog.DebugFormat("Loading Character Creator: {0}", Name);
            designSteps = new List<ICharacterDesignStep>();
            
            foreach(var step in data.GetObject("steps").Children)
            {                
                var typeName = step.GetString("step");
                ShortLog.DebugFormat("Adding Build Step: {0}", typeName);
                var item = typeName.Instantiate<ICharacterDesignStep>();
                designSteps.Add(item);
            }
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            foreach(var step in designSteps)
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