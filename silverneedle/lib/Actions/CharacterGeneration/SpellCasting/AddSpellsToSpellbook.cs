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

    public class AddSpellsToSpellbook : ICharacterDesignStep, ICharacterFeatureCommand
    {
        private string[] spellsToAdd;
        private bool addModifier = false;
        public AddSpellsToSpellbook(IObjectStore configuration)
        {
            spellsToAdd = configuration.GetList("spells");
            addModifier = configuration.GetBoolOptional("add-modifier");
        }
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var casting = components.Get<ISpellCasting>();
            var spellList = casting.SpellList;
            var book = components.Get<Inventory>().Spellbooks.First();
            for(int level = 0; level < spellsToAdd.Length; level++)
            {
                if(spellsToAdd[level].EqualsIgnoreCase("ALL"))
                {
                    book.AddSpells(level, spellList.GetSpells(level, components.GetAll<ISpellCastingRule>()));
                }
                else
                {
                    int spellsToChoose = spellsToAdd[level].ToInteger();
                    if(addModifier)
                        spellsToChoose += casting.CastingAbility.TotalModifier;

                    book.AddSpells(
                        level,
                        spellList.GetSpells(level, components.GetAll<ISpellCastingRule>()).Where(x => !book.ContainsSpell(level, x))
                        .Choose(spellsToChoose)
                    );
                }
            }
        }
    }
}