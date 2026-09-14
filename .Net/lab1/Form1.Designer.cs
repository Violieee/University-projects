namespace lab1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            dataGridView1 = new DataGridView();
            x = new DataGridViewTextBoxColumn();
            y = new DataGridViewTextBoxColumn();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            menuStrip1 = new MenuStrip();
            масивиToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(25, 72);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(96, 27);
            textBox1.TabIndex = 0;
            textBox1.Text = "2.9";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(144, 71);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(96, 27);
            textBox2.TabIndex = 1;
            textBox2.Text = "17.48";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(261, 71);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(96, 27);
            textBox3.TabIndex = 2;
            textBox3.Text = "0.3";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(378, 71);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(96, 27);
            textBox4.TabIndex = 3;
            textBox4.Text = "3";
            // 
            // button1
            // 
            button1.AccessibleName = "";
            button1.BackColor = SystemColors.ControlLightLight;
            button1.Location = new Point(614, 66);
            button1.Name = "button1";
            button1.Size = new Size(94, 32);
            button1.TabIndex = 4;
            button1.Text = "Обчислити";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 49);
            label1.Name = "label1";
            label1.Size = new Size(24, 20);
            label1.TabIndex = 5;
            label1.Text = "xn";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(184, 48);
            label2.Name = "label2";
            label2.Size = new Size(23, 20);
            label2.TabIndex = 6;
            label2.Text = "xk";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(294, 49);
            label3.Name = "label3";
            label3.Size = new Size(24, 20);
            label3.TabIndex = 7;
            label3.Text = "xh";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(420, 49);
            label4.Name = "label4";
            label4.Size = new Size(17, 20);
            label4.TabIndex = 8;
            label4.Text = "a";
            label4.Click += label4_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { x, y });
            dataGridView1.Location = new Point(507, 132);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(304, 289);
            dataGridView1.TabIndex = 9;
            // 
            // x
            // 
            x.HeaderText = "x";
            x.MinimumWidth = 6;
            x.Name = "x";
            x.Width = 125;
            // 
            // y
            // 
            y.HeaderText = "y";
            y.MinimumWidth = 6;
            y.Name = "y";
            y.Width = 125;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Location = new Point(25, 132);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(449, 289);
            chart1.TabIndex = 10;
            chart1.Text = "chart1";
            chart1.Click += chart1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { масивиToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(870, 28);
            menuStrip1.TabIndex = 11;
            menuStrip1.Text = "menuStrip1";
            // 
            // масивиToolStripMenuItem
            // 
            масивиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            масивиToolStripMenuItem.Name = "масивиToolStripMenuItem";
            масивиToolStripMenuItem.Size = new Size(77, 24);
            масивиToolStripMenuItem.Text = "Масиви";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(224, 26);
            toolStripMenuItem1.Text = "Одновимірний";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(224, 26);
            toolStripMenuItem2.Text = "Двовимірний";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 450);
            Controls.Add(chart1);
            Controls.Add(dataGridView1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn x;
        private DataGridViewTextBoxColumn y;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem масивиToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
    }
}
