// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    public class GatewayProvider
    {
        private static GatewayProvider __instance;
        private Dictionary<System.Type, object> gateways;
        private object _threadLock = new object();

        public static GatewayProvider Instance() 
        {
            if(__instance == null)
                __instance = new GatewayProvider();

            return __instance;
        }

        protected GatewayProvider()
        {
            gateways = new Dictionary<System.Type, object>();
        }

        public EntityGateway<T> GetImpl<T>() where T : IGatewayObject
        {
            lock(_threadLock)
            {
                var type = typeof(T);
                if(gateways.ContainsKey(type)) {
                    return (EntityGateway<T>)gateways[type];
                }
                var newGateway = EntityGateway<T>.LoadFromDataFiles();
                gateways.Add(type, newGateway);
                return newGateway;
            }
        }

        public static EntityGateway<T> Get<T>() where T : IGatewayObject
        {
            return Instance().GetImpl<T>();
        }

        public static T Find<T>(string name) where T : IGatewayObject
        {
            return Get<T>().Find(name);
        }

        public static IEnumerable<T> All<T>() where T : IGatewayObject
        {
            return Get<T>().All();
        }
    }
}