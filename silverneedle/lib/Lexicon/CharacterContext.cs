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
            this.Add("charactersheet", character);
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
            //TODO: HairStyle/Color should not use create description here. Need static descriptions
            this.Add("eyecolor", character.Appearance.EyeColor.CreateDescription());
            this.Add("hairstyle", character.Appearance.HairStyle.CreateDescription());
            this.Add("haircolor", character.Appearance.HairColor.CreateDescription());
            this.Add("physical-features", character.Appearance.PhysicalAppearance);
        }
    }
}