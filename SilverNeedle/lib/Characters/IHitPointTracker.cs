// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Characters 
{
    public interface IHitPointTracker
    {
        int MaxHitPoints { get; }
        int CurrentHitPoints { get; }
        void SetMaxHitPoints(int hp);
        void SetCurrentHitPoints(int hp);
        void IncreaseHitPoints(int hp);
    }
}