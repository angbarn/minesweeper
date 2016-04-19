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
    public partial class Form1 : Form
    {

        List<System.Windows.Forms.Button> buttonList;
        int buttonDimensions;
        int gridWidth;
        int gridHeight;

        public Form1()
        {
            InitializeComponent();
            buttonDimensions = 32;
            gridWidth = 10;
            gridHeight = 10;
        }

        private void mineButtonClick(object sender, EventArgs e)
        {
            string locationString = "";
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (sender == buttonList[i])
                {
                    int indexAbove = i - gridWidth;
                    int indexBelow = i + gridWidth;
                    int indexRight = i + 1;
                    int indexLeft = i - 1;

                    if (indexAbove >= 0)
                    {
                        buttonList[indexAbove].Hide();
                    }
                    if ((indexLeft >= 0) & ((int)(i / gridWidth) == (int)(indexRight / gridWidth)))
                    {
                        buttonList[indexLeft].Hide();
                    }
                    if ((indexRight < buttonList.Count) & ((int)(i / gridWidth) == (int)(indexRight / gridWidth)))
                    {
                        buttonList[indexRight].Hide();
                    }
                    if (indexBelow < buttonList.Count)
                    {
                        buttonList[indexBelow].Hide();
                    }
                }
            }
            Console.WriteLine(sender.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            buttonList = new List<System.Windows.Forms.Button> { };
            SuspendLayout();
            //SuspendLayout();
            for (int y = 0; y < gridWidth; y++)
            {
                for (int x = 0; x < gridHeight; x++)
                {
                    buttonNew = new System.Windows.Forms.Button();
                    //SuspendLayout();
                    int locationX = buttonDimensions + x * buttonDimensions;
                    int locationY = buttonDimensions + y * buttonDimensions;
                    buttonNew.Location = new System.Drawing.Point(locationX, locationY);
                    buttonNew.Name = "button_" + x.ToString() + "_" + y.ToString();
                    buttonNew.Size = new System.Drawing.Size(buttonDimensions, buttonDimensions);
                    buttonNew.TabIndex = 0;
                    buttonNew.Text = "";
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
