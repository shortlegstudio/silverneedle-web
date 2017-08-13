// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.MagicItemGeneration
{
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;

    public class WandCreator
    {
        EntityGateway<Spell> spells;
        EntityGateway<SpellList> spellLists;
        
        private string[] classes = new string[] { "wizard", "cleric", "druid" };

        public WandCreator()
        {
            spells = GatewayProvider.Get<Spell>();
            spellLists = GatewayProvider.Get<SpellList>();
        }

        public WandCreator(EntityGateway<Spell> spells, EntityGateway<SpellList> spellLists)
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
            var value = 75000 * (spellLevel.Key) * (spellLevel.Key + spellLevel.Key - 1);
            var wand = new Wand(spell, 50, value);
            return wand;
        }

    }
}