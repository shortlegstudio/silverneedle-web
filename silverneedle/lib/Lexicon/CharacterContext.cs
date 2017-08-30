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
            this.Add("character-father-name", character.Get<History>().FamilyTree.Father);
            this.Add("character-mother-name", character.Get<History>().FamilyTree.Mother);
        }
    }
}