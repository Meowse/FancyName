using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BasicDelegates
{
    public partial class MickeyForm : Form
    {
        private delegate void TellMyselfAboutChanges(MickeyForm source, String message);

        private event TellMyselfAboutChanges ChChChChanges;

        // Mickey to research:
        // 1) Debugging into .NET Framework source
        // 2) Displaying Debug.WriteLine messages in some Visual Studio view

        public MickeyForm()
        {
//            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
//            Debug.AutoFlush = true;
            InitializeComponent();

//            this.doSomethingButton.Click += new System.EventHandler(this.doSomethingElse);

            doSomethingButton.Click += doSomething;

            // These lines, together, would leave the behavior unchanged.  The first would add another instance of the
            // event listener, but the second would remove that added instance, leaving whatever listeners had already been
            // there unchanged.
//            doSomethingButton.Click += doSomething;
//            doSomethingButton.Click -= doSomething;

            // Observation: If we add the same listener multiple times, it is called multiple times when the event is fired


            // Assumption: listeners will be called in the order they were added
            // (but if it is something else, it can't be "alphabetical", because our experiment below disproved that it was alphabetical)

//            ChChChChanges += announceThatIHaveChanged;
//            ChChChChanges += logThatIHaveChanged;


            // Setup
            //            ChChChChanges += announceThatIHaveChanged;
            //            ChChChChanges += logThatIHaveChanged;
            // Experiment
            //            ChChChChanges -= announceThatIHaveChanged;
            //            ChChChChanges += announceThatIHaveChanged;

            // Do the two "Experiment" lines leave the behavior unchanged?
            // Hypothesis 1: These two lines leave the behavior unchanged (first announce, then log)
            //          Explanation 1: -= doesn't actually remove it from the list, it just "turns it off" in the list
            //          Explanation 2: -= doesn't fully remove the knowledge of the original order, and when it's added back in, it's added where it was
            // Hypothesis 2: These two lines invert the order (first log, then announce)
            //          Explanation 1: -= actually removes it from the list.  The following += re-adds it at the end of the list, which is the normal
            //              behavior for +=
            //
            // (hypothesis 3 has been re-expressed as Explanation 1 of Hypothesis 1)
            // Hypothesis 3: The external behavior will remain unchanged, but behind the scenes it will run it twice but then negate the second run.
            //      Observable: order stays unchanged
            //      



            // Experiment:
            //            ChChChChanges += announceThatIHaveChanged;
            //            ChChChChanges += logThatIHaveChanged;
            //            ChChChChanges += announceThatIHaveChanged;
            //            ChChChChanges += logThatIHaveChanged;

            // Next test:
            // ***Hypothesis 1: When you add multiple copies of a listener, they go into separate places in the list
            //      We will see Announce, Log, Announce, Log
            // Hypothesis 2: When you add multiple copies of a listener, they go into a single entry in the list, but bump up a counter
            //      We will see Announce, Announce, Log, Log
            // What experimental test would behave DIFFERENTLY for the two hypotheses above?
            // Note: Glenn's awesome shortcut -- use IntelliSense to find the GetInvocationList() method, and look at the array of delegates it returns.
            // 
            // Add different listeners interleaved.  Hypothesis 1 -- they will be called in alternating order.  Hypothesis 2 -- the first one added
            // will be called twice, then the second one added will be called twice.


            // Experiment:
//            ChChChChanges += announceThatIHaveChanged;
//            ChChChChanges += logThatIHaveChanged;
//            ChChChChanges += announceThatIHaveChanged;
//            ChChChChanges += logThatIHaveChanged;
//            ChChChChanges -= announceThatIHaveChanged;

            // Next test: 
            // Hypothesis 1: When you remove a listener, it removes the first instance in the list (the first one added, which was the first one that will be called).
            //      Log, Announce, Log
            // **** Hypothesis 2: When you remove a listener, it removes the last instance in the list (the one added most recently, which was the last one that will be called).
            //      Announce, Log, Log
            
            // New word: "idempotent" -- means "no matter how many times you do it, it has only the effect of doing it once"
            //    In practice, means: "you don't have to worry about whether you've already done something, if you want to be sure it's done.
            //                          You can just do it again.
            // Example: "Turn the light switch on" is idempotent.
            //          "Flip the light switch" is not idempotent.

            // Something to ponder: an action that has no effects is always idempotent

            // If you need to ensure idempotence of a transaction (e.g. reloading the page doesn't double-bill the credit card), one good
            // strategy is to use a "nonce value" -- a unique (random) token generated by the server and sent down in the original page, that
            // is submitted whenever the form is submitted, which is cached on the server and used to guarantee that any particular page can
            // only be submitted once.
            //
            // Nonce values are also used to prevent replay attacks from people spying on and recording the communications in establishing a secure
            // connection.

            // If you have a computer science theory question, and Googling it hasn't given you the answer, you are welcome to
            // email Mickey about it at meowse@gmail.com.  Seriously.
            // Also, Mickey has a blog: danceswithbugs.blogspot.com
            // And he will post at least one blog entry per month for the next three months.  Promise.

            // Konrad's car: aprs.fi, search for callsign WA4OSH* and select WA4OSH-1 or WA4OSH-2 to see where either of Konrad's cars are.


            // Antepenultimate experiment: what happens if we remove a listener that has never been added?
//            ChChChChanges -= announceThatIHaveChanged;
//            ChChChChanges -= announceThatIHaveChanged;
//            ChChChChanges += announceThatIHaveChanged;
//            ChChChChanges += announceThatIHaveChanged;
//            ChChChChanges += announceThatIHaveChanged;

            // **** H1: "Fault-tolerant" -- It will ignore the subtracts that can't be done
            //      Announce, Announce, Announce
            // H2: "Enforced correctness" -- It will give an error
            //      Exception when we first subtract.
            // H3: "Math" -- It will remember how many times it has been removed
            //      Announce


            // "Ultimate" -- last or greatest
            // "Penultimate" -- just before the last or greatest
            // "Antepenultimate" -- just before the one just before the last or greatest

            // "sesquipedalian"

            // Penultimate experiment: what happens if we have no listeners?
            
            // H1: "Fault-tolerant" -- nothing happens
            //      <nothing>
            // **** H2: "Enforced correctness" -- it will give an error when the event is fired.
            //      <error when we click the button>

            // "mis-feature": a supposed "feature", built by design, that makes actual users want to pour salt in the wounds of the author(s).

            // Ultimate experiment

//            ChChChChanges -= announceThatIHaveChanged;

            // H1: the invisible event object is built on both += and -=
            //      <nothing>
            // **** H2: the invisible event object is built on += only
            //      <an exception when we first fire the event>


//            ChChChChanges += announceThatIHaveChanged;
//            ChChChChanges -= announceThatIHaveChanged;

            // H1: Once the event object is created by +=, it persists
            // ****** H2: When the last listener is removed with -=, the event object is destroyed



            // So, if you might not have any listeners -- and you ALWAYS might not -- check for null before calling
        }

        private void announceThatIHaveChanged(MickeyForm source, String message)
        {
            MessageBox.Show(message, "ANNOUNCE: I have changed!");
        }

        private void logThatIHaveChanged(MickeyForm source, String message)
        {
            MessageBox.Show(string.Format("The source {0} has changed with message {1}.", source.Name, message), "LOG: Pretend this is a log");
            //Debug.WriteLine("The source {0} has changed with message {1}.", source.Name, message);
        }

        // "method signature" (or "call signature"): the number, order, and type of parameters and return values


        // "returns void.  Takes two arguments.  The first is of type "object", and the second is o1f type "EventArgs""
        private void someOtherName(object someFoo, EventArgs ignoreThis)
        {
        }

        // This has a different method signature, so it can have the same name.
        private void doSomethingButton_Click(int alternative, string param) {
        }

        private void doSomething(object sender, EventArgs e)
        {
            if (ChChChChanges != null)
            {
                ChChChChanges(this, "Goodbye Cruel World");
            }

            // For each listener in my invisible listener list, which .net kindly manages for me behind the scenes:
            //     Call that listener with the arguments "this" and "Goodbye Cruel World"
        }

        // If I had ReSharper, from JetBrains, installed, I wouldn't have had to re-compile to see that this
        // is an error.
//        private int doSomethingButton_Click(object sender, EventArgs e)
//        {
//            return 0;
//        }


        // If you want to try to get .NET framework code visible in your debugging sessions, try the instructions at this URL:
        // http://blogs.msdn.com/b/dotnet/archive/2012/08/15/announcing-the-release-of-net-framework-4-5-rtm-product-and-source-code.aspx
    }
}
