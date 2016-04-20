using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    enum cellState { normal, flagged, unsure, empty, numbered, exploded };
    /// <summary>
    /// A coordinate
    /// </summary>
    public class Coordinate
    {
        private int xOrdinate;
        private int yOrdinate;

        /// <summary>
        /// A coordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coordinate(int x, int y)
        {
            xOrdinate = x;
            yOrdinate = y;
        }
        /// <summary>
        /// Gets the coordinate
        /// </summary>
        /// <returns>An integer array, [x, y]</returns>
        public int[] get()
        {
            return new int[] { xOrdinate, yOrdinate };
        }
        /// <summary>
        /// Sets the ordinates of the coordinate
        /// </summary>
        /// <param name="x">The x ordinate</param>
        /// <param name="y">The y ordinate</param>
        public void set(int x, int y)
        {
            xOrdinate = x;
            yOrdinate = y;
        }
        /// <summary>
        /// Adds two coordinates together, dimension by dimension
        /// </summary>
        /// <param name="c1">The first coordinate</param>
        /// <param name="c2">The second coordinate</param>
        /// <returns>A single coordinate - the result of adding the two together</returns>
        public static Coordinate operator +(Coordinate c1, Coordinate c2)
        {
            int newX = c1.get()[0] + c2.get()[1];
            int newY = c1.get()[0] + c2.get()[1];
            return new Coordinate(newX, newY);
        }
    }
    public class GameGrid
    {
        /// <summary>
        /// A grid of cells. Each cell keeps track of its own state.
        /// </summary>
        private GridCell[,] cellList;
        public int buttonDimensions;
        public int gridWidth;
        public int gridHeight;
        public GameGrid(int width, int height)
        {
            gridWidth = width;
            gridHeight = height;
            cellList = new GridCell[gridWidth, gridHeight];
        }

        public GridCell getCell(Coordinate cellCoordinate)
        {
            return cellList[cellCoordinate.get()[0], cellCoordinate.get()[1]];
        }

        public List<GridCell> getAdjacentCells(Coordinate centreCellCoordinate)
        {
            ///<summary>
            ///Gets all cells adjacent to this one
            ///</summary>
            ///<returns>
            ///Adjacent cells in a list - moves left, right, down, left, right, etc
            /// </returns>
            //Create empty list
            List<GridCell> adjacentCellsList = new List<GridCell> { };
            //Move left, right, down, left, right, etc. to populate adjacent cells list
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    Coordinate offsetCoordinate = new Coordinate(x, y);
                    //If it's the centre cell, ignore it
                    if ((x == 0) & (y == 0))
                    {
                        continue;
                    }
                    //If it's too far left or right, ignore it
                    if ((x < 0) | (x >= gridWidth))
                    {
                        continue;
                    }
                    //If it's too far up or down, ignore it
                    if ((y < 0) | (y >= gridHeight))
                    {
                        continue;
                    }
                    //It's passed all tests - add it to the adjacent cells list
                    Coordinate newAdjacentCellCoordinate = offsetCoordinate + centreCellCoordinate;
                    adjacentCellsList.Add(getCell(newAdjacentCellCoordinate));
                }
            }
            return adjacentCellsList;
        }

        private void populateGrid()
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    GridCell newCell = new GridCell(this, new Coordinate(x, y));

                }
            }
        }
    }
    public class GridCell
    {
        GameGrid parent;
        Button cellButton;
        bool mined;
        cellState state;
        Coordinate position;

        public GridCell(GameGrid parentGrid, Coordinate assignedIndex)
        {
            parent = parentGrid;
            state = cellState.normal;
            mined = false;
            position = assignedIndex;
        }
        private Button CreateButton()
        {
            Button newButton = new Button;

            int locationX = parent.buttonDimensions * position.get()[0];
            int locationY = parent.buttonDimensions * position.get()[1];
            newButton.Location = new Point(locationX, locationY);

            newButton.Name = "button_" + locationX.ToString() + "_" + locationY.ToString();
            newButton.Size = new Size(locationX, locationY);
            newButton.Text = "-";
            newButton.UseVisualStyleBackColor = true;

            newButton.MouseClick += new MouseEventHandler(ButtonMouseClick);

            return newButton;
        }
        private void Activate()
        {
            if (mined)
            {
                Console.WriteLine("This cell was mined");
                state = cellState.exploded;
            }
            else
            {
                GridCell[] adjacentCells = parent.getAdjacentCells(position);
            }
        }
        private void ToggleFlagState()
        {
            ///<summary>
            ///Cycles between the three possible states of the button before it's pushed
            ///Normal -> Flagged -> Unsure -> ...
            ///</summary>
            //Only toggle if we've not been clicked already
            if (state > cellState.unsure)
            {
                return;
            }
            //Toggle state between the three possibilities
            state = state + 1;
            if (state > cellState.unsure)
            {
                state = cellState.normal;
            }
        }
        private void ButtonMouseClick(object sender, EventArgs e)
        {

        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GameGrid gameBoard = new GameGrid(10, 10);

        }

        private int[] mineButtonGetAdjacent(int centreCellIndex)
        ///<summary>
        ///Gets cells adjacent to this one
        ///NSEW, above, below, right, left
        ///A value of -1 indicates there is no cell available
        ///</summary>
        ///<param name="centreCellIndex">The index of the centre cell</param>
        {
            //Default to not being available
            int[] arrayAdjacent = { -1, -1, -1, -1 };
            //Calculate index positions of adjacent cells, assuming they're there
            int indexAbove = centreCellIndex - gridWidth;
            int indexBelow = centreCellIndex + gridWidth;
            int indexRight = centreCellIndex + 1;
            int indexLeft = centreCellIndex - 1;
            //Update NSEW if the cell is actually present
            //Index wouldn't be negative
            if (indexAbove >= 0)
            {
                arrayAdjacent[0] = indexAbove;
            }
            //Index wouldn't be greater than the total available
            if (indexBelow < buttonList.Count)
            {
                arrayAdjacent[1] = indexBelow;
            }
            //Index wouldn't be greater than the total available, and we're in the same row
            if ((indexRight < buttonList.Count) & ((int)(centreCellIndex / gridWidth) == (int)(indexRight / gridWidth)))
            {
                arrayAdjacent[2] = indexRight;
            }
            //Index wouldn't be negative, and we're in the same row
            if ((indexLeft >= 0) & ((int)(centreCellIndex / gridWidth) == (int)(indexLeft / gridWidth)))
            {
                arrayAdjacent[3] = indexLeft;
            }
            return arrayAdjacent;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            buttonList = new List<Button> { };
            SuspendLayout();
            //SuspendLayout();
            for (int y = 0; y < gridWidth; y++)
            {
                for (int x = 0; x < gridHeight; x++)
                {
                    buttonNew = new Button();
                    //SuspendLayout();
                    int locationX = buttonDimensions + x * buttonDimensions;
                    int locationY = buttonDimensions + y * buttonDimensions;
                    buttonNew.Location = new System.Drawing.Point(locationX, locationY);
                    buttonNew.Name = "button_" + x.ToString() + "_" + y.ToString();
                    buttonNew.Size = new System.Drawing.Size(buttonDimensions, buttonDimensions);
                    buttonNew.TabIndex = 0;
                    buttonNew.Text = x.ToString();
                    buttonNew.UseVisualStyleBackColor = true;

                    buttonNew.Click += new EventHandler(mineButtonClick);
                    //newButton.Click += new System.EventHandler(newButton);

                    buttonList.Add(buttonNew);
                    Controls.Add(buttonNew);
                }
            }
            ResumeLayout(false);
        }
    }
}
