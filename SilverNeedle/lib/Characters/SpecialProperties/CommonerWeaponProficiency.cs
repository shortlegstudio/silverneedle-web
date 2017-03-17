// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;

namespace SilverNeedle.Characters.SpecialProperties
{
    using SilverNeedle.Equipment;
    
    public class CommonerWeaponProficiency : IDynamicPropertyEvaluator
    {

        
        public string GetString()
        {
            var weapons = GatewayProvider.Get<Weapon>();
            var choice = weapons.Where(x => x.Level == WeaponTrainingLevel.Simple).ChooseOne().Name;
            ShortLog.DebugFormat("Commoner Weapon Proficiency: {0}", choice);
            return choice;
        }
    }
}