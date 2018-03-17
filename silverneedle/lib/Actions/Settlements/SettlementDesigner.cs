// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Settlements
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Settlements;
    using SilverNeedle.Utility;

    public class SettlementDesigner : ISettlementDesignStep, IGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; private set; }

        public IEnumerable<ISettlementDesignStep> DesignSteps { get { return designSteps; } }
        private IList<ISettlementDesignStep> designSteps = new List<ISettlementDesignStep>();

        public SettlementDesigner(IObjectStore configuration)
        {
            configuration.Deserialize(this);
            LoadDesignSteps(configuration.GetObjectList("steps"));
        }
        public void Execute(Settlement settlement)
        {
            foreach(var step in designSteps)
            {
                step.Execute(settlement);
            }
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        private void LoadDesignSteps(IEnumerable<IObjectStore> steps)
        {
            foreach(var step in steps)
            {
                var typeName = step.GetString("step");
                designSteps.Add(typeName.Instantiate<ISettlementDesignStep>(step));
            }
        }
    }
}