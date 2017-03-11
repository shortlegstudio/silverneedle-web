// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class CopperPieces : CoinsBase
    {
        public const int VALUE = 1; 
        public CopperPieces() : base(VALUE)
        {

        }

        public CopperPieces(int pieces) : base(pieces, VALUE)
        {

        }
    }
}