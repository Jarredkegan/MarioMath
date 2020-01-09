using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Interaction logic for wndFinalScore.xaml
    /// </summary>
    public partial class wndFinalScore : Window
    {

        #region ImageBrush images
        /// <summary>
        /// Image represents a low score
        /// </summary>
        ImageBrush lowScore = new ImageBrush(new BitmapImage(new Uri(@"images/marioLuigiLowScore.jpg", UriKind.Relative)));

        /// <summary>
        /// Image represents an average score
        /// </summary>
        ImageBrush averageScore = new ImageBrush(new BitmapImage(new Uri(@"images/marioLuigiAverageScore.jpg", UriKind.Relative)));

        /// <summary>
        /// Image represents a high score
        /// </summary>
        ImageBrush highScore = new ImageBrush(new BitmapImage(new Uri(@"images/marioLuigiHighScore.jpg", UriKind.Relative)));
        #endregion

        #region Sounds
        SoundPlayer sndZoomIn = new SoundPlayer("sndZoomIn.wav");
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lblScore"></param>
        /// <param name="TimeLeft"></param>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="wrongAsnwers"></param>
        public wndFinalScore(Label lblScore, Label TimeLeft, string name, int age, int wrongAsnwers)
        {
            InitializeComponent();
            //gets score from game window
            lblFinalScore.Content = lblScore.Content;

            //gets number of incorrect answers
            lblIncorrectAnswers.Content = wrongAsnwers;

            //gets users name
            lblName.Content = name;

            //gets users age
            lblAge.Content = age;

            //gets the remaining time left
            lblTimeLeft.Content = TimeLeft.Content;

            //button not used atm
            cmdHighScores.IsEnabled = false;

            //sets an images that corrisponds to users score. low, average, high
            if (Convert.ToInt32(lblFinalScore.Content) >= 8)
            {
                imgFinalScore.Source = new BitmapImage(new Uri("images/marioLuigiHighScore.png", UriKind.RelativeOrAbsolute));
            }
            else if (Convert.ToInt32(lblFinalScore.Content) >= 5)
            {
                imgFinalScore.Source = new BitmapImage(new Uri("images/marioLuigiAverageScore.png", UriKind.RelativeOrAbsolute));
            }
            else if (Convert.ToInt32(lblFinalScore.Content) < 5)
            {
                imgFinalScore.Source = new BitmapImage(new Uri("images/marioLuigiLowScore.png", UriKind.RelativeOrAbsolute));
            }
        }
        #endregion

        #region UI Events
        /// <summary>
        /// button to end game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEndGame_Click(object sender, RoutedEventArgs e)
        {
            sndZoomIn.Play();
            this.Hide();
        }

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
        #endregion
    }
}
