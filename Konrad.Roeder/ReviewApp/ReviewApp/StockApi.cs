using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApp
{
    // Action methods -- you can call these, but don't change them.
    // Pretend that they are a fixed API.
    public class StockApi
    {
        public static void RecordStockPurchase(String stock, Decimal purchasePrice, int sharesPurchased)
        {
            Console.WriteLine("You purchased {0} shares of {1} at ${2} per share.", sharesPurchased, stock, purchasePrice);
        }

        public static void RecordStockSale(String stock, Decimal salePrice, int sharesSold)
        {
            Console.WriteLine("You sold {0} shares of {1} at ${2} per share.", sharesSold, stock, salePrice);
        }

        public static void FileSECReport(String transaction, int numberOfShares, String stock, Decimal price)
        {
            Console.WriteLine("The SEC has been informed that you {0} {1} shares of {2} at ${3}.", transaction, numberOfShares, stock, price);
        }
    }
}
