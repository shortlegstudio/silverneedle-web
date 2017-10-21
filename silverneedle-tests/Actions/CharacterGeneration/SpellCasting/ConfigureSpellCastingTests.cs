// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.SpellCasting
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    
    public class ConfigureSpellCastingTests
    {
        [Fact]
        public void IfNotACasterDoNothing()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.SetClass(Class.None);
            var subject = new ConfigureSpellCasting();
            subject.ExecuteStep(character, new CharacterStrategy());
        }
    }
}