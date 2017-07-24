// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    using System.Linq;
    using System.Collections.Generic;

    public static class PrerequisiteExtensions
    {
        public static IEnumerable<T> GetAllQualified<T>(this IEnumerable<IHasPrerequisites> list, CharacterSheet character)
        {
            return list.Where(x => x.IsQualified(character)).Cast<T>();
        }
    }
}