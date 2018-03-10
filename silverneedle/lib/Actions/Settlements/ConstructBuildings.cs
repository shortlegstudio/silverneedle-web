// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.Settlements
{
    using SilverNeedle.Settlements;
    using SilverNeedle.Serialization;
    public class ConstructBuildings : ISettlementDesignStep
    {
        private EntityGateway<Building> buildings = GatewayProvider.Get<Building>();
        public void Execute(Settlement settlement)
        {
            var count = Randomly.Range(5, 15);
            for(int i = 0; i < count; i++)
            {
                settlement.Add(buildings.ChooseOne());
            }
        }
    }
}