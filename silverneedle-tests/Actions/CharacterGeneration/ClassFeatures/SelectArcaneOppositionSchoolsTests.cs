// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;

    public class SelectArcaneOppositionSchoolsTests
    {
        [Fact]
        public void SelectsTwoOppositionSchoolsThatAreDifferentFromFocusSchool()
        {
            var wizard = CharacterTestTemplates.Wizard().WithWizardCasting();
            var casting = wizard.Get<WizardCasting>();
            var focusSchool = ArcaneSchool.CreateForTesting("focused", false);
            casting.SetFocusSchool(focusSchool);

            var oppSchool1 = ArcaneSchool.CreateForTesting("opp school 1", false);
            var oppSchool2 = ArcaneSchool.CreateForTesting("opp school 2", false);
            var gateway = EntityGateway<ArcaneSchool>.LoadFromList(
                new ArcaneSchool[] { focusSchool, oppSchool1, oppSchool2 }
            );

            var step = new SelectArcaneOppositionSchools(gateway);
            step.ExecuteStep(wizard);
            AssertExtensions.Contains(oppSchool1, casting.OppositionSchools);
            AssertExtensions.Contains(oppSchool2, casting.OppositionSchools);
        }

        [Fact]
        [Repeat(200)]
        public void DoNotUseUniversalistStyleSchoolsForOpposition()
        {
            var wizard = CharacterTestTemplates.Wizard().WithWizardCasting();
            var casting = wizard.Get<WizardCasting>();
            var focusSchool = ArcaneSchool.CreateForTesting("focused", false);
            casting.SetFocusSchool(focusSchool);

            var oppSchool1 = ArcaneSchool.CreateForTesting("opp school 1", false);
            var oppSchool2 = ArcaneSchool.CreateForTesting("opp school 2", false);
            var ignoreSchool = ArcaneSchool.CreateForTesting("universalist", true);
            var gateway = EntityGateway<ArcaneSchool>.LoadFromList(
                new ArcaneSchool[] { focusSchool, oppSchool1, oppSchool2, ignoreSchool }
            );

            var step = new SelectArcaneOppositionSchools(gateway);
            step.ExecuteStep(wizard);
            AssertExtensions.Contains(oppSchool1, casting.OppositionSchools);
            AssertExtensions.Contains(oppSchool2, casting.OppositionSchools);
        }

        [Fact]
        public void UniversalistSchoolsDontSelectOppositionSchools()
        {
            var wizard = CharacterTestTemplates.Wizard().WithWizardCasting();
            var casting = wizard.Get<WizardCasting>();
            var focusSchool = ArcaneSchool.CreateForTesting("focused", true);
            casting.SetFocusSchool(focusSchool);

            var oppSchool1 = ArcaneSchool.CreateForTesting("opp school 1", false);
            var oppSchool2 = ArcaneSchool.CreateForTesting("opp school 2", false);
            var gateway = EntityGateway<ArcaneSchool>.LoadFromList(
                new ArcaneSchool[] { focusSchool, oppSchool1, oppSchool2 }
            );

            var step = new SelectArcaneOppositionSchools(gateway);
            step.ExecuteStep(wizard);
            Assert.Empty(casting.OppositionSchools);
        }
    }
}