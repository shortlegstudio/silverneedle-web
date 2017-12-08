// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters;

    public class UpdateArcaneSchoolPowersTests
    {
        [Fact]
        public void AddsAnyNewPowersAtTheCurrentLevel()
        {
            var wizard = CharacterTestTemplates.Wizard().WithWizardCasting();
            var casting = wizard.Get<WizardCasting>();

            var school = ArcaneSchool.CreateForTesting("School", true);
            school.AddPower(1, "Tests.Actions.CharacterGeneration.ClassFeatures.UpdateArcaneSchoolPowersTests+SamplePower");
            casting.SetFocusSchool(school);

            var step = new UpdateArcaneSchoolPowers();
            step.ExecuteStep(wizard);
            Assert.NotNull(wizard.Get<SamplePower>());
        }

        public class SamplePower 
        {

        }
    }
}