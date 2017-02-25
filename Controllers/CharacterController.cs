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

            var character = gen.GenerateCharacter(build, level);
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

        public IActionResult CharacterGroup(string strategy, int number)
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
            var characters = new CharacterSheetTextView[number];

            for(int i = 0; i < number; i++)
            {
                var character = gen.GenerateCharacter(build, 1);
                characters[i] = new CharacterSheetTextView(character);
            }
            
            ViewData["characters"] = characters;
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
