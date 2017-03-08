// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
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

        public ICharacterNamesGateway Names { get; set; }

        public IHomelandGateway Homelands { get; set; }

        public IDrawbackGateway Drawbacks { get; set; }

        public IClassOriginGateway ClassOrigins { get; set; }

        public void SetAllYaml() {
            Armors = new ArmorGateway();
            Weapons = new WeaponGateway();
            Skills = new EntityGateway<Skill>();
            Names = new CharacterNamesGateway();
            Homelands = new HomelandGateway();
            Drawbacks = new DrawbackGateway();
            ClassOrigins = new ClassOriginGateway();
        }

        public EntityGateway<T> GetImpl<T>() where T : IGatewayObject
        {
            return new EntityGateway<T>();
        }

        public static EntityGateway<T> Get<T>() where T : IGatewayObject
        {
            return Instance().GetImpl<T>();
        }
    }
}