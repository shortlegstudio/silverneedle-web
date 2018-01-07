// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Utility; 

    public interface ICharacterFeatureCommand
    {
        void Execute(ComponentContainer components);        
    }
}