// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    public class ComponentPrerequisite : IPrerequisite
    {
        [ObjectStore("component")]
        public string ComponentType { get; private set; }

        public ComponentPrerequisite(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }
        public bool IsQualified(ComponentContainer components)
        {
            var t = Reflector.FindType(ComponentType);
            foreach(var c in components.All)
            {
                if(c.Implements(t))
                    return true;
            }

            return false;
        }
    }
}