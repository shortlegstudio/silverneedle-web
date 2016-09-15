// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Actions.CharacterGenerator
{
    using SilverNeedle.Characters;

    public class FeatSelector
    {
        IFeatGateway feats;

        public FeatSelector(IFeatGateway feats)
        {
            this.feats = feats;
        }

        public void SelectFeats(CharacterSheet character, WeightedOptionTable<string> preferredFeats)
        {
            var selection = preferredFeats.ChooseRandomly();
            var feat = feats.GetByName(selection);
            character.AddFeat(feat);
        }

    }

}