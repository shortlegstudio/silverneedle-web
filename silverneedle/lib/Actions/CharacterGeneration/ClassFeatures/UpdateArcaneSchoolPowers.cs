// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    public class UpdateArcaneSchoolPowers : ICharacterDesignStep, ICharacterFeatureCommand
    {
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var casting = components.Get<WizardCasting>();
            var school = casting.FocusSchool;
            var wizardLevel = casting.Class.Level;

            var abilities = school.GetAbilities();
            foreach(var ability in abilities)
            {
                if(ability.GetInteger("level") == wizardLevel)
                {
                    components.Add(
                        ability.GetString("ability").Instantiate<object>(ability)
                    );
                }
            }
        }
    }
}