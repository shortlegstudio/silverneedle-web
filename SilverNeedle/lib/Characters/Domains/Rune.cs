// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Rune : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private BlastRune blastRune;
        private SpellRune spellRune;
        public Rune(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            blastRune = new BlastRune(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(blastRune);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                spellRune = new SpellRune();
                components.Add(spellRune);
            }
        }
    }
}