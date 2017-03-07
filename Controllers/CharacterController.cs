// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using Microsoft.AspNetCore.Mvc;
using SilverNeedle;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Actions.CharacterGenerator.Abilities;
using SilverNeedle.Actions.NamingThings;
using SilverNeedle.Characters;
using SilverNeedle.Names;
using System.Linq;


namespace silverneedleweb.Controllers
{
    public class CharacterController : Controller
    {
        private CharacterBuildGateway strategyGateway = new CharacterBuildGateway();

        public IActionResult Index()
        {
            var strategies = strategyGateway.All();
            ViewData["Strategies"] = strategies.Select(x => x.Name).ToArray();
            
            return View();
        }

        public IActionResult Character(string strategy, int level)
        {
            var gen = GatewayProvider.Get<CharacterCreator>().All().First();
            var build = strategyGateway.GetBuild(strategy);

            var character = new CharacterSheet();
            gen.ProcessFirstLevel(character, build);
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
