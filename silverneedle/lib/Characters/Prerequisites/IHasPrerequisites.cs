// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using SilverNeedle.Utility;
    public interface IHasPrerequisites
    {
        PrerequisiteList Prerequisites { get; }
        bool IsQualified(ComponentContainer components);
    }
}