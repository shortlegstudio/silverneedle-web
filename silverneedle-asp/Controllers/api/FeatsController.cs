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


namespace silverneedleweb.Controllers.api
{
    public class FeatsController : Controller
    {
        private EntityGateway<Feat> featsGateway = GatewayProvider.Get<Feat>();

        public IActionResult Index()
        {
            var feats = featsGateway.All().Select(
                f =>
                new {
                    name = f.Name,
                }
            );
            return Json(feats);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
