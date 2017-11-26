// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.Collections.Generic;
    public interface IGatewayLoader<T>
    {
        IEnumerable<T> Load(IObjectStore configuration);
    }
}