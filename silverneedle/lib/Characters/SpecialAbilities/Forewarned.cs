// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class Forewarned : AbilityDisplayAsName, IComponent
    {
        private ClassLevel sourceLevel;
        private DelegateStatModifier initModifier;

        public void Initialize(ComponentContainer components)
        {
            sourceLevel = components.Get<ClassLevel>();
            initModifier = new DelegateStatModifier(
                "initiative",
                "bonus", 
                () => { return (sourceLevel.Level/2).AtLeast(1); 
            });
            components.Add(initModifier);
        }
    }
}