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
        internal Form2(Form1 parent, int initialWidth, int initialHeight, int initialMines)
        {
            parentForm = parent;
            InitializeComponent();
            scrollWidth.Value = initialWidth;
            scrollHeight.Value = initialHeight;
            scrollMines.Value = (int)(((float)initialMines / (initialWidth * initialHeight)) * 100);
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
            labelMineCount.Text = "Mines: " + scrollMines.Value + "%";
        }
        

        private void buttonSave_Click(object sender, EventArgs e)
        {
            float minesValue = ((float)scrollMines.Value / 100) * (scrollHeight.Value * scrollHeight.Value);
            parentForm.updateOptions(scrollWidth.Value, scrollHeight.Value, (int)minesValue);
            Close();
        }
    }
}
