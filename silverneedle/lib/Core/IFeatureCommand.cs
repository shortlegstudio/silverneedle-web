// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using SilverNeedle.Utility; 

    public interface IFeatureCommand
    {
        void Execute(ComponentContainer components);        
    }
}