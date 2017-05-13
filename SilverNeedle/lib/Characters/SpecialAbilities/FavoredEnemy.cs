// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using SilverNeedle.Beastiary;
    public class FavoredEnemy : SpecialAbility
    {
        private IDictionary<CreatureType, int> enemies;
        public FavoredEnemy() 
        {
            enemies = new Dictionary<CreatureType, int>();
        }
        public FavoredEnemy(CreatureType type) : this()
        {
            Add(type);
        }
        public IEnumerable<CreatureType> CreatureTypes { get { return enemies.Keys; } }
        public int Bonus(CreatureType type) 
        {
            return enemies[type];
        }
        public void Add(CreatureType additionalType)
        {
            enemies.Add(additionalType, 2);
        }

        public void EnhanceBonus(CreatureType type)
        {
            enemies[type] += 2;
        }

        public override string Name
        {
            get
            {
                
                var buildCharacterList = new List<string>();
                foreach(var type in enemies.Keys)
                {
                    buildCharacterList.Add(string.Format("{0} {1}", type.Name, Bonus(type).ToModifierString()));
                }
                return string.Format("Favored Enemy ({0})", string.Join(", ", buildCharacterList));
            }
        }
    }
}