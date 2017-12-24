// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class IntenseSpells : SpecialAbility, IComponent
    {
        private ClassLevel sourceLevel;

        public int BonusDamage
        {
            get
            {
                return (sourceLevel.Level / 2).AtLeast(1);
            }
        }

        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public override string Name
        {
            get
            {
                return "{0} ({1} spell damage)".Formatted(base.Name, BonusDamage.ToModifierString());
            }
        }
    }
}