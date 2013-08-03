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

        // if stock price changed, check buy or sell conditions
        Void PriceChangedEH()
        {



        }

        // if
        

        public void BuyStock(String stockSymbol, Decimal oldPrice, Decimal newPrice)
        {
            if (newPrice < SellPrice)
            {
                StockApi.RecordStockSale(stockSymbol, newPrice, SharesHeld);
                SharesHeld = 0;
            }
        }

        public void SellStock(String stockSymbol, Decimal oldPrice, Decimal newPrice)
        {
            if (newPrice > SellPrice)
            {
                StockApi.RecordStockSale(stockSymbol, newPrice, SharesHeld);
                SharesHeld = 0;
            }
        }

        public void FileSECPurchase(String stockSymbol, Decimal newPrice)
        {
            StockApi.RecordStockSale("bought", stockSymbol, Price, SharesHeld);
        }

        public void FileSECSale(String stockSymbol, Decimal oldPrice, Decimal newPrice)
        {
            StockApi.RecordStockSale("sold", stockSymbol, newPrice, SharesHeld);
        }


        // public static void RecordStockPurchase(String stock, Decimal purchasePrice, int sharesPurchased)

        // public static void RecordStockSale(String stock, Decimal salePrice, int sharesSold)

        // public static void FileSECReport(String transaction, int numberOfShares, String stock, Decimal price)
        //                                      bought/sold 


    }
}
