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
            
            //Enable/Disable options based on whether qualified for feat
            foreach(var entry in preferredFeats.All())
            {
                var f = feats.GetByName(entry.Option);
                if(!f.IsQualified(character)) {
                    preferredFeats.Disable(entry.Option);
                }
            }
            
            var selection = preferredFeats.ChooseRandomly();
            var feat = feats.GetByName(selection);
            character.AddFeat(feat);
        }

    }

}