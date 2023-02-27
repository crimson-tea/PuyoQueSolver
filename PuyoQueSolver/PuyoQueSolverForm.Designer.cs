
namespace PuyoQueSolver
{
    partial class MainForm
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
            this.BoardPictureBox = new System.Windows.Forms.PictureBox();
            this.GenerateRandomBoardButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DelPuyosCountTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ElapsedTextBox = new System.Windows.Forms.TextBox();
            this.RouteLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Set3Button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SolveButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ColorCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BoardPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RouteLengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // BoardPictureBox
            // 
            this.BoardPictureBox.Location = new System.Drawing.Point(12, 12);
            this.BoardPictureBox.Name = "BoardPictureBox";
            this.BoardPictureBox.Size = new System.Drawing.Size(400, 325);
            this.BoardPictureBox.TabIndex = 0;
            this.BoardPictureBox.TabStop = false;
            // 
            // GenerateRandomBoardButton
            // 
            this.GenerateRandomBoardButton.Location = new System.Drawing.Point(175, 343);
            this.GenerateRandomBoardButton.Name = "GenerateRandomBoardButton";
            this.GenerateRandomBoardButton.Size = new System.Drawing.Size(106, 23);
            this.GenerateRandomBoardButton.TabIndex = 2;
            this.GenerateRandomBoardButton.Text = "GenerateBoard";
            this.GenerateRandomBoardButton.UseVisualStyleBackColor = true;
            this.GenerateRandomBoardButton.Click += new System.EventHandler(this.RandomBoardButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(418, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "DeletedCount:";
            // 
            // DelPuyosCountTextBox
            // 
            this.DelPuyosCountTextBox.Location = new System.Drawing.Point(418, 115);
            this.DelPuyosCountTextBox.Name = "DelPuyosCountTextBox";
            this.DelPuyosCountTextBox.Size = new System.Drawing.Size(96, 23);
            this.DelPuyosCountTextBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(418, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Rensa:";
            // 
            // ComboTextBox
            // 
            this.ComboTextBox.Location = new System.Drawing.Point(418, 71);
            this.ComboTextBox.Name = "ComboTextBox";
            this.ComboTextBox.Size = new System.Drawing.Size(96, 23);
            this.ComboTextBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(418, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Elapsed (ms):";
            // 
            // ElapsedTextBox
            // 
            this.ElapsedTextBox.Location = new System.Drawing.Point(418, 27);
            this.ElapsedTextBox.Name = "ElapsedTextBox";
            this.ElapsedTextBox.Size = new System.Drawing.Size(96, 23);
            this.ElapsedTextBox.TabIndex = 9;
            // 
            // RouteLengthNumericUpDown
            // 
            this.RouteLengthNumericUpDown.Location = new System.Drawing.Point(418, 343);
            this.RouteLengthNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RouteLengthNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RouteLengthNumericUpDown.Name = "RouteLengthNumericUpDown";
            this.RouteLengthNumericUpDown.Size = new System.Drawing.Size(96, 23);
            this.RouteLengthNumericUpDown.TabIndex = 16;
            this.RouteLengthNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Set3Button
            // 
            this.Set3Button.Location = new System.Drawing.Point(418, 299);
            this.Set3Button.Name = "Set3Button";
            this.Set3Button.Size = new System.Drawing.Size(96, 23);
            this.Set3Button.TabIndex = 22;
            this.Set3Button.Text = "Connect 4";
            this.Set3Button.UseVisualStyleBackColor = true;
            this.Set3Button.Click += new System.EventHandler(this.Set3Button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(418, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "CurrentRule:";
            // 
            // SolveButton
            // 
            this.SolveButton.Location = new System.Drawing.Point(337, 343);
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(75, 23);
            this.SolveButton.TabIndex = 24;
            this.SolveButton.Text = "Solve";
            this.SolveButton.UseVisualStyleBackColor = true;
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(418, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 15);
            this.label5.TabIndex = 25;
            this.label5.Text = "なぞる最大の長さ:";
            // 
            // ColorCountNumericUpDown
            // 
            this.ColorCountNumericUpDown.Location = new System.Drawing.Point(108, 343);
            this.ColorCountNumericUpDown.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.ColorCountNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.ColorCountNumericUpDown.Name = "ColorCountNumericUpDown";
            this.ColorCountNumericUpDown.Size = new System.Drawing.Size(61, 23);
            this.ColorCountNumericUpDown.TabIndex = 26;
            this.ColorCountNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 27;
            this.label6.Text = "ドロップの種類数:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 375);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ColorCountNumericUpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SolveButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Set3Button);
            this.Controls.Add(this.RouteLengthNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DelPuyosCountTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ElapsedTextBox);
            this.Controls.Add(this.GenerateRandomBoardButton);
            this.Controls.Add(this.BoardPictureBox);
            this.Name = "MainForm";
            this.Text = "PuyoQueSolver";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BoardPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RouteLengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BoardPictureBox;
        private System.Windows.Forms.Button GenerateRandomBoardButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DelPuyosCountTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ComboTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ElapsedTextBox;
        private System.Windows.Forms.NumericUpDown RouteLengthNumericUpDown;
        private System.Windows.Forms.Button Set3Button;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SolveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown ColorCountNumericUpDown;
        private System.Windows.Forms.Label label6;
    }
}

