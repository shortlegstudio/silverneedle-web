// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    public class ConfigureSneakAttack : ICharacterDesignStep
    {
        private string Damage { get; set; }

        public ConfigureSneakAttack(IObjectStore configuration)
        {
            Damage = configuration.GetString("damage");
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var sneakAttack = character.Get<SneakAttack>();
            if(sneakAttack == null)
            {
                sneakAttack = new SneakAttack();
                character.Add(sneakAttack);
            }
            sneakAttack.SetDamage(this.Damage);
        }
    }
}