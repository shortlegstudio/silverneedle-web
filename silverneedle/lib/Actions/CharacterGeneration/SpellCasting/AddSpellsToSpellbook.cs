// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.SpellCasting
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;

    public class AddSpellsToSpellbook : ICharacterDesignStep
    {
        private string[] spellsToAdd;
        public AddSpellsToSpellbook(IObjectStore configuration)
        {
            spellsToAdd = configuration.GetList("spells");
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var casting = character.Get<ISpellCasting>();
            var spellList = casting.SpellList;
            var book = character.Inventory.Spellbooks.First();
            for(int level = 0; level < spellsToAdd.Length; level++)
            {
                if(spellsToAdd[level].EqualsIgnoreCase("ALL"))
                {
                    book.AddSpells(level, spellList.GetSpells(level));
                }
                else
                {
                    int spellsToChoose = spellsToAdd[level].ToInteger();
                    book.AddSpells(
                        level,
                        spellList.GetSpells(level).Choose(spellsToChoose)
                    );
                }
            }
        }
    }
}