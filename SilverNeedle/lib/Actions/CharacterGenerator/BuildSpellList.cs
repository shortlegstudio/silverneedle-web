// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGenerator
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    public class BuildSpellList : ICharacterDesignStep
    {
        EntityGateway<SpellList> spellLists;
        public BuildSpellList()
        {
            spellLists = GatewayProvider.Get<SpellList>();
        }
        public BuildSpellList(EntityGateway<SpellList> spellLists)
        {
            this.spellLists = spellLists;
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(!character.Class.HasSpells)
                return;

            character.SpellCasting.SpellsKnown = character.Class.Spells.Known;
            character.SpellCasting.CasterLevel = 1;
            var spellList = spellLists.Find(character.Class.Spells.List);

            switch(character.SpellCasting.SpellsKnown)
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
            character.SpellCasting.AddSpells(0, spellList.Levels[0]);
            character.SpellCasting.AddSpells(1, spellList.Levels[1]);
        }
    }
}