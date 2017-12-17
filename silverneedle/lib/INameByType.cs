// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    public interface INameByType
    {

    }

    public static class INameByTypeExtensions
    {
        public static string Name(this INameByType entity)
        {
            return entity.GetType().Name.Titlize();
        }
    }
}