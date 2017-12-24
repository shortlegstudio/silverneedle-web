// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class MetamagicMastery : SpecialAbility, IComponent
    {
        private ClassLevel sourceLevel;
        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public int UsesPerDay
        {
            get { return (sourceLevel.Level - 6) / 2; }
        }

        public override string Name
        {
            get { return "{0} ({1}/day)".Formatted(base.Name, UsesPerDay); }
        }
    }
}