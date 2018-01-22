// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class Forewarned : SpecialAbility, IComponent
    {
        private ClassLevel sourceLevel;
        private DelegateStatModifier initModifier;

        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
            var init = components.Get<Initiative>();
            initModifier = new DelegateStatModifier(
                init.Name, 
                "bonus", 
                () => { return (sourceLevel.Level/2).AtLeast(1); 
            });
            init.AddModifier(initModifier);
        }
    }
}