// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using Xunit;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;

    public class IgnoreSpellsOfOpposingSchoolsTests
    {
        [Fact]
        public void IfOpposingSchoolThenDoNotAllowCasting()
        {
            var rule = new IgnoreSpellsOfOpposingSchools();
            var wizard = CharacterTestTemplates.Wizard().WithWizardCasting();
            var casting = wizard.Get<WizardCasting>();
            var school = ArcaneSchool.CreateForTesting("evocation", false);
            casting.SetOppositionSchools(new IArcaneSchool[] { school });
            wizard.Add(rule);

            var ignoreSpell = new Spell("Fireball", "evocation");
            var allowSpell = new Spell("Entangle", "transmutation");
            Assert.False(rule.CanCastSpell(ignoreSpell));
            Assert.True(rule.CanCastSpell(allowSpell));

        }
    }
}