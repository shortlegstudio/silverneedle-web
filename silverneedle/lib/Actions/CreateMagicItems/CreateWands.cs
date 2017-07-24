// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CreateMagicItems
{
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;

    public class CreateWands
    {
        EntityGateway<Spell> spells;
        EntityGateway<SpellList> spellLists;
        
        private string[] classes = new string[] { "wizard", "cleric", "druid" };

        public CreateWands()
        {
            spells = GatewayProvider.Get<Spell>();
            spellLists = GatewayProvider.Get<SpellList>();
        }

        public CreateWands(EntityGateway<Spell> spells, EntityGateway<SpellList> spellLists)
        {
            this.spells = spells;
            this.spellLists = spellLists;
        }

        public Wand Process()
        {
            // choose from available spell Lists.
            var list = spellLists.Where(x => classes.Any(cls => x.Matches(cls))).ChooseOne();
            var spellLevel = list.Levels.ChooseOne();
            var spellName = spellLevel.Value.ChooseOne();
            var spell = spells.Find(spellName);
            var wand = new Wand(spell, 50, 0);
            return wand;
        }

    }
}