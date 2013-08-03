using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
