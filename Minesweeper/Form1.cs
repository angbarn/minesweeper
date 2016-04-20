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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GameGrid gameBoard = new GameGrid(this, 10, 10);
        }
    }
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
        /// <param name="x">The x ordinate</param>
        /// <param name="y">The y ordinate</param>
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
    /// <summary>
    /// A grid of cells. Each cell keeps track of its own state.
    /// </summary>
    public class GameGrid
    {
        /// <summary>The array of <typeparamref name="GridCell"/> instances making up the game board</summary>
        private GridCell[,] cellArray;
        /// <summary>The dimensions of the buttons on screen that represent the cells</summary>
        public int buttonDimensions;
        /// <summary>The width of the game board, in cells</summary>
        public int gridWidth;
        /// <summary>The height of the game board, in cells</summary>
        public int gridHeight;
        /// <summary>The parent <typeparamref name="Form"/> of this instance</summary>
        public Form1 parentForm;

        /// <summary>
        /// A grid of cells that make up a game. Each cell will keep track of its own state.
        /// </summary>
        /// <param name="width">The width of the game board in cells</param>
        /// <param name="height">The height of the game board in cells</param>
        public GameGrid(Form1 parent, int width, int height)
        {
            parentForm = parent;
            gridWidth = width;
            gridHeight = height;
            cellArray = new GridCell[gridWidth, gridHeight];
            //Create the grid
            populateGrid();
        }
        /// <summary>
        /// Gets a specific cell from <paramref name="cellList"/> based on a specified location
        /// </summary>
        /// <param name="cellCoordinate"></param>
        /// <returns>Cell from <paramref name="cellList"/></returns>
        public GridCell getCell(Coordinate cellCoordinate)
        {
            return cellArray[cellCoordinate.get()[0], cellCoordinate.get()[1]];
        }
        /// <summary>
        /// Gets all cells adjacent to this one, including diagonals
        /// </summary>
        /// <param name="centreCellCoordinate">The coordinate of the cell to search around</param>
        /// <returns>Adjacent cells in a list - moves left, right, down, left, right, etc.</returns>
        public List<GridCell> getAdjacentCells(Coordinate centreCellCoordinate)
        {
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
        /// <summary>
        /// Creates the actual grid of cells
        /// </summary>
        private void populateGrid()
        {
            parentForm.SuspendLayout();
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    GridCell newCell = new GridCell(this, new Coordinate(x, y));
                    cellArray[x, y] = newCell;
                }
            }

        }
    }
    /// <summary>
    /// A single cell in an instance of <typeparamref name="GameGrid"/>
    /// </summary>
    public class GridCell
    {
        /// <summary>The <typeparamref name="GameGrid"/> this cell belongs to</summary>
        GameGrid parent;
        /// <summary>The GUI button that the user will see</summary>
        Button cellButton;
        /// <summary>Whether or not the cell is actually mined</summary>
        bool mined;
        /// <summary>The current state of the cell, i.e., how it will appear (and behave, to an extent)</summary>
        cellState state;
        /// <summary>The position in the <typeparamref name="GameGrid"/> array this cell exists in</summary>
        Coordinate position;
        /// <summary>
        /// Sets the basic information about the cell. Much of the rest of the work is handled automatically.
        /// </summary>
        /// <param name="parentGrid"></param>
        /// <param name="gridLocation"></param>
        public GridCell(GameGrid parentGrid, Coordinate gridLocation)
        {
            parent = parentGrid;
            state = cellState.normal;
            mined = false;
            position = gridLocation;
        }
        /// <summary>
        /// Creates the GUI button that the user will see, and that will receive input
        /// </summary>
        /// <returns>A <typeparamref name="Button"/> instance representing this <typeparamref name="GridCell"/></returns>
        private Button CreateButton()
        {
            Button newButton = new Button();

            int locationX = parent.buttonDimensions * position.get()[0];
            int locationY = parent.buttonDimensions * position.get()[1];
            newButton.Location = new Point(locationX, locationY);

            newButton.Name = "button_" + locationX.ToString() + "_" + locationY.ToString();
            newButton.Size = new Size(locationX, locationY);
            newButton.Text = "-";
            newButton.UseVisualStyleBackColor = true;

            newButton.MouseClick += new MouseEventHandler(ButtonMouseClick);

            parent.parentForm.Controls.Add(newButton);

            return newButton;
        }
        /// <summary>
        /// <para>Sweeps the cell for mines</para>
        /// <para>If there are mines, the game is over</para>
        /// <para>If there are no mines, the number of adjacent mines is displayed</para>
        /// <para>If there are no adjacent mines, adjacent cells are activated</para>
        /// </summary>
        private void Activate()
        {
            if (mined)
            {
                Console.WriteLine("This cell was mined");
                state = cellState.exploded;
            }
            else
            {
                List<GridCell> adjacentCells = parent.getAdjacentCells(position);
            }
        }
        ///<summary>
        ///Cycles between the three possible states of the button before it's pushed
        ///Normal -> Flagged -> Unsure -> ...
        ///</summary>
        private void ToggleFlagState()
        {
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
    }
}
