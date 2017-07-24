// Copyright (c) 2017 Trevor Redfern
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SilverNeedle;
using SilverNeedle.Actions.Settlements;
using SilverNeedle.Characters;
using SilverNeedle.Groups;
using SilverNeedle.Serialization;


namespace silverneedleweb.Controllers
{
    public class SettlementController : Controller
    {
        private EntityGateway<CharacterBuildStrategy> strategyGateway = GatewayProvider.Get<CharacterBuildStrategy>();

        public IActionResult Index()
        {
            var strategies = this.strategyGateway.All();
            this.ViewData["Strategies"] = strategies.Select(x => x.Name).ToArray();
            return this.View();
        }


        public IActionResult Settlement(string strategy, int number)
        {
            var settlementBuilder = new SettlementBuilder();
            var settlement = settlementBuilder.Create(number);
            this.ViewData["settlement"] = new SettlementTextView(settlement);
            this.ViewData["strategy"] = strategy;
            this.ViewData["level"] = 1;
            return this.View();
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
