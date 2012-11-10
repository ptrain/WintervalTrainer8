using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.Media;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace HelloWorld
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : HelloWorld.Common.LayoutAwarePage
    {
        private int numWrong = 0;
        private int numRight = 0;

        /// <summary>
        /// This variable is used to determine if we are waiting for our sound events to finish.
        ///   If we are, this is true and all other events are ignored. If not, they are allowed.
        /// </summary>
        private bool holdForSound = false;

        //private Interval lastInt = null;

        List<Interval> intervals = new List<Interval>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            ansWrongNum.Text = numWrong.ToString();
            ansRightNum.Text = numRight.ToString();
            this.populateIntervals();
            //this
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            greetingOutput.Text = "Hello, " + nameInput.Text + "!";
        }*/

        private void Play_Scale(object sender, RoutedEventArgs e)
        {
            // See if we can play a scale
            if (holdForSound)
            {
                return;
            }
            else
            {
                holdForSound = true;
            }
            // First play a sound

            // Now set the interval that was for later
            //lastInt = 
            
            
            //return null;
            //ansWrongNum.Text = (theIntervals.SelectedItem as Interval).Name;








            //////////THIS LOGIC HAS TO BE CHANGED

            ///// CHANGE THIS FUCKING LOGIC

            ///// THIS IS IN HERE SO WE DON'T FUCK UP LATER, BUT IF WE LEAVE IT WE WILL
            holdForSound = false;
            /////those are some angry FUCKING COMMENTS!!!!!!!!!!!!!! LISTEN TO THEM!!!!!!!!!!!!
            //REMOVE THIS WHEN WE HAVE CODE THAT CLEANS UP AFTER 
        }

        private void populateIntervals()
        {
            intervals.Add(new Interval() { Name = "m2" });
            intervals.Add(new Interval() { Name = "M2" });
            intervals.Add(new Interval() { Name = "m3" });
            intervals.Add(new Interval() { Name = "M3" });
            intervals.Add(new Interval() { Name = "P4" });
            intervals.Add(new Interval() { Name = "P5" });
            intervals.Add(new Interval() { Name = "m6" });
            intervals.Add(new Interval() { Name = "M6" });
            intervals.Add(new Interval() { Name = "m7" });
            intervals.Add(new Interval() { Name = "M7"});
            intervals.Add(new Interval() { Name = "P8"});

            var res = from i in intervals group i by i.Name into grp orderby grp.Key select grp;
            cvsInts.Source = res;
        }

        private void Submit_Answer(object sender, RoutedEventArgs e)
        {
            //submit answer and return true or false
            var ans = (theIntervals.SelectedItem as Interval);

            //play a sound
            firstNotePlayer.Play();
            //return null;
        }


        private void firstNotePlayer_PlaySecondNote(object sender, RoutedEventArgs e)
        {
            secondNotePlayer.Play();
        }
    }

    public class Interval
    {
        public string Name { get;  set; }
        //public string File { get; set; }

    }
}
