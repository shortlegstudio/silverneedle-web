// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    public static class DomainTemplates
    {
        public static Air AirDomain()
        {
            var data = new MemoryStore();
            data.SetValue("spells", "");
            data.SetValue("name", "air");
            return new Air(data);
        }
    }
}