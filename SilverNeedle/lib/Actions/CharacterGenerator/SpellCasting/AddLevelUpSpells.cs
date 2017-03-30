// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.SpellCasting
{
    using SilverNeedle.Characters;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    public class AddLevelUpSpells : ICharacterDesignStep
    {
        EntityGateway<SpellList> spellLists;

        public AddLevelUpSpells()
        {
            spellLists = GatewayProvider.Get<SpellList>();
        }

        public AddLevelUpSpells(EntityGateway<SpellList> spellLists)
        {
            this.spellLists = spellLists;
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            switch(character.SpellCasting.SpellsKnown)
            {
                case SpellsKnown.All:
                    HandleALLSpellCaster(character, strategy);
                    return;
            }
        }    


        private void HandleALLSpellCaster(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            //check if we have spells for the highest Level
            var spellCasting = character.SpellCasting;
            int maxLevel = spellCasting.MaxLevel;
            int nextLevel = maxLevel + 1;
            if(spellCasting.GetSpellsPerDay(nextLevel) > 0)
            {
                var spellList = spellLists.Find(character.Class.Spells.List);    
                spellCasting.AddSpells(nextLevel, spellList.Levels[nextLevel]);
            }
        }
    }
}