// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Tasks
{
    using SilverNeedle.Serialization;

    public class OccupationSlot
    {
        public OccupationSlot(IObjectStore store)
        {
            store.Deserialize(this);
        }

        [ObjectStore("acceptable-occupations")]
        public string[] AcceptableOccupations { get; private set; }

        [ObjectStoreOptional("open-slots", 1)]
        public int OpenSlots { get; private set; }
    }
}