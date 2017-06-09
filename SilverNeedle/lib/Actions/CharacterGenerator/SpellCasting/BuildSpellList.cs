// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGenerator.SpellCasting
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;

    public class BuildSpellList : ICharacterDesignStep
    {
        EntityGateway<SpellList> spellLists;
        EntityGateway<Spell> spells;
        public BuildSpellList()
        {
            spellLists = GatewayProvider.Get<SpellList>();
            spells = GatewayProvider.Get<Spell>();
        }
        public BuildSpellList(EntityGateway<SpellList> spellLists, EntityGateway<Spell> spells)
        {
            this.spellLists = spellLists;
            this.spells = spells;
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(!character.Class.HasSpells)
                return;

            character.Get<SpellCasting>().SpellsKnown = character.Class.Spells.Known;
            character.Get<SpellCasting>().CasterLevel = 1;
            var spellList = spellLists.Find(character.Class.Spells.List);

            switch(character.Get<SpellCasting>().SpellsKnown)
            {
                case SpellsKnown.Spellbook:
                    BuildSpellbook(character, strategy, spellList);
                    break;

                case SpellsKnown.All:
                    AddAllSpells(character, strategy, spellList);
                    break;
            }
        }


        private void BuildSpellbook(CharacterSheet character, CharacterBuildStrategy strategy, SpellList spellList)
        {
            var spellbook = new Spellbook();
            
            character.Inventory.AddGear(spellbook);
            //Assign All Zero Level Spells
            spellbook.AddSpells(0, spellList.Levels[0]);

            // Choose three first level spells
            spellbook.AddSpells(1, spellList.Levels[1].Choose(3).ToArray());
        }

        private void AddAllSpells(CharacterSheet character, CharacterBuildStrategy strategy, SpellList spellList)
        {
            character.Get<SpellCasting>().AddSpells(0, spells.FindAll(spellList.Levels[0]).ToArray());
            character.Get<SpellCasting>().AddSpells(1, spells.FindAll(spellList.Levels[1]).ToArray());
        }
    }
}