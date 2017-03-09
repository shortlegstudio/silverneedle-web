// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle
{
    using SilverNeedle.Characters.Background;
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

        public IHomelandGateway Homelands { get; set; }

        public void SetAllYaml() {
            Homelands = new HomelandGateway();
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