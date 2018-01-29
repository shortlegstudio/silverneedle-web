// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters.Magic;

    public class SelectArcaneSchoolsTests : RequiresDataFiles
    {
        [Fact]
        public void SelectsAFocusedSchool()
        {
            var wizard = CharacterTestTemplates.Wizard().WithWizardCasting().WithSkills();
            var select = new SelectArcaneSchool();
            var casting = wizard.Get<WizardCasting>();

            select.ExecuteStep(wizard);
            Assert.NotNull(casting.FocusSchool);
            Assert.NotNull(wizard.Get<IArcaneSchool>());
        }
    }
}