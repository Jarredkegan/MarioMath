using System.Media;
using System.Windows;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Interaction logic for wndMathMenu.xaml
    /// </summary>
    public partial class wndMathMenu : Window
    {

        #region Class references
        /// <summary>
        /// Class that holds the user data.
        /// </summary>
        wndEnterUserData wndEnterUserDataForm;
        #endregion

        #region Sounds
        /// <summary>
        /// Theme song
        /// </summary>
        //SoundPlayer sndMarioThemeSong = new SoundPlayer("sndMarioThemeSong.wav");

        /// <summary>
        /// Button mouseover sound
        /// </summary>
        SoundPlayer sndBlupip = new SoundPlayer("sndKick.wav");

        /// <summary>
        /// Change window sound
        /// </summary>
        SoundPlayer sndZoomIn = new SoundPlayer("sndZoomIn.wav");
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public wndMathMenu()
        {
            InitializeComponent();

            //MAKE SURE TO INCLUDE THIS LINE OR THE APPLICATION WILL NOT CLOSE
            //BECAUSE THE WINDOWS ARE STILL IN MEMORY
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            //wndHighScoresForm = new wndHighScores();
            wndEnterUserDataForm = new wndEnterUserData();

            //Pass the high scores form to the game form.  This way the high scores form may be displayed via the game form.
            //wndGameForm.CopyHighScores = wndHighScoresForm;

            cmdHighScores.IsEnabled = false;

            //sndMarioThemeSong.Play();
        }
        #endregion

        #region UI Events

        /// <summary>
        /// Button to open wndEnterUserData for getting users information and game type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEnterUserData_Click(object sender, RoutedEventArgs e)
        {
            sndZoomIn.Play();

            //Hide the menu
            this.Hide();

            //window must be hidden to ShowDialog()?
            wndEnterUserDataForm.Hide();

            //Show the user data form
            wndEnterUserDataForm.ShowDialog();

            //Show the main form
            this.Show();
        }

        /// <summary>
        /// Mouse over sound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdPlayGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sndBlupip.Play();
        }

        /// <summary>
        /// Mouse over sound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdHighScores_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sndBlupip.Play();
        }

        /// <summary>
        /// Mouse over sound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdEnterUserData_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sndBlupip.Play();
        }

        /// <summary>
        /// Quit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}
