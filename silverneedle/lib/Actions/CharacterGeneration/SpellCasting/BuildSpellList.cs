// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.SpellCasting
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
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            if(!character.Contains<SpellCasting>())
                return;

            foreach(var spellcasting in character.GetAll<SpellCasting>())
            {
                SpellList spellList;
                switch(spellcasting.SpellsKnown)
                {
                    case SpellsKnown.Spellbook:
                        spellList = spellLists.Find(spellcasting.SpellList);
                        BuildSpellbook(character, strategy, spellList);
                        break;

                    case SpellsKnown.All:
                        spellList = spellLists.Find(spellcasting.SpellList);
                        AddAllSpells(spellcasting, strategy, spellList);
                        break;
                    
                    case SpellsKnown.Domain:
                        break;
                }
            }
        }


        private void BuildSpellbook(CharacterSheet character, CharacterStrategy strategy, SpellList spellList)
        {
            var spellbook = new Spellbook();
            
            character.Inventory.AddGear(spellbook);
            //Assign All Zero Level Spells
            spellbook.AddSpells(0, spellList.Levels[0]);

            // Choose three first level spells
            spellbook.AddSpells(1, spellList.Levels[1].Choose(3).ToArray());
        }

        private void AddAllSpells(SpellCasting casting, CharacterStrategy strategy, SpellList spellList)
        {
            casting.AddSpells(0, spells.FindAll(spellList.Levels[0]).ToArray());
            casting.AddSpells(1, spells.FindAll(spellList.Levels[1]).ToArray());
        }
    }
}