// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    public class CharacterContext : PhraseContext
    {
        public CharacterContext(CharacterSheet character)
        {
            this.Add("name", character.Name);
            this.Add("pronoun", character.Gender.Pronoun());
            this.Add("possessivepronoun", character.Gender.PossessivePronoun());
            this.Add("character-sheet", character);
            AddParentInformation(character);
        }

        private void AddParentInformation(CharacterSheet character)
        {
            var father = character.Get<History>().FamilyTree.Father;
            var mother = character.Get<History>().FamilyTree.Mother;
            this.Add("character-father-name", father.Name);
            this.Add("character-mother-name", mother.Name);
            this.Add("character-father-job", father.GetOrDefault<Occupation>(Occupation.Unemployed()).Name);
            this.Add("character-mother-job", mother.GetOrDefault<Occupation>(Occupation.Unemployed()).Name);
        }
    }
}