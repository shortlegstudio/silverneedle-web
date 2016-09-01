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
        public IActionResult Index()
        {
            var strategyRepo = new CharacterBuildYamlGateway();
            var strategies = strategyRepo.All();
            ViewData["Strategies"] = strategies.Select(x => x.Name).ToArray();
            
            return View();
        }

        public IActionResult Character()
        {
            var gen = new CharacterBuilder(
                new StandardAbilityScoreGenerator(),
                new LanguageSelector(new LanguageYamlGateway()),
                new RaceAssigner(new TraitYamlGateway()),
                new NameCharacter(new CharacterNamesYamlGateway()),
                new GatewayProvider()
            );

            var character = gen.GenerateRandomCharacter();
            ViewData["character"] = new CharacterSheetTextView(character);
            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
