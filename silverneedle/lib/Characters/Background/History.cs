// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Background
{
    public class History
    {
        public History()
        {
            Homeland = new Homeland();
            FamilyTree = new FamilyTree();
            ClassOriginStory = new ClassOrigin();
            Drawback = new Drawback();
            BirthCircumstance = new BirthCircumstance();
        }

        public Homeland Homeland { get; set; }
        public FamilyTree FamilyTree { get; set; }
        public ClassOrigin ClassOriginStory { get; set; }
        public Drawback Drawback { get; set; }
        public BirthCircumstance BirthCircumstance { get; set; }
    }
}

