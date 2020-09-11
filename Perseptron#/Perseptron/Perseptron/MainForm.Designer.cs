namespace Perseptron
{
    partial class MainForm
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
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.archTabPage = new System.Windows.Forms.TabPage();
            this.autosizeCheckBox = new System.Windows.Forms.CheckBox();
            this.createNetButton = new System.Windows.Forms.Button();
            this.addSliceButton = new System.Windows.Forms.Button();
            this.outLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.inLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.shadowSlicesLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tutorTabPage = new System.Windows.Forms.TabPage();
            this.classifyTabPage = new System.Windows.Forms.TabPage();
            this.mainTabControl.SuspendLayout();
            this.archTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outLengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inLengthNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.archTabPage);
            this.mainTabControl.Controls.Add(this.tutorTabPage);
            this.mainTabControl.Controls.Add(this.classifyTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(365, 404);
            this.mainTabControl.TabIndex = 0;
            // 
            // archTabPage
            // 
            this.archTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.archTabPage.Controls.Add(this.autosizeCheckBox);
            this.archTabPage.Controls.Add(this.createNetButton);
            this.archTabPage.Controls.Add(this.addSliceButton);
            this.archTabPage.Controls.Add(this.outLengthNumericUpDown);
            this.archTabPage.Controls.Add(this.inLengthNumericUpDown);
            this.archTabPage.Controls.Add(this.shadowSlicesLabel);
            this.archTabPage.Controls.Add(this.label2);
            this.archTabPage.Controls.Add(this.label1);
            this.archTabPage.Location = new System.Drawing.Point(4, 22);
            this.archTabPage.Name = "archTabPage";
            this.archTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.archTabPage.Size = new System.Drawing.Size(357, 378);
            this.archTabPage.TabIndex = 0;
            this.archTabPage.Text = "Архитектура";
            // 
            // autosizeCheckBox
            // 
            this.autosizeCheckBox.AutoSize = true;
            this.autosizeCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.autosizeCheckBox.Location = new System.Drawing.Point(11, 61);
            this.autosizeCheckBox.Name = "autosizeCheckBox";
            this.autosizeCheckBox.Size = new System.Drawing.Size(314, 21);
            this.autosizeCheckBox.TabIndex = 9;
            this.autosizeCheckBox.Text = "Определить длины по обучающей выборке";
            this.autosizeCheckBox.UseVisualStyleBackColor = true;
            this.autosizeCheckBox.CheckedChanged += new System.EventHandler(this.autosizeCheckBox_CheckedChanged);
            // 
            // createNetButton
            // 
            this.createNetButton.Location = new System.Drawing.Point(72, 325);
            this.createNetButton.Name = "createNetButton";
            this.createNetButton.Size = new System.Drawing.Size(208, 47);
            this.createNetButton.TabIndex = 8;
            this.createNetButton.Text = "Создать сеть";
            this.createNetButton.UseVisualStyleBackColor = true;
            // 
            // addSliceButton
            // 
            this.addSliceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.addSliceButton.Location = new System.Drawing.Point(27, 125);
            this.addSliceButton.Name = "addSliceButton";
            this.addSliceButton.Size = new System.Drawing.Size(91, 23);
            this.addSliceButton.TabIndex = 7;
            this.addSliceButton.Text = "Новый слой";
            this.addSliceButton.UseVisualStyleBackColor = true;
            this.addSliceButton.Click += new System.EventHandler(this.addSliceButton_Click);
            // 
            // outLengthNumericUpDown
            // 
            this.outLengthNumericUpDown.Location = new System.Drawing.Point(185, 34);
            this.outLengthNumericUpDown.Name = "outLengthNumericUpDown";
            this.outLengthNumericUpDown.Size = new System.Drawing.Size(47, 20);
            this.outLengthNumericUpDown.TabIndex = 4;
            // 
            // inLengthNumericUpDown
            // 
            this.inLengthNumericUpDown.Location = new System.Drawing.Point(11, 34);
            this.inLengthNumericUpDown.Name = "inLengthNumericUpDown";
            this.inLengthNumericUpDown.Size = new System.Drawing.Size(47, 20);
            this.inLengthNumericUpDown.TabIndex = 3;
            // 
            // shadowSlicesLabel
            // 
            this.shadowSlicesLabel.AutoSize = true;
            this.shadowSlicesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.shadowSlicesLabel.Location = new System.Drawing.Point(7, 95);
            this.shadowSlicesLabel.Name = "shadowSlicesLabel";
            this.shadowSlicesLabel.Size = new System.Drawing.Size(142, 24);
            this.shadowSlicesLabel.TabIndex = 2;
            this.shadowSlicesLabel.Text = "Скрытые слои:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(181, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Длина выхода:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Длина входа:";
            // 
            // tutorTabPage
            // 
            this.tutorTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.tutorTabPage.Location = new System.Drawing.Point(4, 22);
            this.tutorTabPage.Name = "tutorTabPage";
            this.tutorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tutorTabPage.Size = new System.Drawing.Size(357, 378);
            this.tutorTabPage.TabIndex = 1;
            this.tutorTabPage.Text = "Обучение";
            // 
            // classifyTabPage
            // 
            this.classifyTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.classifyTabPage.Location = new System.Drawing.Point(4, 22);
            this.classifyTabPage.Name = "classifyTabPage";
            this.classifyTabPage.Size = new System.Drawing.Size(357, 378);
            this.classifyTabPage.TabIndex = 2;
            this.classifyTabPage.Text = "Классификация";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 428);
            this.Controls.Add(this.mainTabControl);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.mainTabControl.ResumeLayout(false);
            this.archTabPage.ResumeLayout(false);
            this.archTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outLengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inLengthNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage archTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tutorTabPage;
        private System.Windows.Forms.NumericUpDown outLengthNumericUpDown;
        private System.Windows.Forms.NumericUpDown inLengthNumericUpDown;
        private System.Windows.Forms.Label shadowSlicesLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage classifyTabPage;
        private System.Windows.Forms.Button addSliceButton;
        private System.Windows.Forms.Button createNetButton;
        private System.Windows.Forms.CheckBox autosizeCheckBox;
    }
}

