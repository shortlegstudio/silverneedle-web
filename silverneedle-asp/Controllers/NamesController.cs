// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using Microsoft.AspNetCore.Mvc;
using SilverNeedle;
using SilverNeedle.Actions.NamingThings;
using SilverNeedle.Characters;
using System.Collections.Generic;

namespace silverneedleweb.Controllers
{
    public class NamesController : Controller
    {
        
        public IActionResult Index()
        {
            var namer = new NameCharacter();
            var races = GatewayProvider.Get<Race>().All();
            var number = 5;
            var results = new List<NamesViewModel>();

            //Foreach race and gender make a collection of names
            foreach(var r in races)
            {
                foreach(var g in EnumHelpers.GetValues<Gender>())
                {
                    var genderRaceNames = new NamesViewModel(r.Name, g);
                    for(int i = 0; i < number; i++)
                    {
                        var name = namer.CreateFullName(g, r.Name);
                        genderRaceNames.Names.Add(name);                        
                    }
                    results.Add(genderRaceNames);
                }
            }
            ViewData["names"] = results;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public class NamesViewModel
        {
            public List<string> Names { get; private set; }
            public string Race { get; set ; }
            public Gender Gender { get; set; }

            public NamesViewModel(string race, Gender gender)
            {                
                Race = race;
                Gender = gender;
                Names = new List<string>();
            }
        }
    }
}
