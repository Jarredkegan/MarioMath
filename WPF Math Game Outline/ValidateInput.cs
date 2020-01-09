using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Validate Input class holds logic to validate user's input specifically their age
    /// </summary>
    public class ValidateInput
    {
        #region Methods
        /// <summary>
        /// Makes sure age entered is a number only
        /// </summary>
        /// <param name="e"></param>
        public void isNumber(KeyEventArgs e)
        {
            //Only allow numbers to be entered
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                  e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                //Allow the user to use the backspace and delete keys
                if (!(e.Key == Key.Back || e.Key == Key.Delete))
                {
                    //No other keys allowed besides numbers, backspace, and delete
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Makes sure age entered is within ages 3 and 10
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public bool isWithinAgeRange(string age)
        {

            if (int.Parse(age) >= 3 && int.Parse(age) <= 10)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        #endregion
    }
}
