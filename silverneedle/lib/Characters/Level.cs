// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Actions;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Level : Feature
    {
        [ObjectStore("level")]
        public int Number { get; private set; }

        public Level()
        {
        }

        public Level(IObjectStore objectStore) : base(objectStore)
        {
            objectStore.Deserialize(this);
        }

        public Level(int number) : this()
        {
            Number = number;
        }
    }
}