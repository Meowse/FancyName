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
                        
        public delegate void StockHasBeenPurchase(String stock, Decimal purchasePrice, int sharesPurchased);
        public delegate void StockHasBeenSale(String stock, Decimal salePrice, int sharesSold);
        public delegate void SECHasBeenReported(String transaction, int numberOfShares, String stock, Decimal price)

        public static event StockHasBeenPurchase purchased;  
        public static event StockHasBeenSale sold;
        public static event SECHasBeenReported SEC;




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
                if (oldPrice != currentPrice)
                {
                    //PriceChanged(oldPrice, currentPrice);
                    Console.WriteLine("We should announce that stock {0} has changed from {1} to {2}.", stockToTrack, oldPrice, currentPrice);
                    
                    
                }
            }
        }

        private static void SetupStockTrackingService(out String stockToTrack)
        {
            Console.WriteLine("Enter a stock to track:");
            stockToTrack = Console.ReadLine();
        }

        public static void SetupUserConcerns() {
            Console.WriteLine("Enter a buy price (stock will be purchased if the price drops below this amount):");
            Decimal buyPrice = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter a sell price (stock will be sold if the price rises above this amount):");
            Decimal sellPrice = Decimal.Parse(Console.ReadLine());

            Console.WriteLine("Do you want a record of your stock purchases?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                purchased += RecordStockPurchase;
                
            }

            Console.WriteLine("Do you want a record of your stock sales?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                sold += RecordStockSale;
            }

            Console.WriteLine("Do you want to file an SEC report of your stock transactions?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                SEC += FileSECReport;
            }

        }

        // Action methods -- you can call these, but don't change them.
        // Pretend that they are a fixed API.
        public static void RecordStockPurchase(String stock, Decimal purchasePrice, int sharesPurchased)
        {
            
            Console.WriteLine("You purchased {0} shares of {1} at ${2} per share.", sharesPurchased, stock, purchasePrice);
        }

        private static void RecordStockSale(String stock, Decimal salePrice, int sharesSold)
        {
            Console.WriteLine("You sold {0} shares of {1} at ${2} per share.", sharesSold, stock, salePrice);
        }

        private static void FileSECReport(String transaction, int numberOfShares, String stock, Decimal price)
        {
            Console.WriteLine("The SEC has been informed that you {0} {1} shares of {2} at ${3}.", transaction, numberOfShares, stock, price);
        }
    }
}
