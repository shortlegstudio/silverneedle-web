// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Linq;
    using SilverNeedle.Utility;

    public class WildShape : SpecialAbility, IComponent
    {
        private ClassLevel druidLevels;
        public int UsesPerDay
        {
            get
            {
                if(druidLevels.Level == 20)
                    return int.MaxValue;

                return 1 + (druidLevels.Level - 4) / 2;
            }
        }

        public override string Name
        {
            get 
            { 
                if(druidLevels.Level == 20)
                    return string.Format("Wild Shape (at will)");
                return string.Format("Wild Shape ({0}/day)", UsesPerDay);
            }
        }

        public void Initialize(ComponentContainer components)
        {
            druidLevels = components.GetAll<ClassLevel>().First(x => x.Class.Name.EqualsIgnoreCase("druid"));
        }
    }
}