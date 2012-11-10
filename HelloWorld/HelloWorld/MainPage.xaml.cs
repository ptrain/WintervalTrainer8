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

        private Interval currAns;
        private Note n1;
        private Note n2;

        /// <summary>
        /// This variable is used to determine if we are waiting for our sound events to finish.
        ///   If we are, this is true and all other events are ignored. If not, they are allowed.
        /// </summary>
        private bool holdForSound = false;

        // Class variable to hold the Wrong Text Style
        private Style WrongStyle;

        // Another one for correct text style
        private Style CorrectStyle;

        //private Interval lastInt = null;

        public List<Interval> intervals = new List<Interval>();
        Dictionary<int, Interval> intervalsDict = new Dictionary<int,Interval>();

        List<Note> notes = new List<Note>();

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
            this.populateNotes();
            WrongStyle = Application.Current.Resources["WrongTextStyle"] as Style;
            CorrectStyle = Application.Current.Resources["CorrectTextStyle"] as Style;

            // Pick new notes, set global vars
            setupTry();
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

        private void setupTry()
        {
            Random rand = new Random();
            int n1Index = rand.Next(0, 12);
            int n2Index = rand.Next(0, 12);

            // Pick random note 1
            n1Index = rand.Next(0, 12);
            n1 = notes[n1Index];
            //firstNotePlayer.Source = new Uri(@"C:\Code\Active\WintervalTrainer8\Helloworld\Helloworld" + n1.getFilePath(), UriKind.RelativeOrAbsolute);
            Uri u = new Uri("ms-resource:/Files/sounds/" + n1.File, UriKind.Absolute);
            firstNotePlayer.Source = u;
            // Pick random note 2
            while(n1Index == n2Index){
                // pick a new index
                n2Index = rand.Next(0, 12);
            }

            n2 = notes[n2Index];
            //secondNotePlayer.Source = new Uri(@"C:\Code\Active\WintervalTrainer8\Helloworld\Helloworld" + n2.getFilePath(), UriKind.RelativeOrAbsolute);
            secondNotePlayer.Source = new Uri("ms-resource:/Files/sounds/" + n2.File, UriKind.Absolute);

            // Calclulate interval
            currAns = calculateInterval(n1, n2);
        }

        private void Play_Scale(object sender, RoutedEventArgs e)
        {
            // clear last guess' metadata
            note1.Text = "";
            note2.Text = "";
            theAnswer.Text = "";
            yourGuess.Text = "";
            // This simply plays the currently selected notes. These notes are globals somewhere
            
            // Plays the first sound, then an event handler
            // attached to firstNotePlayer plays the second note
            firstNotePlayer.Play();


            // See if we can play a scale
            if (holdForSound)
            {
                return;
            }
            else
            {
                holdForSound = true;
            }
            
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
            intervals.Add(new Interval() { Name = "m2", NumHalfSteps = 1, Hint = "Jaws..." });
            intervals.Add(new Interval() { Name = "M2", NumHalfSteps = 2, Hint = "The Beatles - Yesterday" });
            intervals.Add(new Interval() { Name = "m3", NumHalfSteps = 3, Hint = "Sad sound" });
            intervals.Add(new Interval() { Name = "M3", NumHalfSteps = 4, Hint = "Doorbell"});
            intervals.Add(new Interval() { Name = "P4", NumHalfSteps = 5, Hint = "Amazing Grace"});
            intervals.Add(new Interval() { Name = "A4/D5", NumHalfSteps = 6, Hint = "Diabolus in Musica" });
            intervals.Add(new Interval() { Name = "P5", NumHalfSteps = 7, Hint = "Star wars!"});
            intervals.Add(new Interval() { Name = "m6", NumHalfSteps = 8, Hint = "Love Story (Where Do I Begin?)" });
            intervals.Add(new Interval() { Name = "M6", NumHalfSteps = 9, Hint = "NBC" });
            intervals.Add(new Interval() { Name = "m7", NumHalfSteps = 10, Hint = "West Side Story"});
            intervals.Add(new Interval() { Name = "M7", NumHalfSteps = 11, Hint = "Pure Imagination" });
            intervals.Add(new Interval() { Name = "P8", NumHalfSteps = 12, Hint = "Sounds like the same note..."});

            var res = from i in intervals group i by i.Name into grp orderby grp.Key select grp;
            cvsInts.Source = res;
        }

        private void populateNotes()
        {
            notes.Add(new Note() { Name = "C6", Num = 1, File="c6.wav" });
            notes.Add(new Note() { Name = "C#6", Num = 2, File = "cs6.wav" });
            notes.Add(new Note() { Name = "D6", Num = 3, File = "d6.wav" });
            notes.Add(new Note() { Name = "D#6", Num = 4, File = "ds6.wav" });
            notes.Add(new Note() { Name = "E6", Num = 5, File = "e6.wav" });
            notes.Add(new Note() { Name = "F6", Num = 6, File = "f6.wav" });
            notes.Add(new Note() { Name = "F#6", Num = 7, File = "fs6.wav" });
            notes.Add(new Note() { Name = "G6", Num = 8, File = "g6.wav" });
            notes.Add(new Note() { Name = "G#6", Num = 9, File = "gs6.wav" });
            notes.Add(new Note() { Name = "A6", Num = 10, File = "a6.wav" });
            notes.Add(new Note() { Name = "A#6", Num = 11, File = "as6.wav" });
            notes.Add(new Note() { Name = "B6", Num = 12, File = "b6.wav" });
            notes.Add(new Note() { Name = "C7", Num = 13, File = "c7.wav" });

        }

        private void Submit_Answer(object sender, RoutedEventArgs e)
        {
            //submit answer and return true or false
            var ans = (theIntervals.SelectedItem as Interval);

            // Check if right or wrong and take appropriate action
            if (ans.Name == currAns.Name){
                // do whatever happens when it's right
                numRight++;
                ansWrongNum.Text = numWrong.ToString();
                ansRightNum.Text = numRight.ToString();
                yourGuessTitle.Style = CorrectStyle;
                yourGuess.Style = CorrectStyle;
            }
            else {
                // do whatever happens when it's wrong
                numWrong++;
                ansWrongNum.Text = numWrong.ToString();
                ansRightNum.Text = numRight.ToString();
                yourGuess.Style = WrongStyle;
                yourGuessTitle.Style = WrongStyle;
            }

            // display right info
            note1.Text = n1.Name;
            note2.Text = n2.Name;
            theAnswer.Text = currAns.Name;
            yourGuess.Text = ans.Name;

            // Choose new notes for next try, set up correct answer interval (which is global I guess?)
            setupTry();
        }


        private void firstNotePlayer_PlaySecondNote(object sender, RoutedEventArgs e)
        {
            secondNotePlayer.Play();
        }
        
        private void failedSound(object sender, RoutedEventArgs e)
        {
            failSound.Play();
        }
        
        public List<Interval> getIntervals()
        {
            return intervals;
        }

    
        // Given two notes, returns the Interval object representing the
        // distance between them

        public Interval calculateInterval(Note n1, Note n2)
        {

            int index;

            // n2 will have been played after n1

            if (n2.Num > n1.Num)
            {
                index = n2.Num - n1.Num - 1;
            }
            else
            {
                index = Math.Abs(n2.Num - n1.Num) - 1;
            }


            
            return intervals[index];
        }
    }





        public class Interval
        {
            public string Name { get; set; }
            public int NumHalfSteps { get; set; }
            public string Hint { get; set; }
            
        }

        public class Note
        {
            public string Name { get; set; }
            public int Num { get; set; }
            public string File { get; set; }

            public string getFilePath()
            {
                return "/sounds/" + this.File;
            }
        }

}
