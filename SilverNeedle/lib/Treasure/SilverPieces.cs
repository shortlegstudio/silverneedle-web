// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class SilverPieces : CoinsBase
    {
        public const int VALUE = 10; 
        public SilverPieces() : base(VALUE)
        {

        }

        public SilverPieces(int pieces) : base(pieces, VALUE)
        {

        }
    }
}