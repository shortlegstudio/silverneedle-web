// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System;

    public class MissingDataFileException : System.Exception
    {
        public MissingDataFileException() { }
        public MissingDataFileException(string message) : base(message) { }
        public MissingDataFileException(string message, System.Exception inner) : base(message, inner) { }

        public static MissingDataFileException MissingDataFilesForType(string typeName)
        {
            return new MissingDataFileException(string.Format("Missing data file for type: {0}", typeName));
        }
    }
}