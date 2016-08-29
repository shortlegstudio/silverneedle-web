// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using Microsoft.AspNetCore.Mvc;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Actions.CharacterGenerator.Abilities;
using SilverNeedle.Actions.NamingThings;
using SilverNeedle.Characters;
using SilverNeedle.Names;


namespace silverneedleweb.Controllers
{
    public class CharacterController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Strategies"] = new string [] {
              "Front-Line",
              "Caster",
              "Healer"
            };
            return View();
        }

        public IActionResult Character()
        {
            var gen = new CharacterGenerator(
                new StandardAbilityScoreGenerator(),
                new LanguageSelector(new LanguageYamlGateway()),
                new RaceSelector(new RaceYamlGateway(), new TraitYamlGateway()),
                new NameCharacter(new CharacterNamesYamlGateway())
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
