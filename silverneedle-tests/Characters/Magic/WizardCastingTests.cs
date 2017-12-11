// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using Xunit;
    using SilverNeedle.Characters.Magic;

    public class WizardCastingTests
    {
        [Fact]
        public void DontUseOpposingSchoolSpellsForAvailableSpellList()
        {
            var wizard = CharacterTestTemplates.Wizard().WithWizardCasting();
            var casting = wizard.Get<WizardCasting>();
            var oppSchool = ArcaneSchool.CreateForTesting("Evocation", false);
            casting.SetOppositionSchools(
                new IArcaneSchool[] { oppSchool }
            );
        }
    }
}