// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class AuraOfAlignment : IAbility, INameByType, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private CharacterAlignment alignment;
        private ClassLevel level;
        public AuraOfAlignment()
        {
        }

        public string Strength
        {
            get
            { 
                if(level.Level >= 11)
                    return "Overwhelming";
                if(level.Level >= 5)
                    return "Strong";
                if(level.Level >= 2)
                    return "Moderate";

                return "Faint";
            }
        }

        public string DisplayString()
        {
            return "Aura ({0} {1})".Formatted(Strength, alignment.ToString().Titlize());
        }

        public void Initialize(ComponentContainer components)
        {
            alignment = components.Get<CharacterAlignment>();
            level = components.Get<ClassLevel>();
        }
    }
}