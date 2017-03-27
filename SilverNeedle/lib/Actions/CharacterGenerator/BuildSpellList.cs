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

            switch(character.Class.Spells.Known)
            {
                case SpellsKnown.Spellbook:
                    BuildSpellbook(character, strategy);
                    break;
            }
        }


        private void BuildSpellbook(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.CasterLevel = 1;
            var spellList = spellLists.Find(character.Class.Spells.List);
            var spellbook = new Spellbook();
            character.Inventory.AddGear(spellbook);
            //Assign All Zero Level Spells
            spellbook.Spells.Add(0, spellList.Levels[0]);

            // Choose three first level spells
            spellbook.Spells.Add(1, spellList.Levels[1].Choose(3).ToArray());
        }
    }
}