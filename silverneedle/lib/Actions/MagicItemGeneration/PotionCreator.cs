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
    public class PotionCreator
    {
        private EntityGateway<SpellList> spellLists;
        private EntityGateway<Spell> spells;
        private string[] classes = new string[] { "wizard", "cleric", "druid" };
        public PotionCreator(EntityGateway<SpellList> spellLists, EntityGateway<Spell> spells)
        {
            this.spellLists = spellLists;
            this.spells = spells;
        }

        public PotionCreator()
        {
            this.spellLists = GatewayProvider.Get<SpellList>();
            this.spells = GatewayProvider.Get<Spell>();
        }

        public IPotion Process()
        {
            var list = spellLists.Where(x => classes.Any(cls => x.Matches(cls))).ChooseOne();
            var spellLevel = list.Levels.ChooseOne();
            var spellName = spellLevel.Value.ChooseOne();
            var spell = spells.Find(spellName);
            //var value = 75000 * (spellLevel.Key) * (spellLevel.Key + spellLevel.Key - 1);
            var potion = new Potion(spell, 0);
            return potion;
        }
    }
}