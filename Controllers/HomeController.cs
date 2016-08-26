using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Actions.CharacterGenerator.Abilities;
using SilverNeedle.Actions.NamingThings;
using SilverNeedle.Characters;
using SilverNeedle.Names;
using SilverNeedle;

namespace silverneedleweb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var gen = new CharacterGenerator(
                new StandardAbilityScoreGenerator(),
                new LanguageSelector(new LanguageYamlGateway()),
                new RaceSelector(new RaceYamlGateway(), new TraitYamlGateway()),
                new NameCharacter(new CharacterNamesYamlGateway())
            );

            var character = gen.GenerateRandomCharacter();
            SetCharacterViewData(character);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private void SetCharacterViewData(CharacterSheet character)
        {
            ViewData["Name"] = character.Name;
            ViewData["Race"] = character.Race.Name;
            ViewData["Initiative"] = character.Initiative.TotalValue.ToModifierString();
            ViewData["Senses"] = character.GetSkillValue("Perception").ToModifierString();
            ViewData["Class"] = character.Class.Name;
            ViewData["Size"] = character.Size.Size.ToString();
            ViewData["Alignment"] = character.Alignment.ToString();
        }
    }
}
