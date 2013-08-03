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

        public decimal BuyPrice { private get; set; }
        public decimal SellPrice { private get; set; }
        public int SharesHeld { get; private set; }

        public void HandlePriceChanged(String stockSymbol, Decimal oldPrice, Decimal newPrice)
        {
            if (newPrice > SellPrice)
            {
                int sharesSold = SharesHeld;
                SharesHeld = 0;
                if (Sale != null)
                {
                    Sale(stockSymbol, newPrice, sharesSold);
                }
            }
            if (newPrice < BuyPrice)
            {
                SharesHeld += 100;
                if (Purchase != null)
                {
                    Purchase(stockSymbol, newPrice, 100);
                }
            }
        }

        public void Sell(string stockSymbol, decimal salePrice)
        {
            if (SharesHeld != 0)
            {
                Sale(stockSymbol, salePrice, SharesHeld);
                SharesHeld = 0;
            }
        }
    }
}
