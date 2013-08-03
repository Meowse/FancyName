using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultsFromDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            Subscriber sub1 = new Subscriber("Subscriber 1");
            Subscriber sub2 = new Subscriber("Subscriber 2");
            pub.Announce += sub1.Listen;
//            pub.Announce += sub2.Listen;

            pub.SpreadTheWord("Good news!");
            pub.SpreadTheWord("Subscriber 1: Bad news!");
            pub.SpreadTheWord("Subscriber 2: Bad news!");

            Console.ReadKey();
        }
    }
}
