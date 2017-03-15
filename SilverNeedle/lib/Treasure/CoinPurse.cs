// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Treasure
{
    public class CoinPurse
    {
        public int Value { get { return TotalValue(); } }
        public PlatinumPieces Platinum { get; private set; }
        public GoldPieces Gold { get; private set; }
        public SilverPieces Silver { get; private set; }
        public CopperPieces Copper { get; private set; }

        public CoinPurse()
        {
            ZeroOutPurse();
        }

        public CoinPurse(int value) : this()
        {
            SetValue(value);
        }

        public void AddPlatinum(int pieces)
        {
            Platinum.Pieces += pieces;
        }

        public void AddGold(int pieces)
        {
            Gold.Pieces += pieces;
        }

        public void AddSilver(int pieces)
        {
            Silver.Pieces += pieces;
        }

        public void AddCopper(int pieces)
        {
            Copper.Pieces += pieces;
        }

        public void SetValue(int value)
        {
            ZeroOutPurse();
            Gold = new GoldPieces(value / GoldPieces.VALUE);
            Silver = new SilverPieces((value - this.Value) / SilverPieces.VALUE);
            Copper = new CopperPieces((value - this.Value) / CopperPieces.VALUE);
        }

        public void Spend(int value)
        {
            if(value > this.Value)
            {
                throw new InsufficientFundsException();
            }
            SetValue(Value - value);
        }

        public override string ToString()
        {
            return string.Format("pp: {0} gp: {1} sp: {2} cp: {3}", Platinum.Pieces, Gold.Pieces, Silver.Pieces, Copper.Pieces);
        }

        public bool CanAfford(SilverNeedle.Equipment.IGear item)
        {
            return this.Value >= item.Value;
        }

        private int TotalValue()
        {
            return Platinum.Value +
                Gold.Value +
                Silver.Value + 
                Copper.Value;
        }

        private void ZeroOutPurse()
        {
            Platinum = new PlatinumPieces();
            Gold = new GoldPieces();
            Silver = new SilverPieces();
            Copper = new CopperPieces();
        }
    }
}