using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApp
{
    class StockHolder
    {
        public delegate void StockPurchased(String stockSymbol, Decimal purchasePrice, int sharesPurchased);
        public delegate void StockSold(String stockSymbol, Decimal salePrice, int sharesSold);

        public event StockPurchased Purchase;
        public event StockSold Sale;

        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int SharesHeld { get; set; }

        public void HandlePriceChanged(String stockSymbol, Decimal oldPrice, Decimal newPrice)
        {
            if (newPrice > SellPrice)
            {
                if (Sale != null)
                {
                    Sale(stockSymbol, newPrice, SharesHeld);
                }
                SharesHeld = 0;
            }
            if (newPrice < BuyPrice)
            {
                if (Purchase != null)
                {
                    Purchase(stockSymbol, newPrice, 100);
                }
                SharesHeld += 100;
            }
        }
    }
}
