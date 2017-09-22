/*  FileManager.cs
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
using System.IO;


namespace SKimAssignment03
{
    /// <summary>
    /// FileManager class to control file stream
    /// </summary>
    class FileManager
    {
        ///Fields of class
        #region Fields
        /// <summary>
        /// Const value of class
        /// </summary>
        const string DIVIDER = "\t";
        const int ASCIIFORZERO = 48;
        const int ASCIIDIVDER = 9; // "\t"
        const int ASCIICARRIEGRETURN = 13; // "\n"
        const int ASCCILINEFEED = 10;

        /// <summary>
        /// Variables of class
        /// </summary>
        private string _fileName; 
        #endregion


        /// <summary>
        ///  Properties for Filemanager 
        /// </summary>
        #region Properties
        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        } 
        #endregion

        /// <summary>
        /// Constructor with file name
        /// </summary>
        /// <param name="fileName">file path of selected file</param>
        public FileManager(string fileName)
        {
            this.FileName = fileName;
        }

        /// <summary>
        /// Save current puzzle to file
        /// </summary>
        public void saveFile()
        {
            StreamWriter writer = new StreamWriter(FileName);

            writer.WriteLine(BoardManager.Row);
            writer.WriteLine(BoardManager.Col);

            for(int i = 0; i < BoardManager.Row; i++)
            {
                for(int j = 0; j < BoardManager.Col; j++)
                {
                    writer.Write(BoardManager.Board[i, j] + DIVIDER);
                }
                writer.WriteLine();
            }
            writer.Close();
        }

        /// <summary>
        /// Load file and reinitialize the boad
        /// </summary>
        /// <param name="np">NPuzzle Form</param>
        public void loadFile(NPuzzleForm np)
        {
            StreamReader reader = new StreamReader(FileName);

            int row = int.Parse(reader.ReadLine());
            int col = int.Parse(reader.ReadLine());

            int[,] board = new int[row, col];

            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    board[i, j] = int.Parse(getString(reader, ASCIIDIVDER));
                    flushStream(reader);
                }
            }
            BoardManager.reInitGame(np, row, col, board);
            reader.Close();
        }

        /// <summary>
        /// To remove dilimiters
        /// </summary>
        /// <param name="reader">StreamReader object to be read</param>
        private void flushStream(StreamReader reader)
        {
            int temp = reader.Peek();
            while (!reader.EndOfStream && (temp == ASCIICARRIEGRETURN || temp == ASCIIDIVDER || temp == ASCCILINEFEED))
            {
                reader.Read();
                temp = reader.Peek();
            }
        }

        /// <summary>
        /// To retrieve board value from file
        /// </summary>
        /// <param name="reader">StreamReader object to be read</param>
        /// <param name="divider">string divider distinguish inputs</param>
        /// <returns>Converted intgere string</returns>
        private string getString(StreamReader reader, int divider)
        {
            string temp = "";
            while(!reader.EndOfStream && reader.Peek() != divider)
            {
                temp += (reader.Read() - ASCIIFORZERO).ToString();
            }
            return temp;
        }
    }
}
