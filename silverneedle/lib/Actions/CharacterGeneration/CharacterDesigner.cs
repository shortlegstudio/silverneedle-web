// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CharacterDesigner : IGatewayObject, ICharacterDesignStep
    {
        public virtual string Name { get; private set; }
        public IEnumerable<ICharacterDesignStep> Steps { get { return designSteps; } }

        private IList<ICharacterDesignStep> designSteps;
        
        public CharacterDesigner()
        {
            Name = "Unset";
            designSteps = new List<ICharacterDesignStep>();
        }

        public CharacterDesigner(IObjectStore data)
        {
            Name = data.GetString("name");
            if (data.HasKey("type"))
            {
                DesignerType = data.GetEnum<Type>("type");
            }
            
            ShortLog.DebugFormat("Loading Character Creator: {0}", Name);
            designSteps = new List<ICharacterDesignStep>();
            
            foreach(var step in data.GetObject("steps").Children)
            {                
                if(step.HasKey("step"))
                {
                    var typeName = step.GetString("step");
                    ShortLog.DebugFormat("Adding Build Step: {0}", typeName);
                    var item = typeName.Instantiate<ICharacterDesignStep>();
                    designSteps.Add(item);
                } 
                else if (step.HasKey("designer"))
                {
                    var designer = step.GetString("designer");
                    ShortLog.DebugFormat("Adding Designer: {0}", designer);
                    var item = new DesignerExecuterStep(designer);
                    designSteps.Add(item);
                }
            }
        }

        public Type DesignerType { get; private set; }

        public virtual void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            ShortLog.DebugFormat("Executing Designer: {0}", Name);
            if (DesignerType == Type.LevelUp)
                ProcessLevelUp(character, strategy);
            else    
                ProcessSteps(character, strategy);
        }

        private void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            while(character.Level < strategy.TargetLevel)
            {
                var currentLevel = character.Level;
                ProcessSteps(character, strategy);
                if(character.Level <= currentLevel)
                {
                    throw new System.InvalidOperationException("Designer needs a step that increments character level");
                }
            }
        }

        private void ProcessSteps(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            foreach (var step in designSteps)
            {
                ShortLog.DebugFormat("Executing Step: {0}", step);
                step.Process(character, strategy);
            }
        }

        public virtual bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }

        public enum Type 
        {
            Normal,
            LevelUp
        }

    }
}