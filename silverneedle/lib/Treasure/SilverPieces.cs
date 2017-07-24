// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class SilverPieces : CoinsBase
    {
        public const int VALUE = 10; 
        public const string ABBR = "sp";

        public SilverPieces() : base(VALUE, ABBR)
        {

        }

        public SilverPieces(int pieces) : base(pieces, VALUE, ABBR)
        {

        }
    }
}