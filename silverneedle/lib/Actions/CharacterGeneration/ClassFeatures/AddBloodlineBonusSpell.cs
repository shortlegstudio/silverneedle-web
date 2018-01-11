// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class AddBloodlineBonusSpell : ICharacterDesignStep, ICharacterFeatureCommand
    {
        private int spellLevel;
        public AddBloodlineBonusSpell(IObjectStore configuration)
        {
            spellLevel = configuration.GetInteger("spell-level");
        }
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var bloodline = components.Get<Bloodline>();
            var casting = components.Get<SpontaneousCasting>();
            var classLevel = components.Get<ClassLevel>();
            var bonusSpell = bloodline.GetBonusSpell(classLevel.Level);
            casting.LearnSpell(spellLevel, bonusSpell);
        }
    }
}