/*  Puzzle.cs
 *  Assignment 3
 *  Revision History:
 *      soochang: 2016. 10. 16 Created
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SKimAssignment03
{
    /// <summary>
    /// Puzzle class
    /// </summary>
    class Puzzle : Button
    {
        /// <summary>
        /// Fields
        /// </summary>
        #region Fields
        /// <summary>
        /// Direction to move
        /// </summary>
        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
        /// <summary>
        /// Const values
        /// </summary>
        public const int PUZZLESIZE = 40;

        /// <summary>
        /// Variables of class
        /// </summary>
        private int _idxRow;
        private int _idxCol;
        #endregion

        /// <summary>
        /// Properties to abstract fields
        /// </summary>
        #region Properties
        public int IdxRow
        {
            get
            {
                return _idxRow;
            }

            set
            {
                _idxRow = value;
            }
        }

        public int IdxCol
        {
            get
            {
                return _idxCol;
            }

            set
            {
                _idxCol = value;
            }
        }
        #endregion


        /// <summary>
        /// Puzzle class constructor
        /// </summary>
        /// <param name="np">NPuzzleForm class where puzzle will be added</param>
        /// <param name="top">Top position of button</param>
        /// <param name="left">Left position of button</param>
        /// <param name="msg">Message to show within button</param>
        /// <param name="idxRow">Row index of Puzzle array </param>
        /// <param name="idxCol">Col index of Puzzle array</param>
        public Puzzle(NPuzzleForm np, int top, int left, string msg, int idxRow, int idxCol)
        {
            this.Top = top;
            this.Left = left;
            this.Text = msg;
            this.Width = PUZZLESIZE;
            this.Height = PUZZLESIZE;
            this.IdxRow = idxRow;
            this.IdxCol = idxCol;
            this.Click += np.btnNumber_Click;
            np.Controls.Add(this);
        }
        

        /// <summary>
        /// Move puzzle to given direction
        /// </summary>
        /// <param name="moveTimes">How many times to reach destination</param>
        /// <param name="direction">Direction to move</param>
        public void moveTo(int moveTimes, Direction direction)
        {
            int distance = PUZZLESIZE / moveTimes;
            switch (direction)
            {
                case Direction.Up:
                    this.Top -= distance;
                    break;
                case Direction.Right:
                    this.Left += distance;
                    break;
                case Direction.Down:
                    this.Top += distance;
                    break;
                case Direction.Left:
                    this.Left -= distance;
                    break;
                default:
                    break;
            }
        }
    }
}
