// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;

    public class SelectArcaneOppositionSchools : ICharacterDesignStep
    {
        private EntityGateway<ArcaneSchool> arcaneSchools;
        public SelectArcaneOppositionSchools() : this(GatewayProvider.Get<ArcaneSchool>())
        {

        }

        public SelectArcaneOppositionSchools(EntityGateway<ArcaneSchool> schools)
        {
            this.arcaneSchools = schools;
        }

        public void ExecuteStep(CharacterSheet character)
        {
            var wizardCasting = character.Get<WizardCasting>();
            var focusSchool = wizardCasting.FocusSchool;
            if(focusSchool.NoOppositionSchools)
                return;

            var opps = arcaneSchools.Where(
                x => x.Equals(focusSchool) == false && !x.NoOppositionSchools
                ).Choose(2);
            wizardCasting.SetOppositionSchools(opps);
        }
    }
}