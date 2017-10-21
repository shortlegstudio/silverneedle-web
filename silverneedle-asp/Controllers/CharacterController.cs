// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SilverNeedle;
using SilverNeedle.Actions.CharacterGeneration;
using SilverNeedle.Characters;
using SilverNeedle.Serialization;


namespace silverneedleweb.Controllers
{
    public class CharacterController : Controller
    {
        private EntityGateway<CharacterStrategy> strategyGateway = GatewayProvider.Get<CharacterStrategy>();

        public IActionResult Index()
        {
            var strategies = strategyGateway.All().OrderBy(x => x.Name);
            ViewData["Strategies"] = strategies.ToArray();
            ViewData["ScoreGenerators"] = GatewayProvider.All<AbilityScoreGenerator>();
            return View();
        }

        public IActionResult Character(string strategy, int level, string scores)
        {
            var build = strategyGateway.Find(strategy);
            var roller = GatewayProvider.Find<AbilityScoreGenerator>(scores);
            build.TargetLevel = level;
            build.AbilityScoreRoller = roller.Generator;

            var gen = GatewayProvider.Find<CharacterDesigner>(build.Designer);
            
            var character = new CharacterSheet(build);
            gen.ExecuteStep(character);
            
            ViewData["character"] = new CharacterSheetTextView(character);
            ViewData["characterFull"] = character;
            ViewData["strategy"] = strategy;
            ViewData["scores"] = scores;
            ViewData["level"] = level;
            return View();
        }

        public IActionResult Group()
        {
            var strategies = strategyGateway.All();
            ViewData["Strategies"] = strategies.Select(x => x.Name).ToArray();
            
            return View();
        }

        public IActionResult Strategy(string strategyName)
        {
            var strat = strategyGateway.Find(strategyName);
            if(strat == null)
            {
                strat = new CharacterStrategy();
                strat.Name = "Not Found!";
            }
            return Json(strat);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
