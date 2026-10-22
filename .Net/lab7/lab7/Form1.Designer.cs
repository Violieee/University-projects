namespace lab7
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
            label1 = new Label();
            txtWeight = new TextBox();
            btnCheck = new Button();
            btnReset = new Button();
            lstLog = new ListBox();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 76);
            label1.Name = "label1";
            label1.Size = new Size(116, 20);
            label1.TabIndex = 0;
            label1.Text = "Вага деталі (кг):";
            // 
            // txtWeight
            // 
            txtWeight.Location = new Point(134, 73);
            txtWeight.Name = "txtWeight";
            txtWeight.Size = new Size(125, 27);
            txtWeight.TabIndex = 1;
            // 
            // btnCheck
            // 
            btnCheck.Location = new Point(275, 72);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(171, 29);
            btnCheck.TabIndex = 2;
            btnCheck.Text = "Перевірити деталь";
            btnCheck.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(275, 107);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(171, 29);
            btnReset.TabIndex = 3;
            btnReset.Text = "Скинути аварію";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // lstLog
            // 
            lstLog.FormattingEnabled = true;
            lstLog.Location = new Point(12, 279);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(434, 124);
            lstLog.TabIndex = 4;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 256);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(44, 20);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Стан: конвеєр працює";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(461, 450);
            Controls.Add(lblStatus);
            Controls.Add(lstLog);
            Controls.Add(btnReset);
            Controls.Add(btnCheck);
            Controls.Add(txtWeight);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Конвеєрна лінія";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtWeight;
        private Button btnCheck;
        private Button btnReset;
        private ListBox lstLog;
        private Label lblStatus;
    }
}
