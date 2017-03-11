// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class PlatinumPieces : CoinsBase
    {
        public const int VALUE = 1000; 
        public const string ABBR = "pp";

        public PlatinumPieces() : base(VALUE, ABBR)
        {

        }

        public PlatinumPieces(int pieces) : base(pieces, VALUE, ABBR)
        {

        }
    }
}