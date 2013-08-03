using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApp
{
    class Program
    {
        private static readonly Decimal NO_PRICE_INFORMATION = -1;

        private delegate void StockPriceChanged(String stockSymbol, Decimal oldPrice, Decimal newPrice);

        private static event StockPriceChanged PriceChanged;

        static void Main(string[] args)
        {
            String stockToTrack;

            SetupStockTrackingService(out stockToTrack);
            SetupUserConcerns();

            Decimal oldPrice = NO_PRICE_INFORMATION;
            Decimal currentPrice = NO_PRICE_INFORMATION;

            // Pretend to be a stock service (a feed that sends events).
            // Don't put any business logic in here -- this "stock service" should just
            // send out an event when the stock price changes.
            while (true) {
                Console.WriteLine("Enter the current price of the stock as it changes (\"q\" to quit):");
                String currentPriceString = Console.ReadLine();
                if (currentPriceString.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
                oldPrice = currentPrice;
                currentPrice = Decimal.Parse(currentPriceString);
                if ((oldPrice != currentPrice) && (PriceChanged != null))
                {
                    PriceChanged(stockToTrack, oldPrice, currentPrice);
                }
            }
        }

        private static void SetupStockTrackingService(out String stockToTrack)
        {
            Console.WriteLine("Enter a stock to track:");
            stockToTrack = Console.ReadLine();
        }

        private static void SetupUserConcerns() {
            StockHolder user = new StockHolder();

            Console.WriteLine("Enter a buy price (stock will be purchased if the price drops below this amount):");
            user.BuyPrice = Decimal.Parse(Console.ReadLine());
            
            Console.WriteLine("Enter a sell price (stock will be sold if the price rises above this amount):");
            user.SellPrice = Decimal.Parse(Console.ReadLine());

            PriceChanged += user.HandlePriceChanged;

            Console.WriteLine("Do you want a record of your stock purchases?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                user.Purchase += StockApi.RecordStockPurchase;
            }

            Console.WriteLine("Do you want a record of your stock sales?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                user.Sale += StockApi.RecordStockSale;
            }

            Console.WriteLine("Do you want to file an SEC report of your stock transactions?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                user.Purchase += ReportPurchaseToSec;

                user.Sale += (String stockSymbol, Decimal salePrice, int sharesSold) =>
                {
                    StockApi.FileSECReport("sold", sharesSold, stockSymbol, salePrice);
                };
            }
        }

        private static void ReportPurchaseToSec(String stockSymbol, Decimal purchasePrice, int sharesPurchased) 
        {
            StockApi.FileSECReport("bought", sharesPurchased, stockSymbol, purchasePrice);
        }
    }
}
