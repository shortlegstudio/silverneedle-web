// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using System.Linq;
    public class SpecialAbilityPrerequisite : IPrerequisite
    {
        public string SpecialAbilityName { get; set; }
        public SpecialAbilityPrerequisite(string name)
        {
            SpecialAbilityName = name;
        }

        /// <summary>
        /// Determines whether this instance is qualified the specified character.
        /// </summary>
        /// <returns>true if the character is qualified</returns>
        /// <param name="character">Character to assess qualification.</param>
        public bool IsQualified(CharacterSheet character)
        {
            return 
                character.Components.GetAll<SpecialAbility>().Any(x => x.Name.EqualsIgnoreCase(SpecialAbilityName))
                || character.Components.GetAll<Trait>().Any(x => x.Name.EqualsIgnoreCase(SpecialAbilityName))
                || character.Components.GetAll<Feat>().Any(x => x.Name.EqualsIgnoreCase(SpecialAbilityName));
        }
    }
}