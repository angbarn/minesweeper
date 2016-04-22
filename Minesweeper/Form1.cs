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
    internal enum cellState { normal, flagged, unsure, empty, numbered, exploded };

    internal partial class Form1 : Form
    {
        int cellCountWidth;
        int cellCountHeight;
        double mineProbability;
        int timeElapsed;
        GameGrid gameBoard;
        internal Form1()
        {
            cellCountWidth = 10;
            cellCountHeight = 10;
            mineProbability = 0.1;
            InitializeComponent();
            //gameBoard = new GameGrid(this, mineProbability, cellCountWidth, cellCountHeight);
        }

        internal void updateOptions(int xDimension, int yDimension, double mineDimension)
        {
            cellCountWidth = xDimension;
            cellCountHeight = yDimension;
            mineProbability = mineDimension;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            Form2 settings = new Form2(this, cellCountWidth, cellCountHeight, mineProbability);
            settings.Show();
        }

        private void buttonNewGame_Click_1(object sender, EventArgs e)
        {
            if (gameBoard != null)
            {
                gameBoard.delete();
                panelPrimary.Controls.Remove(gameBoard.layoutGrid);
            }
            gameBoard = new GameGrid(this, mineProbability, cellCountWidth, cellCountHeight);
            labelInformation.Text = "Minesweeper";

            timeElapsed = -1;
            timer1_Tick();
        }

        internal void endGame(bool win)
        {
            timer1.Stop();
            if (win)
            {
                labelInformation.Text = "Congratulations! - " + secondsToTime(timeElapsed);
            }
            else
            {
                labelInformation.Text = "You lose";
            }
        }

        private string secondsToTime(int seconds)
        {
            int secondsElapsed = seconds % 60;
            int minutesElapsed = (seconds - secondsElapsed) / 60;
            return minutesElapsed.ToString() + ":" + secondsElapsed.ToString().PadLeft(2, '0');
        }
        /// <summary>
        /// Increments seconds count by 1, and writes out the time
        /// </summary>
        private void timer1_Tick()
        {
            timeElapsed = timeElapsed + 1;
            labelInformation.Text = secondsToTime(timeElapsed);
            timer1.Start();
        }
        /// <summary>
        /// Overload because we don't actually use any of the arguments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1_Tick();
        }
    }
    /// <summary>
    /// A coordinate
    /// </summary>
    internal class Coordinate
    {
        private int xOrdinate;
        private int yOrdinate;

        /// <summary>
        /// A coordinate
        /// </summary>
        /// <param name="x">The x ordinate</param>
        /// <param name="y">The y ordinate</param>
        internal Coordinate(int x, int y)
        {
            xOrdinate = x;
            yOrdinate = y;
        }
        /// <summary>
        /// Gets the coordinate
        /// </summary>
        /// <returns>An integer array, [x, y]</returns>
        internal int[] get()
        {
            return new int[] { xOrdinate, yOrdinate };
        }
        /// <summary>
        /// Sets the ordinates of the coordinate
        /// </summary>
        /// <param name="x">The x ordinate</param>
        /// <param name="y">The y ordinate</param>
        internal void set(int x, int y)
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
            int newX = c1.get()[0] + c2.get()[0];
            int newY = c1.get()[1] + c2.get()[1];
            return new Coordinate(newX, newY);
        }

        public static bool operator ==(Coordinate c1, Coordinate c2)
        {
            return ((c1.get()[0] == c2.get()[0]) & (c1.get()[1] == c2.get()[1]));
        }

        public static bool operator !=(Coordinate c1, Coordinate c2)
        {
            return (!((c1.get()[0] == c2.get()[0]) & (c1.get()[1] == c2.get()[1])));
        }
    }
    /// <summary>
    /// A grid of cells. Each cell keeps track of its own state.
    /// </summary>
    internal class GameGrid
    {
        /// <summary>The array of <typeparamref name="GridCell"/> instances making up the game board</summary>
        private GridCell[,] cellArray;
        /// <summary>The dimensions of the buttons on screen that represent the cells</summary>
        internal int buttonDimensions;
        /// <summary>The width of the game board, in cells</summary>
        internal int gridWidth;
        /// <summary>The height of the game board, in cells</summary>
        internal int gridHeight;
        /// <summary>The number of mines of the board</summary>
        private double mineChance;
        /// <summary>The parent <typeparamref name="Form"/> of this instance</summary>
        internal Form1 parentForm;
        /// <summary>Used to make the controls auto size to the window correctly</summary>
        internal TableLayoutPanel layoutGrid;

        /// <summary>
        /// A grid of cells that make up a game. Each cell will keep track of its own state.
        /// </summary>
        /// <param name="width">The width of the game board in cells</param>
        /// <param name="height">The height of the game board in cells</param>
        internal GameGrid(Form1 parent, double mines, int width, int height)
        {
            parentForm = parent;
            gridWidth = width;
            gridHeight = height;
            mineChance = mines;
            cellArray = new GridCell[gridWidth, gridHeight];

            layoutGrid = new TableLayoutPanel();
            layoutGrid.ColumnCount = gridWidth;
            layoutGrid.RowCount = gridHeight;
            layoutGrid.Dock = DockStyle.Fill;
            layoutGrid.Location = new Point(0, 0);
            layoutGrid.Name = "LayoutGrid";
            for (int i = 0; i < gridWidth; i++)
            {
                layoutGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / gridWidth));
            }
            for (int i = 0; i < gridHeight; i++)
            {
                layoutGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / gridHeight));
            }

            buttonDimensions = 64;
            //Create the grid
            populateGrid();
        }
        /// <summary>
        /// Gets a specific cell from <paramref name="cellList"/> based on a specified location
        /// </summary>
        /// <param name="cellCoordinate"></param>
        /// <returns>Cell from <paramref name="cellList"/>, or null if one at the specified coordinate doesn't exist</returns>
        internal GridCell getCell(Coordinate cellCoordinate)
        {
            int x = cellCoordinate.get()[0];
            int y = cellCoordinate.get()[1];
            //Make sure the cell exists before attempting to return it
            if ((x < 0) | (x >= gridWidth))
            {
                return null;
            }
            if ((y < 0) | (y >= gridHeight))
            {
                return null;
            }
            //Return it if it exists
            return cellArray[cellCoordinate.get()[0], cellCoordinate.get()[1]];
        }
        /// <summary>
        /// Gets all cells adjacent to this one, including diagonals
        /// </summary>
        /// <param name="centreCellCoordinate">The coordinate of the cell to search around</param>
        /// <returns>Adjacent cells in a list - moves left, right, down, left, right, etc.</returns>
        internal List<GridCell> getAdjacentCells(Coordinate centreCellCoordinate, bool onlyMined = true)
        {
            //Create empty list
            List<GridCell> adjacentCellsList = new List<GridCell> { };
            //Move left, right, down, left, right, etc. to populate adjacent cells list
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    Coordinate newAdjacentCellCoordinate = new Coordinate(x, y) + centreCellCoordinate;
                    GridCell adjacentCell = getCell(newAdjacentCellCoordinate);
                    if (newAdjacentCellCoordinate == centreCellCoordinate)
                    {
                        continue;
                    }
                    if (adjacentCell == null)
                    {
                        continue;
                    }
                    if ((onlyMined) & (!adjacentCell.isMined()))
                    {
                        continue;
                    }
                    adjacentCellsList.Add(adjacentCell);
                }
            }
            return adjacentCellsList;
        }
        /// <summary>
        /// Creates the actual grid of cells
        /// </summary>
        private void populateGrid()
        {
            //Generate random locations for mines
            Random randomIndexGenerator = new Random();

            parentForm.SuspendLayout();
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    Coordinate location = new Coordinate(x, y);
                    GridCell newCell = new GridCell(this, location);

                    Console.WriteLine((double)randomIndexGenerator.Next(100) / 100);

                    if ((double)randomIndexGenerator.Next(100) / 100 < mineChance)
                    {
                        newCell.mineCell();
                    }

                    cellArray[x, y] = newCell;
                }
            }
            parentForm.panelPrimary.Controls.Add(layoutGrid);
            parentForm.ResumeLayout();
        }

        internal void checkVictory()
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    if (cellArray[x, y].state == cellState.exploded)
                    {
                        gameEnd(false);
                        return;
                    }
                    if ((cellArray[x, y].state == cellState.normal) & !(cellArray[x, y].isMined()))
                    {
                        return;
                    }
                }
            }
            Console.WriteLine("Woohoo");
            //Victory has been achieved
            gameEnd(true);
        }

        /// <summary>
        /// Ends the game
        /// </summary>
        /// <param name="win">Whether the game ended in defeat or victory</param>
        internal void gameEnd(bool win)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    cellArray[x, y].activate();
                }
            }
            parentForm.endGame(win);
        }

        internal void delete()
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    cellArray[x, y].delete();
                }
            }
        }
    }
    /// <summary>
    /// A single cell in an instance of <typeparamref name="GameGrid"/>
    /// </summary>
    internal class GridCell
    {
        /// <summary>The <typeparamref name="GameGrid"/> this cell belongs to</summary>
        GameGrid parent;
        /// <summary>The GUI button that the user will see</summary>
        Button cellButton;
        /// <summary>Whether or not the cell is actually mined</summary>
        bool mined;
        /// <summary>The current state of the cell, i.e., how it will appear (and behave, to an extent)</summary>
        internal cellState state;
        /// <summary>The position in the <typeparamref name="GameGrid"/> array this cell exists in</summary>
        Coordinate position;
        /// <summary>
        /// Sets the basic information about the cell. Much of the rest of the work is handled automatically.
        /// </summary>
        /// <param name="parentGrid"></param>
        /// <param name="gridLocation"></param>
        internal GridCell(GameGrid parentGrid, Coordinate gridLocation)
        {
            parent = parentGrid;
            state = cellState.normal;
            mined = false;
            position = gridLocation;
            cellButton = createButton();

            cellStateUpdate();
        }
        /// <summary>
        /// Creates the GUI button that the user will see, and that will receive input
        /// </summary>
        /// <returns>A <typeparamref name="Button"/> instance representing this <typeparamref name="GridCell"/></returns>
        private Button createButton()
        {
            Button newButton = new Button();

            int locationX = parent.buttonDimensions * position.get()[0];
            int locationY = parent.buttonDimensions * position.get()[1];
            newButton.Location = new Point(locationX, locationY);

            newButton.Name = "button_" + locationX.ToString() + "_" + locationY.ToString();
            //newButton.Size = new Size(parent.buttonDimensions, parent.buttonDimensions);
            newButton.Dock = DockStyle.Fill;
            newButton.BackgroundImageLayout = ImageLayout.Zoom;
            newButton.Text = "-";
            newButton.UseVisualStyleBackColor = true;
            newButton.TabIndex = 0;

            newButton.MouseDown += new MouseEventHandler(buttonMouseClick);

            parent.layoutGrid.Controls.Add(newButton, position.get()[0], position.get()[1]);
            //parent.parentForm.Controls.Add(newButton);

            return newButton;
        }
        /// <summary>
        /// <para>Sweeps the cell for mines</para>
        /// <para>If there are mines, the game is over</para>
        /// <para>If there are no mines, the number of adjacent mines is displayed</para>
        /// <para>If there are no adjacent mines, adjacent cells are activated</para>
        /// </summary>
        internal void activate()
        {
            if (state == cellState.normal)
            {
                if (mined)
                {
                    state = cellState.exploded;
                    parent.gameEnd(false);
                }
                else
                {
                    List<GridCell> adjacentCells = parent.getAdjacentCells(position);
                    if (adjacentCells.Count == 0)
                    {
                        state = cellState.empty;
                        adjacentCells = parent.getAdjacentCells(position, false);
                        for (int i = 0; i < adjacentCells.Count; i++)
                        {
                            adjacentCells[i].activate();
                        }
                    }
                    else
                    {
                        state = cellState.numbered;
                    }
                }
                cellStateUpdate();
                parent.checkVictory();
            }
        }
        ///<summary>
        ///Cycles between the three possible states of the button before it's pushed
        ///Normal -> Flagged -> Unsure -> ...
        ///</summary>
        private void flagStateToggle()
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
            cellStateUpdate();
        }
        /// <summary>
        /// Updates the appearance of the button based on its state
        /// </summary>
        private void cellStateUpdate()
        {
            cellButton.Show();
            cellButton.Text = "";
            cellButton.BackgroundImage = null;
            switch (state)
            {
                case cellState.empty:
                    cellButton.Hide();
                    break;
                case cellState.exploded:
                    cellButton.BackgroundImage = Properties.Resources.seamine;
                    break;
                case cellState.flagged:
                    cellButton.BackgroundImage = Properties.Resources.flag;
                    break;
                case cellState.normal:
                    break;
                case cellState.numbered:
                    List<GridCell> adjacentCells = parent.getAdjacentCells(position);
                    cellButton.Text = adjacentCells.Count.ToString();
                    break;
                case cellState.unsure:
                    cellButton.BackgroundImage = Properties.Resources.questionmark;
                    break;
            }
        }
        /// <summary>
        /// Marks this cell as mined
        /// </summary>
        internal void mineCell()
        {
            mined = true;
        }
        /// <summary>
        /// Whether the cell is mined or not
        /// </summary>
        /// <returns>true if cell is mined, false if not</returns>
        /// <remarks>This is only necessary because the data is encapsulated</remarks>
        internal bool isMined()
        {
            return mined;
        }

        internal void delete()
        {
            parent.parentForm.Controls.Remove(cellButton);
        }

        private void buttonMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                activate();
            }
            if (e.Button == MouseButtons.Right)
            {
                flagStateToggle();
            }
        }
    }
}
