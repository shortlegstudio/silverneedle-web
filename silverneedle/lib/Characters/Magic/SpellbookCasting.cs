// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    public class SpellbookCasting : SpellCasting, ICastingPreparation
    {
        private Spellbook spellbook;
        private Dictionary<int, IList<string>> preparedSpells = new Dictionary<int, IList<string>>();
        private Inventory inventory;
        public SpellbookCasting(IObjectStore configuration) : base(configuration)
        {
        }

        public SpellbookCasting(IObjectStore configuration, EntityGateway<SpellList> spellLists) : base(configuration, spellLists)
        {

        }

        public override void Initialize(ComponentBag components)
        {
            base.Initialize(components);
            spellbook = new Spellbook();
            this.inventory = components.Get<Inventory>();
            inventory.AddGear(spellbook);
        }

        public override IEnumerable<string> GetKnownSpells(int spellLevel)
        {
            return this.inventory.Spellbooks.SelectMany(x => x.GetSpells(spellLevel));
        }

        public void PrepareSpell(int level, string spellName)
        {
            var spells = GetKnownSpells(level);

            if(!spells.Contains(spellName))
                throw new CannotPrepareSpellException();

            if(!this.preparedSpells.ContainsKey(level))
                preparedSpells[level] = new List<string>();

            this.preparedSpells[level].Add(spellName);
        }

        public override IEnumerable<string> GetReadySpells(int spellLevel)
        {
            if(!this.preparedSpells.ContainsKey(spellLevel))
                return new string[] { };
            return this.preparedSpells[spellLevel];
        }

        public void PrepareSpells(int level, IEnumerable<string> spells)
        {
            foreach(var spell in spells)
            {
                PrepareSpell(level, spell);
            }
        }
    }
}