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
    internal partial class Form2 : Form
    {
        Form1 parentForm;
        internal Form2(Form1 parent, int initialWidth, int initialHeight, double initialMines)
        {
            parentForm = parent;
            InitializeComponent();
            scrollWidth.Value = initialWidth;
            scrollHeight.Value = initialHeight;
            scrollMines.Value = (int)(initialMines * 100);
            updateScrollBars();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            updateScrollBars();
        }

        public void updateScrollBars()
        {
            labelGridWidth.Text = "Grid Width: " + scrollWidth.Value;
            labelGridHeight.Text = "Grid Height: " + scrollHeight.Value;
            labelMineCount.Text = "Mines: " + (scrollMines.Value).ToString() + "%";
        }
        

        private void buttonSave_Click(object sender, EventArgs e)
        {
            parentForm.updateOptions(scrollWidth.Value, scrollHeight.Value, (double)(scrollMines.Value)/100);
            Close();
        }
    }
}
