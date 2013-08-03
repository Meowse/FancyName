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

        static void Main(string[] args)
        {
            // A delegate type for hooking up change notifications.
            public delegate void StockPriceChangedEventHandler(object sender, EventArgs e);
            
            
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

        private static void SetupUserConcerns() {
            Console.WriteLine("Enter a buy price (stock will be purchased if the price drops below this amount):");
            Decimal buyPrice = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter a sell price (stock will be sold if the price rises above this amount):");
            Decimal sellPrice = Decimal.Parse(Console.ReadLine());

            StockPriceChanged += PriceChangedEH;

            Console.WriteLine("Do you want a record of your stock purchases?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                // Do something here to record stock purchases
                // setup event listener for stock price changed - if below the buy price then call record stock purchase
                BoughtStock += RecordStockBoughtEH;

            }

            Console.WriteLine("Do you want a record of your stock sales?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                // Do something here to record stock sales
                // setup event listener for stock price changed -- if above the sell price call record stock sale 
                SoldStock += RecordStockSoldEH;
                
            }

            Console.WriteLine("Do you want to file an SEC report of your stock transactions?");
            if (Console.ReadLine().Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                // Do something here to file SEC reports of stock purchases and sales
                // setup event listener for stock price changed -- 
                BoughtStock += ReportToSecEH;
                SoldStock += ReportToSecEH;
            }

        }

        // if stock price changed, check buy or sell conditions
        Void PriceChangedEH()

        // if below the buy price then call record stock purchase
        Void RecordStockBoughtEH()

        // if above the sell price call record stock sale 
        Void RecordStockSoldEH()

        // if below the buy price 
        Void ReportToSecEH()

        
    }
}
