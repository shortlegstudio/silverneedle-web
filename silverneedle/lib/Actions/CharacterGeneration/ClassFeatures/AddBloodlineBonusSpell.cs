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

    public class AddBloodlineBonusSpell : ICharacterDesignStep
    {
        private int spellLevel;
        public AddBloodlineBonusSpell(IObjectStore configuration)
        {
            spellLevel = configuration.GetInteger("spell-level");
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var bloodline = character.Get<Bloodline>();
            var casting = character.Get<SpontaneousCasting>();
            var classLevel = character.Get<ClassLevel>();
            var bonusSpell = bloodline.GetBonusSpell(classLevel.Level);
            casting.LearnSpell(spellLevel, bonusSpell);
        }
    }
}