using System;
using System.Media;
using System.Windows;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Interaction logic for wndEnterUserData.xaml
    /// </summary>
    public partial class wndEnterUserData : Window
    {

        #region Class References
        /// <summary>
        /// Class where the game is played.
        /// </summary>
        wndGame wndGameForm;

        /// <summary>
        /// Class to validate users input
        /// </summary>
        ValidateInput ValidateInput;

        /// <summary>
        /// Class holds users info
        /// </summary>
        User clsUser;
        #endregion

        #region Sounds

        SoundPlayer sndMarioThemeSong = new SoundPlayer("sndMarioThemeSong.wav");
        SoundPlayer sndKick = new SoundPlayer("sndKick.wav");
        SoundPlayer sndBump = new SoundPlayer("sndBump.wav");
        SoundPlayer sndZoomIn = new SoundPlayer("sndZoomIn.wav");

        #endregion

        #region Constructor

        public wndEnterUserData()
        {
            InitializeComponent();
            //Creates new instance of User class
            clsUser = new User();

            //Creates new instance of ValidateInput class
            ValidateInput = new ValidateInput();

            //Start window with everything blank
            txtboxAge.Text = "";
            txtboxName.Text = "";
            radioSubtract.IsChecked = false;
            radioMultiply.IsChecked = false;
            radioDivide.IsChecked = false;
            radioAdd.IsChecked = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check what game type was picked and display it on the UI
        /// </summary>
        private void CheckOperation()
        {
            if (radioAdd.IsChecked == true)
            {
                wndGameForm.lblOperation.Content = "+";
            }
            else if (radioSubtract.IsChecked == true)
            {
                wndGameForm.lblOperation.Content = "-";
            }
            else if (radioMultiply.IsChecked == true)
            {
                wndGameForm.lblOperation.Content = "x";
            }
            else if (radioDivide.IsChecked == true)
            {
                wndGameForm.lblOperation.Content = "/";
            }
        }

        #endregion

        #region UI Events

        /// <summary>
        /// Closes window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// Validates users input and then sets attributes accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlayGame_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtboxName.Text))
            {
                sndBump.Play();
                lblUserDataError.Content = "Please enter your name";
            }
            else if (String.IsNullOrWhiteSpace(txtboxAge.Text))
            {
                sndBump.Play();
                lblUserDataError.Content = "Please enter your age";
            }
            else if (!ValidateInput.isWithinAgeRange(txtboxAge.Text))
            {
                sndBump.Play();
                lblUserDataError.Content = "Please enter an age between 3 - 10";
            }
            else if (radioAdd.IsChecked == false && radioSubtract.IsChecked == false && radioMultiply.IsChecked == false && radioDivide.IsChecked == false)
            {
                sndBump.Play();
                lblUserDataError.Content = "Please select an operation (+ , - , x , or /)";
            }
            //everything is good
            else
            {
                //sets users age
                clsUser.age = Convert.ToInt32(txtboxAge.Text);

                //sets users name
                clsUser.name = Convert.ToString(txtboxName.Text);

                //creates new game with users name and age
                wndGameForm = new wndGame(clsUser.name, clsUser.age);

                //sets the game type
                CheckOperation();

                //play "ZoomIn" mario sound
                sndZoomIn.Play();

                //Hide the menu
                this.Hide();

                //Hide the Game form so we can ShowDialog(); ??
                wndGameForm.Hide();

                //Show the Game form
                wndGameForm.ShowDialog();
            }
        }

        /// <summary>
        /// Makes sure the textbox for users age only accepts numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtboxAge_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ValidateInput.isNumber(e);
        }

        /// <summary>
        /// Mouse over button sound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlayGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sndKick.Play();
        }

        #endregion
    }
}
