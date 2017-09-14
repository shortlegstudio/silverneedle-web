// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    public class TypeNotFoundException : System.Exception
    {
        public TypeNotFoundException(string typeName) : base(string.Format("Could not find type: {0}", typeName))
        {

        }

    }
}