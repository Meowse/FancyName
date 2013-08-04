using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ResultsFromDelegates
{
    class Subscriber
    {
        private String _name;

        public Subscriber(String name)
        {
            _name = name;
        }

        internal bool Listen(string text)
        {
            Console.WriteLine("I am {0} and I listened to \"{1}\".", _name, text);
            if (text.StartsWith(_name))
            {
                Console.WriteLine("And no one else can listen to it!");
                return true;
            }
            return false;
        }

        internal string AnswerIt(string question)
        {
            return "My name is " + _name;
        }

        internal string SlowThing(int input)
        {
            Console.WriteLine("I am {0} and I am starting a slow thing.", _name);
            Thread.Sleep(input * 1000);
            Console.WriteLine("I am {0} and I am ending a slow thing.", _name);
            return string.Format("I spent {0} seconds doing a slow thing.", input);
        }
    }
}
