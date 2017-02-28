// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using Microsoft.AspNetCore.Mvc;
using SilverNeedle.Actions.Settlements;
using SilverNeedle.Characters;
using SilverNeedle.Groups;
using System.Linq;


namespace silverneedleweb.Controllers
{
    public class SettlementController : Controller
    {
        private CharacterBuildGateway strategyGateway = new CharacterBuildGateway();

        public IActionResult Index()
        {
            var strategies = strategyGateway.All();
            ViewData["Strategies"] = strategies.Select(x => x.Name).ToArray();
            
            return View();
        }


        public IActionResult Settlement(string strategy, int number)
        {
            var settlementBuilder = new SettlementBuilder();
            var settlement = settlementBuilder.Create(number);
            
            ViewData["settlement"] = new SettlementTextView(settlement);
            ViewData["strategy"] = strategy;
            ViewData["level"] = 1;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
