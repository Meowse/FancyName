using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApp
{
    class StockHolder
    {
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int SharesHeld { get; set; }

        public void SellStock(String stockSymbol, Decimal oldPrice, Decimal newPrice)
        {
            if (newPrice > SellPrice)
            {
                StockApi.RecordStockSale(stockSymbol, newPrice, SharesHeld);
                SharesHeld = 0;
            }
        }
    }
}
