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

        public delegate string QuestionAndAnswer(String question);
        public event QuestionAndAnswer Ask;

        internal void AskTheQuestion(string question)
        {
            Console.WriteLine("\n\n\nPublisher sez: Asking \"{0}\":", question);

            if (Ask != null)
            {
                List<string> answers = new List<string>();

                foreach (Delegate answerer in Ask.GetInvocationList())
                {
                    answers.Add((string)answerer.DynamicInvoke(question));
                }
                Console.WriteLine("Publisher sez: Got {0} answers.", answers.Count());
                Console.WriteLine("Publisher sez: " + string.Join(", ", answers));
            }
        }

        internal void SpreadTheWord(string announcement)
        {
            Console.WriteLine("\n\n\nPublisher sez: Announcing \"{0}\":", announcement);

            if (Announce != null)
            {
                // If I have only one delegate, I will see its return value from the call to Announce().
                // If I have more than one delegate, I will see only the return value of the LAST delegate
                // from the call to Announce.

                foreach (Delegate listener in Announce.GetInvocationList()) {
                    if ((bool)listener.DynamicInvoke(announcement))
                    {
                        Console.WriteLine("Publisher sez: Private message received by a subscriber!  Not calling any more!");
                        break;
                    }
                }

//                if (Announce(announcement))
//                {
//                    Console.WriteLine("Publisher sez: Private message received by a subscriber!");
//                }
            }
        }
    }
}
