/*  BoardManager.cs
 *  Assignment 3
 *  Revision History:
 *      soochang: 2016. 10. 16 Created
 * 
 */


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SKimAssignment03
{
    /// <summary>
    /// BoardManager class to manage board game
    /// </summary>
    class BoardManager
    {
        //Fields to be used within the class
        #region Fields
        /// <summary>
        /// Const value
        /// </summary>
        public const int TOP = 100;
        public const int LEFT = 20;

        /// <summary>
        /// Global variable for this game
        /// </summary>
        private static int[,] _board;
        private static Puzzle[,] _puzzles;
        private static int _row;
        private static int _col;
        private static Point _start;
        private static Point _destination;
        private static Point _idxCliked;
        private static Point _idxDestination;
        private static bool _isMoving;
        private static int _size;
        #endregion

        /// <summary>
        /// Properties of BoardManager class
        /// </summary>
        #region Properties
        public static int Row
        {
            get
            {
                return _row;
            }

            set
            {
                _row = value;
            }
        }

        public static int Col
        {
            get
            {
                return _col;
            }

            set
            {
                _col = value;
            }
        }

        public static int[,] Board
        {
            get
            {
                return _board;
            }

            set
            {
                _board = value;
            }
        }

        internal static Puzzle[,] Puzzles
        {
            get
            {
                return _puzzles;
            }

            set
            {
                _puzzles = value;
            }
        }

        public static bool IsMoving
        {
            get
            {
                return _isMoving;
            }

            set
            {
                _isMoving = value;
            }
        }

        public static Point Destination
        {
            get
            {
                return _destination;
            }

            set
            {
                _destination = value;
            }
        }

        public static Point Start
        {
            get
            {
                return _start;
            }

            set
            {
                _start = value;
            }
        }

        public static int Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }

        public static Point IdxDestination
        {
            get
            {
                return _idxDestination;
            }

            set
            {
                _idxDestination = value;
            }
        }

        public static Point IdxCliked
        {
            get
            {
                return _idxCliked;
            }

            set
            {
                _idxCliked = value;
            }
        }
        #endregion

        /// <summary>
        /// Validate input
        /// </summary>
        /// <param name="row">number of row</param>
        /// <param name="col">number of column</param>
        /// <returns>True only if inputs are correctly converted to integer</returns>
        public static bool validateInput(string row, string col)
        {
            if (int.TryParse(row, out _row) && int.TryParse(col, out _col))
            {
                if (Row > 1 && Col > 1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Change form size to display all puzzle
        /// </summary>
        /// <param name="np">Form which will be changed</param>
        public static void changeFormSize(NPuzzleForm np)
        {
            np.Width = max((Col + 1) * Puzzle.PUZZLESIZE + LEFT, np.Width);
            np.Height = max((Row + 1) * Puzzle.PUZZLESIZE + TOP + np.menuFile.Height, np.Height);
        }

        /// <summary>
        /// Method to find max number
        /// </summary>
        /// <param name="num1">candidate1 of max number</param>
        /// <param name="num2">candidate2 of max number</param>
        /// <returns>Max number among given number</returns>
        private static int max(int num1, int num2)
        {
            return num1 > num2 ? num1 : num2;
        }

        /// <summary>
        /// Method to compare given number
        /// </summary>
        /// <param name="num1">base number to be compared with other number</param>
        /// <param name="num2">compared number</param>
        /// <returns>True if num1 is bigger</returns>
        private static bool compare(int num1, int num2)
        {
            return num1 > num2;
        }

        /// <summary>
        /// Method to set values to move puzzles
        /// </summary>
        /// <param name="b">Puzzle which is clicked</param>
        public static void buttonClicked(Puzzle b)
        {
            if (!IsMoving)
            {
                int val = int.Parse(b.Text);
                Start = findVal(val);
                IdxCliked = new Point(b.IdxRow, b.IdxCol);
            }
        }

        /// <summary>
        /// Method to find puzzle index which match with given value
        /// </summary>
        /// <param name="val">Value to check whether the puzzle have the value or not</param>
        /// <returns>Indedx of row and colum</returns>
        private static Point findVal(int val)
        {
            for(int i = 0; i < Row ; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (Board[i,j] == val)
                    {
                        return new Point(i,j);
                    }
                }
            }
            return new Point();
        }

        /// <summary>
        /// Method to check if clicked puzzle is aloing with blank puzzle
        /// </summary>
        /// <returns>True if movable</returns>
        public static bool checkMovability()
        {
            return getDistance() == 1;
        }


        /// <summary>
        /// Method to get distance between clicked puzzle and blank puzzle
        /// </summary>
        /// <returns>Distance</returns>
        private static double getDistance()
        {
            return Math.Sqrt(Math.Pow(Puzzles[IdxDestination.X, IdxDestination.Y].Left - Puzzles[IdxCliked.X, IdxCliked.Y].Left, 2)
                + Math.Pow(Puzzles[IdxDestination.X, IdxDestination.Y].Top - Puzzles[IdxCliked.X, IdxCliked.Y].Top, 2)) / Puzzle.PUZZLESIZE;
        }
                
        /// <summary>
        /// Method to get absolute balue of given number
        /// </summary>
        /// <param name="num1">Number to be converted positive value</param>
        /// <returns>Positive inger number</returns>
        private static int abs(int num1)
        {
            return num1 > 0 ? num1 : - num1;
        }

        /// <summary>
        /// Method to check if the puzzle is reached destination
        /// </summary>
        /// <returns>True if reached to destination</returns>
        public static bool stop()
        {
            return Puzzles[IdxCliked.X, IdxCliked.Y].Left == Puzzles[IdxDestination.X, IdxDestination.Y].Left
                && Puzzles[IdxCliked.X, IdxCliked.Y].Top == Puzzles[IdxDestination.X, IdxDestination.Y].Top;
        }

        /// <summary>
        /// Method to move puzzle
        /// </summary>
        /// <param name="times">Number of times to reach destination</param>
        public static void move(int times)
        {
            IsMoving = true;
            Puzzles[IdxCliked.X, IdxCliked.Y].moveTo(times, getDirection(new Puzzle.Direction()));
        }

        /// <summary>
        /// Method to get direction
        /// </summary>
        /// <param name="d">Direction which will be assigned direction</param>
        /// <returns>Direction to move</returns>
        private static Puzzle.Direction getDirection(Puzzle.Direction d)
        {

            if (Destination.X != Start.X)
            {
                d = compare(Destination.X, Start.X) ? Puzzle.Direction.Down : Puzzle.Direction.Up;
            }

            if (Destination.Y != Start.Y)
            {
                d = compare(Destination.Y, Start.Y) ? Puzzle.Direction.Right : Puzzle.Direction.Left;
            }

            return d;
        }


        /// <summary>
        /// Method to reset position
        /// </summary>
        public static void reset()
        {
            IsMoving = false;
            setPosition();
        }

        /// <summary>
        /// Method to reset current position of puzzles
        /// </summary>
        private static void setPosition()
        {
            Puzzles[IdxDestination.X, IdxDestination.Y].Top = TOP + Start.X * Puzzle.PUZZLESIZE;
            Puzzles[IdxDestination.X, IdxDestination.Y].Left = LEFT + Start.Y * Puzzle.PUZZLESIZE;

            Board[Destination.X, Destination.Y] = Board[Start.X, Start.Y];
            Board[Start.X, Start.Y] = Size;
            Destination = Start;
        }

        /// <summary>
        /// Method to remove all puzzles
        /// </summary>
        /// <param name="np">Form where puzzles will be removed</param>
        public static void removePuzzles(NPuzzleForm np)
        {
            if(Puzzles != null)
            {
                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Col; j++)
                    {
                        np.Controls.Remove(Puzzles[i, j]);
                    }
                }
                Puzzles = null;
            }
        }

        /// <summary>
        /// Method to check if current game is solved
        /// </summary>
        /// <returns>True if solved</returns>
        public static bool checkSolved()
        {
            int counter = 1;
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (Board[i,j] != counter++)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Change puzzles status to keep current position
        /// </summary>
        public static void setPuzzleStatus()
        {
            for(int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                { 
                    Puzzles[i, j].Enabled = false;
                }
            }
        }

        /// <summary>
        /// Initialize game
        /// </summary>
        /// <param name="np">Form where puzzle will be added</param>
        /// <param name="r">Random object to generate random number</param>
        public static void initGame(NPuzzleForm np, Random r)
        {
            changeFormSize(np);

            do
            {
                generateGameBoard(r);
            } while (!checkSolvability());

            generatePuzzle(np);
        }

        /// <summary>
        /// Method to reinitialize game
        /// </summary>
        /// <param name="np">Form where puzzle will be added</param>
        /// <param name="row">Number of rows</param>
        /// <param name="col">Number of columns</param>
        /// <param name="board">Loaded puzzle position</param>
        public static void reInitGame(NPuzzleForm np, int row, int col, int[,] board)
        {
            removePuzzles(np);
            Row = row;
            Col = col;
            changeFormSize(np);
            generateGameBoard(board);
            generatePuzzle(np);
        }

        /// <summary>
        /// Method to generate board which will be used to keep track physical puzzle position
        /// </summary>
        /// <param name="ran">Random object to generate random number</param>
        private static void generateGameBoard(Random ran)
        {
            Board = new int[Row, Col];

            Size = Row * Col;
            List<int> lists = new List<int>(Size);

            for (int i = 0; i < Size; i++)
            {
                lists.Add(i + 1);
            }

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    int tempIndex = ran.Next(0, lists.Count);
                    Board[i, j] = lists[tempIndex];
                    lists.RemoveAt(tempIndex);
                }
            }
        }


        /// <summary>
        /// Method to generate game board from load file event
        /// </summary>
        /// <param name="loadedArray">Loaded puzzle position</param>
        private static void generateGameBoard(int[,] loadedArray)
        {
            Board = new int[Row, Col];

            Size = Row * Col;

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Board[i, j] = loadedArray[i, j];
                }
            }
        }

        /// <summary>
        /// Method to check if generated game is solvable
        /// </summary>
        /// <returns>True if the game is solvable</returns>
        private static bool checkSolvability()
        {
            int count = 0;

            for (int i = 0; i < Size; i++)
            {
                int r = i / Col;
                int c = i % Col;

                if (Board[r, c] == Size)
                {
                    count += Row - r - 1;
                    count += Col - c - 1;
                }

                for (int j = r; j < Row; j++)
                {

                    int z = (j == r) ? c : 0;
                    while (z < Col)
                    {
                        if (Board[r, c] > Board[j, z])
                        {
                            count++;
                        }
                        z++;
                    }
                }
            }

            if (count % 2 == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to generate puzzle
        /// </summary>
        /// <param name="np">Form where puzzle will be added</param>
        private static void generatePuzzle(NPuzzleForm np)
        {
            int x = TOP;
            int y = LEFT;

            Puzzles = new Puzzle[Row, Col];

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    x = TOP + i * Puzzle.PUZZLESIZE;
                    y = LEFT + j * Puzzle.PUZZLESIZE;

                    Puzzles[i, j] = new Puzzle(np, x, y, Board[i, j].ToString(), i, j);

                    if (Board[i, j] == Size)
                    {
                        Puzzles[i, j].Visible = false;
                        IdxDestination = Destination = new Point(i, j);
                    }
                }
            }
        }
    }
}