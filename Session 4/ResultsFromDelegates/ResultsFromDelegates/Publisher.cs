using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResultsFromDelegates
{
    class Publisher
    {
        public delegate bool Announcement(String text);
        public event Announcement Announce;

        internal void SpreadTheWord(string announcement)
        {
            if (Announce != null)
            {
                if (Announce(announcement))
                {
                    Console.WriteLine("Publisher sez: Private message received by a subscriber!");
                }
            }
        }
    }
}
