// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle
{
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Names;
    using SilverNeedle.Utility;
    public class GatewayProvider
    {
        private static GatewayProvider __instance;
        public static GatewayProvider Instance() 
        {
            if(__instance == null)
                __instance = new GatewayProvider();

            return __instance;
        }
        
        public GatewayProvider()
        {
            SetAllYaml();
        }

        public IArmorGateway Armors { get; set; }
        public IWeaponGateway Weapons { get; set; }
        public IEntityGateway<Skill> Skills { get; set; }

        public IClassGateway Classes { get; set; }

        public IRaceMaturityGateway Maturity { get; set; }

        public IRaceGateway Races { get; set; }

        public ICharacterNamesGateway Names { get; set; }

        public IFeatGateway Feats { get; set; }

        public void SetAllYaml() {
            Armors = new ArmorGateway();
            Weapons = new WeaponGateway();
            Skills = new EntityGateway<Skill>();
            Classes = new ClassGateway();
            Maturity = new RaceMaturityGateway();
            Races = new RaceGateway();
            Names = new CharacterNamesGateway();
            Feats = new FeatGateway();
        }

        public EntityGateway<T> Get<T>()
        {
            return new EntityGateway<T>();
        }
    }
}