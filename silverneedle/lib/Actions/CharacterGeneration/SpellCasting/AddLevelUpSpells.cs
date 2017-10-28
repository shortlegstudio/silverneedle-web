// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.SpellCasting
{
    using System;
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

        public void ExecuteStep(CharacterSheet character)
        {
            var spellCasting = character.GetAll<ISpellCasting>();
            foreach(var sc in spellCasting)
            {
                //TODO: This should be replaced by using the specific implementation
                switch(sc.SpellsKnown)
                {
                    case SpellsKnown.All:
                        HandleALLSpellCaster(sc);
                        break;
                    case SpellsKnown.Domain:
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }    


        private void HandleALLSpellCaster(ISpellCasting spellCasting)
        {
            //check if we have spells for the highest Level
            int maxLevel = spellCasting.MaxLevel;
            int nextLevel = maxLevel + 1;
            if(spellCasting.GetSpellsPerDay(nextLevel) > 0)
            {
                var spellList = spellLists.Find(spellCasting.SpellList);    
                spellCasting.AddSpells(nextLevel, spells.FindAll(spellList.Levels[nextLevel]).ToArray());
            }
        }
    }
}