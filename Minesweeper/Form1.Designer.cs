﻿using System.Collections.Generic;

namespace Minesweeper
{
    class Mine
    {

    }
    class MineRow
    {
        //list<mine> mineColumn = new list<Mine>
    }
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.labelInformation = new System.Windows.Forms.Label();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.panelPrimary = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panelPrimary.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.buttonNewGame, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelInformation, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonOptions, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1088, 55);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNewGame.Location = new System.Drawing.Point(4, 4);
            this.buttonNewGame.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(209, 47);
            this.buttonNewGame.TabIndex = 5;
            this.buttonNewGame.Text = "Restart Game";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click_1);
            // 
            // labelInformation
            // 
            this.labelInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInformation.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelInformation.Location = new System.Drawing.Point(221, 0);
            this.labelInformation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInformation.Name = "labelInformation";
            this.labelInformation.Size = new System.Drawing.Size(644, 55);
            this.labelInformation.TabIndex = 4;
            this.labelInformation.Text = "Minesweeper";
            this.labelInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonOptions
            // 
            this.buttonOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOptions.Location = new System.Drawing.Point(873, 4);
            this.buttonOptions.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(211, 47);
            this.buttonOptions.TabIndex = 1;
            this.buttonOptions.Text = "Options";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // panelPrimary
            // 
            this.panelPrimary.ColumnCount = 1;
            this.panelPrimary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelPrimary.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.panelPrimary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrimary.Location = new System.Drawing.Point(0, 0);
            this.panelPrimary.Margin = new System.Windows.Forms.Padding(4);
            this.panelPrimary.Name = "panelPrimary";
            this.panelPrimary.RowCount = 2;
            this.panelPrimary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.panelPrimary.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelPrimary.Size = new System.Drawing.Size(1096, 769);
            this.panelPrimary.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 769);
            this.Controls.Add(this.panelPrimary);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelPrimary.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonOptions;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Label labelInformation;
        public System.Windows.Forms.TableLayoutPanel panelPrimary;
        private System.Windows.Forms.Timer timer1;
    }
}

