/* SKimTicTacToe.cs
 * Assignment02
 *  Revision History:
 *      soochang: 2016. 09. 26 created
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace SKimAssignment02
{

    /// <summary>
    /// Tic Tac Toe form class which control this game
    /// </summary>
    public partial class SKimTicTacToe : Form
    {
        /// <summary>
        /// Global variables and const values to control this game
        /// </summary>
        #region Global Variables
            
        //const values
        const int ROW = 0;
        const int COL = 1;
        const int DIA = 2;
        const int LEFT = 0;
        const int RIGHT = 1;
        const int SIZE = 3;
        const int DIASIZE = 2;
        const int FIRSTPLAYER = 0;

        /// <summary>
        /// global variables for this game
        /// </summary>
        Line[][] lines = new Line[3][];
        int turn = 1;
        bool isWin = false;
        int countDrawLines = 0;

        #endregion

        /// <summary>
        /// Application constructor
        /// </summary>
        public SKimTicTacToe()
        {
            InitializeComponent();
        }


        /// <summary>
        /// When user click any picture box it will change image and check score
        /// </summary>
        /// <param name="sender">clicked picture box</param>
        /// <param name="e"></param>
        private void pbx_Click(object sender, EventArgs e)
        {

            //to convert type from object to picturebox
            PictureBox pb = (PictureBox)sender;
            
            //check if it is already clicked
            if (pb.Image == null)
            {
                //picture box tage property is used to know where the picture box is located
                int row = int.Parse(pb.Tag.ToString()) / SIZE;
                int col = int.Parse(pb.Tag.ToString()) % SIZE;
                
                drawImage(pb);
                saveRecord(row, col);

                //if there is winner or all lines is not winnable it will finish the game
                if (isWin || countDrawLines == 8)
                {
                    showWinner(isWin);
                    reInit();
                }
            }
        }

        /// <summary>
        /// to save back ground image
        /// </summary>
        /// <param name="pb">picture box to save for back-ground image</param>
        private void drawImage(PictureBox pb)
        {
            //if this turn is player 0 it will save image X, other case save O
            pb.Image = ((turn ^= 1) == 0) ? SKimAssignment02.Properties.Resources.player0 : SKimAssignment02.Properties.Resources.player1;
        }

        /// <summary>
        /// Save score and check if there is winner or the line is not winnable
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="col">col index</param>
        private void saveRecord(int row, int col)
        {
            //if user is X it will give score 1 other -1
            int temp = (turn == FIRSTPLAYER) ? 1 : -1;
            
            //to go through all possiblity
            for(int i = 0; i < 3 && !isWin; i++)
            {

                int check = -1;

                switch (i)
                {
                    case ROW:
                        check = row;
                        break;
                    case COL:
                        check = col;
                        break;
                    case DIA:
                        break;
                    default:
                        break;
                }

                if(check != -1)
                {
                    lines[i][check].Score += temp;
                    markDraw(lines[i], check);
                    isWin = lines[i][check].isWin();
                }
                else
                {
                    isWin = (dia(row, col, temp, LEFT) || dia(row, col, temp, RIGHT)) ? true : false;
                }
            }
        }

        /// <summary>
        /// Check for diagonal
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="col">col index</param>
        /// <param name="insert">score to be updated</param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool dia(int row, int col, int insert, int direction)
        {
            if (isInRange(row,col,direction))
            {
                lines[2][direction].Score += insert;;
                markDraw(lines[2], direction);
                return lines[2][direction].isWin();
            }

            return false;
        }

        /// <summary>
        /// check if the position is in diagonal
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="col">colum index</param>
        /// <param name="direction">direction of diagonal (left, right)</param>
        /// <returns>true only when it is in diagonal</returns>
        private bool isInRange(int row, int col, int direction)
        {
            return (direction == LEFT) ? (row == col) : (col == SIZE - 1 - row);
        }

        /// <summary>
        /// to mark draw line
        /// </summary>
        /// <param name="line">line which will be checked</param>
        /// <param name="pos">actually line number (row 1, row 2 etc)</param>
        private void markDraw(Line[] line, int pos)
        {
            line[pos].NumberOfCheckedPoint++;

            if(line[pos].isNeedToMark())
            {
                line[pos].IsMarked = true;
                countDrawLines++;
            }
        }

        private void showWinner(bool isWin)
        {
            if (isWin)
            {
                MessageBox.Show($"Congraturation Player {turn.ToString()} win this game");
            }
            else
            {
                MessageBox.Show($"Oops! you are draw this game");
            }
        }

        /// <summary>
        /// it will re-initialize all value which is used within this game
        /// </summary>
        private void reInit()
        {
            for(int i = 0; i < 3; i++)
            {
                initLineArray(lines[i]);
            }

            removeAllPictureBoxImage();

            turn = 1;
            isWin = false;
            countDrawLines = 0;
        }

        /// <summary>
        /// initialize Line class array 
        /// </summary>
        /// <param name="l"></param>
        private void initLineArray(Line[] l)
        {
            for (int i = 0; i < l.Length; i++)
            {
                if(l[i] == null)
                {
                    l[i] = new Line();
                }

                l[i].IsMarked = false;
                l[i].NumberOfCheckedPoint = 0;
                l[i].Score = 0;
            }
        }

        /// <summary>
        /// To remove all background image of picture boxes
        /// </summary>
        private void removeAllPictureBoxImage()
        {
            foreach (Control ct in this.Controls)
            {
                if (ct.GetType() == pbx1.GetType())
                {
                    PictureBox pb = (PictureBox)ct;
                    pb.Image = null;
                }
            }
        }

        /// <summary>
        /// initialize all variables
        /// </summary>
        private void init()
        {
            int tempSize = 0;

            for (int i = 0; i < 3; i++)
            {
                //only diagonal is need to 2 array for left direction & right direction, other depend on size
                tempSize = (i != 2) ? SIZE : DIASIZE;
                lines[i] = new Line[tempSize];
                initLineArray(lines[i]);
            }
        }

        /// <summary>
        /// When form is loaded initiate all variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SKimTicTacToe_Load(object sender, EventArgs e)
        {
            init();
        }
    }
}