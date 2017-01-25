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
            foreach(var token in character.FeatTokens) 
            {
                //Enable/Disable options based on whether qualified for feat
                foreach(var entry in preferredFeats.All())
                {
                    var f = feats.GetByName(entry.Option);
                    if(!f.IsQualified(character)) {
                        ShortLog.DebugFormat("Feat {0} Disabled - Unqualified", f.Name); 
                        preferredFeats.Disable(entry.Option);
                    }
                    else if(!token.Qualifies(f)) {
                        ShortLog.DebugFormat("Feat {0} Disabled - Token Unable {1}", f.Name, token.ToString()); 
                        preferredFeats.Disable(entry.Option);
                    } else {
                        preferredFeats.Enable(entry.Option);
                    }
                }
                ShortLog.Debug(preferredFeats.ToString());
                var selection = preferredFeats.ChooseRandomly();
                var feat = feats.GetByName(selection);
                character.AddFeat(feat);
            }

            character.FeatTokens.Clear();
        }

    }

}