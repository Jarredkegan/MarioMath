using System;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Interaction logic for wndGame.xaml
    /// </summary>
    public partial class wndGame : Window
    {

        #region Attributes

        /// <summary>
        /// decrement is a timer that starts at 30 and decrements every tick
        /// </summary>
        int decrement = 30;

        /// <summary>
        /// helps get users name from user data window
        /// </summary>
        public string userName;

        /// <summary>
        /// helps get users age from user data window
        /// </summary>
        public int userAge;

        /// <summary>
        /// counts the incorrect answers submitted
        /// </summary>
        int wrongAnswers = 0;

        #endregion

        #region Sounds
        SoundPlayer sndKick = new SoundPlayer("sndKick.wav");
        SoundPlayer sndHereWeGo = new SoundPlayer("sndHereWeGo.wav");
        SoundPlayer sndBump = new SoundPlayer("sndBump.wav");
        SoundPlayer snd1up = new SoundPlayer("snd1up.wav");
        SoundPlayer sndZoomIn = new SoundPlayer("sndZoomIn.wav");
        SoundPlayer sndTick = new SoundPlayer("sndTick.wav");

        //MediaPlayer mp = new MediaPlayer();

        #endregion

        #region Class References

        /// <summary>
        /// References the DispatcherTimer class
        /// </summary>
        DispatcherTimer MyTimer;

        /// <summary>
        /// References Game class
        /// </summary>
        Game clsGame;

        /// <summary>
        /// References ValidateInput class
        /// </summary>
        ValidateInput clsValidateInput;

        /// <summary>
        /// References final score window
        /// </summary>
        wndFinalScore FinalScore;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        public wndGame(string name, int age)
        {
            InitializeComponent();

            //sets users name
            userName = name;

            //sets users age
            userAge = age;

            //start with start timer button enabled
            cmdStartTimer.IsEnabled = true;

            //start with submit answer button disabled until user presses start timer
            cmdSubmitAnswer.IsEnabled = false;

            //label prompting user that they must start timer to begin
            lblPressStartTimerToPlay.Content = "Press 'Start Timer' to play";

            //new instance of DispatcherTimer
            MyTimer = new DispatcherTimer();

            //set timer intervals to 1 second
            MyTimer.Interval = TimeSpan.FromSeconds(1);

            //add and set tick to MyTimer_Tick
            MyTimer.Tick += new EventHandler(MyTimer_Tick);

            //Create new game
            clsGame = new Game();

            //create new instance of validate input class
            clsValidateInput = new ValidateInput();

        }

        #endregion

        #region UI Events

        /// <summary>
        /// Button event to end current round and display final score the that round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEndGame_Click(object sender, RoutedEventArgs e)
        {
            //Hide the menu
            this.Hide();

            //end round, show score
            EndRound();
        }

        /// <summary>
        /// High Schore window, not in use atm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHighScores_Click(object sender, RoutedEventArgs e)
        {
            //Hide the game form
            this.Hide();
            //Show the high scores
            //wndCopyHighScores.ShowDialog();
        }

        /// <summary>
        /// Closes current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// Starts the timer and begins the game!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdStartTimer_Click(object sender, RoutedEventArgs e)
        {
            //gets rid of red prompt text
            lblPressStartTimerToPlay.Content = "";

            //gets 2 random numbers
            GenerateNumbers();

            //disable start timer button
            cmdStartTimer.IsEnabled = false;

            //enable submit answers button
            cmdSubmitAnswer.IsEnabled = true;

            //start the timer
            MyTimer.Start();

            //play here we go sound
            sndHereWeGo.Play();

            //focus the answer textbox for immediate answer action
            txtboxUserAnswer.Focus();

        }

        /// <summary>
        /// Mouse over button sound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdStartTimer_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sndKick.Play();
        }

        /// <summary>
        /// Mouse over button sound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdEndGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sndKick.Play();
        }

        /// <summary>
        /// When the 'Enter' key is pressed - Checks users answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtboxUserAnswer_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                DetermineSolutioinType();
            }
            //answer has been checked
            e.Handled = true;
        }

        /// <summary>
        /// When the 'Submit' button is pressed - Checks users answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdSubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            DetermineSolutioinType();
            txtboxUserAnswer.Focus();
        }

        /// <summary>
        /// Makes sure that the user is only able to input numbers into answer textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtboxUserAnswer_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            clsValidateInput.isNumber(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method for determining what happens each timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MyTimer_Tick(object sender, EventArgs e)
        {
            //decrement by 1 each tick
            decrement--;

            //display that decrement
            lblTimer.Content = decrement.ToString();

            //make a tick sound every tick
            sndTick.Play();

            //when decrement equals 10 play running out of time sound
            //if (decrement == 10)
            //{
            //    sndRunningOutOfTime.Play();
            //}

            //when decrement equals 0 - times up, end round
            if (decrement == 0)
            {
                EndRound();
            }

        }

        /// <summary>
        /// Method checks what game type it is, passes numbers to Game class to do the math, and if it's a win or loss - set ui labels accordingly
        /// Method kinda has a bad name
        /// </summary>
        private void DetermineSolutioinType()
        {
            //add
            if (Convert.ToString(lblOperation.Content) == "+")
            {
                if (String.IsNullOrWhiteSpace(txtboxUserAnswer.Text))
                {
                    IsLoss();
                }
                else if (Convert.ToInt32(txtboxUserAnswer.Text) == clsGame.AddNumbers(Convert.ToInt32(lblTopNumber.Content), Convert.ToInt32(lblBottomNumber.Content)))
                {
                    IsWin();
                }
                else
                {
                    IsLoss();
                }
            }
            //subtract
            else if (Convert.ToString(lblOperation.Content) == "-")
            {
                if (String.IsNullOrWhiteSpace(txtboxUserAnswer.Text))
                {
                    IsLoss();
                }
                else if(Convert.ToInt32(txtboxUserAnswer.Text) == clsGame.SubtractNumbers(Convert.ToInt32(lblTopNumber.Content), Convert.ToInt32(lblBottomNumber.Content)))
                {
                    IsWin();
                }
                else
                {
                    IsLoss();
                }
            }
            //multiply
            else if (Convert.ToString(lblOperation.Content) == "x")
            {
                if (String.IsNullOrWhiteSpace(txtboxUserAnswer.Text))
                {
                    IsLoss();
                }
                else if(Convert.ToInt32(txtboxUserAnswer.Text) == clsGame.MultiplyNumbers(Convert.ToInt32(lblTopNumber.Content), Convert.ToInt32(lblBottomNumber.Content)))
                {
                    IsWin();
                }
                else
                {
                    IsLoss();
                }
            }
            //divide
            else if (Convert.ToString(lblOperation.Content) == "/")
            {
                if (String.IsNullOrWhiteSpace(txtboxUserAnswer.Text))
                {
                    IsLoss();
                }
                else if(Convert.ToInt32(txtboxUserAnswer.Text) == clsGame.result)
                {
                    IsWin();
                }
                else
                {
                    IsLoss();
                }
            }
        }

        /// <summary>
        /// Adjusts UI labels to corrispond with a incorrect answer
        /// </summary>
        private void IsLoss()
        {
            //sound for wrong answer
            sndBump.Play();

            //increment wrong answer
            wrongAnswers++;

            //display wrong answer value
            lblScoreIncorrect.Content = wrongAnswers;

            //give result message red text
            lblResult.Foreground = Brushes.Red;

            //show how sympathetic you are for the users wrong answer
            lblResult.Content = "Sorry, Incorrect";

            //wrong answer reset to blank
            txtboxUserAnswer.Text = "";


            GenerateNumbers();
        }

        /// <summary>
        /// Adjusts UI labels to corrispond with a correct answer
        /// </summary>
        private void IsWin()
        {
            //sound for a WIN
            snd1up.Play();

            //give user a point
            clsGame.score++;

            //update score label
            lblScore.Content = clsGame.score;

            //give label green text for a win
            lblResult.Foreground = Brushes.Green;

            //give user praise for a correct answer
            lblResult.Content = "Yes, Correct!";

            //if the user has 10 correct answers - end round - horray for getting 10 answers correct
            if (Convert.ToInt32(lblScore.Content) == 10)
            {
                EndRound();
            }
            //should this be an else right here? wouldn't hurt, i think it would be beneficial actually

            //reset to blank
            txtboxUserAnswer.Text = "";

            //generate 2 more numbers
            GenerateNumbers();
        }

        /// <summary>
        /// Method generates 2 random numbers - top and bottom
        /// </summary>
        private void GenerateNumbers()
        {

            if (Convert.ToString(lblOperation.Content) == "-")
            {
                lblTopNumber.Content = clsGame.GenerateNumber();
                lblBottomNumber.Content = clsGame.GenerateNumber();

                //checks to see if the bottom number is larger than the top, if so swap them.
                SwapNumbers();
            }
            else if (Convert.ToString(lblOperation.Content) == "/")
            {
                //if division is selected we need to genereate the numbers differently with GenerateDivisionNumbers(); method contained in Game class
                clsGame.GenerateDivisionNumbers();

                lblTopNumber.Content = clsGame.topNumber;
                lblBottomNumber.Content = clsGame.GetBottomNumber(clsGame.topNumber, clsGame.result);

                //checks to see if the bottom number is larger than the top, if so swap them.
                SwapNumbers();
            }
            else
            //subtraction nor division was selected, generate numbers normally - nothing special needed
            {
                lblTopNumber.Content = clsGame.GenerateNumber();
                lblBottomNumber.Content = clsGame.GenerateNumber();
            }
        }

        /// <summary>
        /// Checks to see if the bottom number is larger than the top, if so swap them.
        /// </summary>
        private void SwapNumbers()
        {
            if (Convert.ToInt32(lblTopNumber.Content) < Convert.ToInt32(lblBottomNumber.Content))
            {
                var temp = lblTopNumber.Content;
                lblTopNumber.Content = lblBottomNumber.Content;
                lblBottomNumber.Content = temp;
            }
        }

        /// <summary>
        /// Ends the round and shows a final score for that round
        /// </summary>
        private void EndRound()
        {
            MyTimer.Stop();

            //play "ZoomIn" mario sound
            sndZoomIn.Play();

            //Hide the menu
            this.Hide();


            FinalScore = new wndFinalScore(lblScore, lblTimer, userName, userAge, wrongAnswers);

            //Show the Game form
            FinalScore.ShowDialog();
        }

        #endregion
    }
}
