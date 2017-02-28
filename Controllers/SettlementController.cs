// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using Microsoft.AspNetCore.Mvc;
using SilverNeedle;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Actions.CharacterGenerator.Abilities;
using SilverNeedle.Actions.NamingThings;
using SilverNeedle.Characters;
using SilverNeedle.Names;
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
            var gateways = new GatewayProvider();
            
            var gen = new CharacterBuilder(
                new StandardAbilityScoreGenerator(),
                new LanguageSelector(new LanguageGateway()),
                new RaceSelector(gateways.Races, new TraitGateway()),
                new NameCharacter(new CharacterNamesGateway()),
                new FeatSelector(gateways.Feats),
                new GatewayProvider()
            );

            var build = strategyGateway.GetBuild(strategy);
            var settlement = new Settlement();

            for(int i = 0; i < number; i++)
            {
                var character = gen.GenerateCharacter(build, 1);
                settlement.AddInhabitant(character);                
            }
            
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
