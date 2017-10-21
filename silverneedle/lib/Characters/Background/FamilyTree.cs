// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    public class FamilyTree
    {
        private CharacterSheet father;
        private CharacterSheet mother;

        public CharacterSheet Father 
        { 
            get 
            { 
                if(father == null) 
                    father = new CharacterSheet(CharacterStrategy.Default());
                return father;
            } 
        }
        public CharacterSheet Mother 
        { 
            get
            {
                if(mother == null)
                    mother = new CharacterSheet(CharacterStrategy.Default());

                return mother;
             } 
        }

        public string FatherName 
        { 
            get { return Father.Name; }
        }
        public string MotherName 
        { 
            get { return Mother.Name; }
        }
    }
}

