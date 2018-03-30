// Copyright (c) 2017 Trevor Redfern
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SilverNeedle;
using SilverNeedle.Actions.Settlements;
using SilverNeedle.Characters;
using SilverNeedle.Settlements;
using SilverNeedle.Serialization;


namespace silverneedleweb.Controllers
{
    public class SettlementController : Controller
    {
        private EntityGateway<SettlementStrategy> strategyGateway = GatewayProvider.Get<SettlementStrategy>();

        public IActionResult Index()
        {
            var strategies = this.strategyGateway.All();
            this.ViewData["Strategies"] = strategies.OrderBy(x => x.MinimumPopulation).Select(x => x.Name).ToArray();
            return this.View();
        }


        public IActionResult Settlement(string strategy)
        {
            var strat = strategyGateway.Find(strategy);
            var settlement = new Settlement(strat);
            var settlementBuilder = GatewayProvider.Find<SettlementDesigner>("create-settlement");
            settlementBuilder.Execute(settlement);

            this.ViewData["settlement"] = settlement;
            this.ViewData["strategy"] = strategy;
            return this.View();
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
