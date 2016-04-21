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
    public partial class Form2 : Form
    {
        Form1 parentForm;
        public Form2(Form1 parent)
        {
            parentForm = parent;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            labelGridWidth.Text = "Grid Width: " + scrollWidth.Value;
            labelGridHeight.Text = "Grid Height: " + scrollHeight.Value;
            labelMineCount.Text = "Mine Count: " + scrollMines.Value;
            scrollMines.Maximum =  scrollWidth.Value * scrollHeight.Value;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            parentForm.updateOptions(scrollWidth.Value, scrollHeight.Value, scrollMines.Value);
            Close();
        }
    }
}
