/*  NPuzzle.cs
 *  Assignment 3
 *  Revision History:
 *      soochang: 2016. 10. 16 Created
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKimAssignment03
{

    /// <summary>
    /// Npuzzle game form class to handle evnets
    /// </summary>
    public partial class NPuzzleForm : Form
    {


        /// <summary>
        /// Total number of times to move to destination
        /// </summary>
        const int TOTALTIMES = 40;

        /// <summary>
        /// Random object to generate random puzzle game
        /// </summary>
        Random ran = new Random();


        /// <summary>
        /// Application constructor
        /// </summary>
        public NPuzzleForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Process to close current form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Process to generate puzzles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            BoardManager.removePuzzles(this);

            if (!BoardManager.validateInput(txtRows.Text, txtColumns.Text))
            {
                MessageBox.Show("Please enter valid inputs");
                return;
            }

            BoardManager.initGame(this, ran);
        }

        /// <summary>
        /// Proccess to init button move animation if movable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnNumber_Click(object sender, EventArgs e)
        {
            Puzzle b = (Puzzle)sender;
            BoardManager.buttonClicked(b);

            if(BoardManager.checkMovability())
            {
                timer.Enabled = true;
            }
        }

        /// <summary>
        /// Process to give button move animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if(!BoardManager.stop())
            { 
                BoardManager.move(TOTALTIMES);
            }
            else
            {
                timer.Enabled = false;
                BoardManager.reset();
                if(BoardManager.checkSolved())
                {
                    MessageBox.Show("You solve the puzzle","Congraturation");
                    BoardManager.setPuzzleStatus();
                }
            }
        }

        /// <summary>
        /// Process to save current data to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSave_Click(object sender, EventArgs e)
        {
            DialogResult dr = dlgSave.ShowDialog();

            switch (dr)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        FileManager fm = new FileManager(dlgSave.FileName);
                        fm.saveFile();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Sorry erorr occured\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Process to load file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileLoad_Click(object sender, EventArgs e)
        {
            DialogResult dr = dlgOpen.ShowDialog();

            switch (dr)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    try
                    {
                        FileManager fm = new FileManager(dlgOpen.FileName);
                        fm.loadFile(this);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Sorry can't load file\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }
    }
}
