// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public interface IArmor : IGear
    {
        int ArmorClass { get; }
        int MaximumDexterityBonus { get; }
        int ArmorCheckPenalty { get; }
        int ArcaneSpellFailureChance { get; }
        int MovementSpeedPenalty20 { get; }
        int MovementSpeedPenalty30 { get; }
        ArmorType ArmorType { get; }
    }
}