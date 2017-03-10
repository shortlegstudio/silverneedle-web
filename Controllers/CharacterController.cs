// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SilverNeedle;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Characters;
using SilverNeedle.Utility;


namespace silverneedleweb.Controllers
{
    public class CharacterController : Controller
    {
        private EntityGateway<CharacterBuildStrategy> strategyGateway = GatewayProvider.Get<CharacterBuildStrategy>();

        public IActionResult Index()
        {
            var strategies = strategyGateway.All();
            ViewData["Strategies"] = strategies.Select(x => x.Name).ToArray();
            
            return View();
        }

        public IActionResult Character(string strategy, int level)
        {
            var gen = GatewayProvider.Get<CharacterDesigner>().Find("create-default");
            var build = strategyGateway.Find(strategy);

            var character = new CharacterSheet();
            gen.Process(character, build);
            
            ViewData["character"] = new CharacterSheetTextView(character);
            ViewData["strategy"] = strategy;
            ViewData["level"] = level;
            return View();
        }

        public IActionResult Group()
        {
            var strategies = strategyGateway.All();
            ViewData["Strategies"] = strategies.Select(x => x.Name).ToArray();
            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
