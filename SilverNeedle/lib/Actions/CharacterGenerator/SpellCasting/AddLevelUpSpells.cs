// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.SpellCasting
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;

    public class AddLevelUpSpells : ICharacterDesignStep
    {
        EntityGateway<SpellList> spellLists;
        EntityGateway<Spell> spells;

        public AddLevelUpSpells()
        {
            spellLists = GatewayProvider.Get<SpellList>();
            spells = GatewayProvider.Get<Spell>();
        }

        public AddLevelUpSpells(EntityGateway<SpellList> spellLists, EntityGateway<Spell> spells)
        {
            this.spellLists = spellLists;
            this.spells = spells;
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            switch(character.Get<SpellCasting>().SpellsKnown)
            {
                case SpellsKnown.All:
                    HandleALLSpellCaster(character, strategy);
                    return;
            }
        }    


        private void HandleALLSpellCaster(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            //check if we have spells for the highest Level
            var spellCasting = character.Get<SpellCasting>();
            int maxLevel = spellCasting.MaxLevel;
            int nextLevel = maxLevel + 1;
            if(spellCasting.GetSpellsPerDay(nextLevel) > 0)
            {
                var spellList = spellLists.Find(character.Class.Spells.List);    
                spellCasting.AddSpells(nextLevel, spells.FindAll(spellList.Levels[nextLevel]).ToArray());
            }
        }
    }
}