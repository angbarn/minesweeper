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
    public class Coordinate
    {
        private int xOrdinate;
        private int yOrdinate;

        public Coordinate(int x, int y)
        {
            xOrdinate = x;
            yOrdinate = y;
        }

        public int[] get()
        {
            return new int[] { xOrdinate, yOrdinate };
        }
        public void set(int x, int y)
        {
            xOrdinate = x;
            yOrdinate = y;
        }
    }
    public class GameGrid
    {
        List<GridCell> cellList;
        public int buttonDimensions;
        public int gridWidth;
        public int gridHeight;
        public GameGrid(int gridWidth, int gridHeight)
        {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
        }
        public GridCell[] GetAdjacentCells(index)
        {
            
        }
    }
    public class GridCell
    {
        GameGrid parent;
        Button cellButton;
        bool mined;
        cellState state;
        int index;

        public GridCell(GameGrid parentGrid, int assignedIndex)
        {
            parent = parentGrid;
            state = cellState.normal;
            mined = false;
            index = assignedIndex;
        }
        private Button CreateButton()
        {
            Button newButton = new Button;

            int locationX = parent.buttonDimensions * (index % parent.gridWidth);
            int locationY = parent.buttonDimensions * (int)(index / parent.gridWidth);
            newButton.Location = new Point(locationX, locationY);

            newButton.Name = "button_" + locationX.ToString() + "_" + locationY.ToString();
            newButton.Size = new Size(locationX, locationY);
            newButton.TabIndex = index;
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
                GridCell[] adjacentCells = parent.GetAdjacentCells(index);
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
