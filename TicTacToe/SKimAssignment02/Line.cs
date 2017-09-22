/* Line.cs
 * Assignment 02
 *  Revision History:
 *      soochan: 2016. 09. 18 created
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKimAssignment02
{
    class Line
    {
        /// <summary>
        /// All field of Line class
        /// </summary>
        #region Fields
        private bool _isMarked;
        private int _numberOfCheckedPoint;
        private int _score; 
        #endregion

        /// <summary>
        /// Getter and setter for properties
        /// </summary>
        #region Properties

        public bool IsMarked
        {
            get
            {
                return _isMarked;
            }

            set
            {
                _isMarked = value;
            }
        }

        public int NumberOfCheckedPoint
        {
            get
            {
                return _numberOfCheckedPoint;
            }

            set
            {
                _numberOfCheckedPoint = value;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }

            set
            {
                _score = value;
            }
        }
        #endregion

        /// <summary>
        /// Line class constructor
        /// </summary>
        public Line()
        {
            this._isMarked = false;
            this._numberOfCheckedPoint = 0;
            this._score = 0;
        }

        /// <summary>
        /// To get absolute value
        /// </summary>
        /// <param name="val">Value to be converted</param>
        /// <returns>Absolute value of given input</returns>
        private int abs(int val)
        {
            return val > 0 ? val : -val;
        }

        /// <summary>
        /// To check if there is winner
        /// </summary>
        /// <returns>True when all line is marked by same user</returns>
        public bool isWin()
        {
            return abs(Score) == 3;
        }
        
        /// <summary>
        /// Check if the line is not winnable
        /// </summary>
        /// <returns>True when the line is not marked, but need to be marked</returns>
        public bool isNeedToMark()
        {
            if (!IsMarked)
            {
                return NumberOfCheckedPoint >= 2 && abs(Score) != NumberOfCheckedPoint;
            }
            return false;
        }
    }
}
