// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    using System;
    public static class Repeat
    {
        public static void Times(int numberOfRepeats, Action repeatedAction)
        {
            for(int count = 0; count < numberOfRepeats; count++)
            {
                repeatedAction();
            }
        }
    }
}