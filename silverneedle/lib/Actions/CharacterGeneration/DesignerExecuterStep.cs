// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class DesignerExecuterStep : ICharacterDesignStep
    {
        private string designerName;
        private EntityGateway<CharacterDesigner> gateway;

        public DesignerExecuterStep(string name)
        {
            designerName = name;
        }

        public DesignerExecuterStep(string name, EntityGateway<CharacterDesigner> gateway)
        {
            designerName = name;
            this.gateway = gateway;
        }

        public virtual void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var designer = FindDesigner(designerName);
            designer.ExecuteStep(character, strategy);
        }

        private CharacterDesigner FindDesigner(string name)
        { 
            if(gateway == null)
                gateway = GatewayProvider.Get<CharacterDesigner>();
            
            return gateway.Find(designerName);
        }
    }
}