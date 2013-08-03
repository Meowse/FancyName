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

            // If you have multiple delegates listening to an event, the return value of the LAST one
            // is the value returned by firing the event.
            pub.Announce += sub1.Listen;
            pub.Announce += sub2.Listen;

            pub.SpreadTheWord("Good news!");
            pub.SpreadTheWord("Subscriber 1: Bad news!");
            pub.SpreadTheWord("Subscriber 2: Bad news!");

            pub.Ask += sub1.AnswerIt;
            pub.Ask += sub2.AnswerIt;
            pub.AskTheQuestion("What is your name?");

            Subscriber sub3 = new Subscriber("Johnny-come-lately");
            pub.Ask += sub3.AnswerIt;

            pub.AskTheQuestion("What is your name, really?");

            Console.ReadKey();
        }
    }
}
