// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public abstract class DomainTestBase<T> where T : Domain
    {
        protected T domain;
        protected CharacterSheet character;

        public void InitializeDomain(string name)
        {
            var data = new MemoryStore();
            data.SetValue("spells", "");
            data.SetValue("name", name);
            domain = typeof(T).Instantiate<T>(data);

            character = new CharacterSheet();
            character.InitializeComponents();
            var Class = new Class("cleric");
            character.SetClass(Class);
            character.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 16);
            character.Add(domain);
        }
    }
}