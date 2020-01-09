using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Game class holds math logic
    /// </summary>
    public class Game
    {
        #region Attributes
        /// <summary>
        /// User's tally of correct answers
        /// </summary>
        public int score;

        /// <summary>
        /// Top number used for division only
        /// top number * result = bottom number
        /// </summary>
        public int topNumber;

        /// <summary>
        /// Bottom number used for division only
        /// top number * result = bottom number
        /// </summary>
        public int bottomNumber;

        /// <summary>
        /// result used for division only
        /// top number * result = bottom number
        /// </summary>
        public int result;
        #endregion

        #region Random object
        /// <summary>
        /// New instance of random class creates a random object to get our randomized numbers
        /// </summary>
        Random random = new Random();
        #endregion

        #region Methods
        /// <summary>
        /// Method to get random numbers between 1 - 10
        /// </summary>
        /// <returns></returns>
        public int GenerateNumber()
        {
            return random.Next(1, 10);
        }

        /// <summary>
        /// Method used for generating random numbers for division only
        /// Generate numbers that divide into whole numbers only
        /// top number * result = bottom number
        /// </summary>
        public void GenerateDivisionNumbers()
        {
            topNumber = 1 + random.Next(1, 10) % 5 * 2;
            result = 1 + random.Next(1, 10) % 5 * 2;
        }

        /// <summary>
        /// Method for finding out bottom number for Division only
        /// top number * result = bottom number
        /// </summary>
        /// <param name="top"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public int GetBottomNumber(int top, int result)
        {
            bottomNumber = top * result;
            return bottomNumber;
        }

        /// <summary>
        /// Method for adding the numbers
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public int AddNumbers(int top, int bottom)
        {
            return top + bottom;
        }

        /// <summary>
        /// method for subtracting the numbers
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public int SubtractNumbers(int top, int bottom)
        {
            return top - bottom;
        }

        /// <summary>
        /// Method for multiplying the numbers
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public int MultiplyNumbers(int top, int bottom)
        {
            return top * bottom;
        }
        #endregion
    }
}
