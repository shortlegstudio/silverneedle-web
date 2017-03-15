// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Groups
{
    using SilverNeedle.Characters;

    public class SettlementTextView
    {
        public CharacterSheetTextView[] Inhabitants { get; set; }

        public SettlementTextView(Settlement settlement)
        {
            Inhabitants = new CharacterSheetTextView[settlement.Population];
            var index = 0;
            foreach(var c in settlement.Inhabitants)
            {
                Inhabitants[index] = new CharacterSheetTextView(c);
                index++;
            }            
        }
    }
}