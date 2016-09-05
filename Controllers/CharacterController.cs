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
        private CharacterBuildYamlGateway strategyGateway = new CharacterBuildYamlGateway();

        public IActionResult Index()
        {
            var strategies = strategyGateway.All();
            ViewData["Strategies"] = strategies.Select(x => x.Name).ToArray();
            
            return View();
        }

        public IActionResult Character(string strategy)
        {
            var gateways = new GatewayProvider();
            
            var gen = new CharacterBuilder(
                new StandardAbilityScoreGenerator(),
                new LanguageSelector(new LanguageYamlGateway()),
                new RaceSelector(gateways.Races, new TraitYamlGateway()),
                new NameCharacter(new CharacterNamesYamlGateway()),
                new GatewayProvider()
            );

            var build = strategyGateway.GetBuild(strategy);

            var character = gen.GenerateCharacter(build);
            ViewData["character"] = new CharacterSheetTextView(character);
            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
