namespace Task8.WF
{
    partial class CurvesWF
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurvesWF));
            this.CurvesList = new System.Windows.Forms.ComboBox();
            this.BuildFunctionButton = new System.Windows.Forms.Button();
            this.AvailableFunctionLabel = new System.Windows.Forms.Label();
            this.ScalingLabel = new System.Windows.Forms.Label();
            this.ScaleLevelLabel = new System.Windows.Forms.Label();
            this.ScaleUpButton = new System.Windows.Forms.Button();
            this.ScaleDownButton = new System.Windows.Forms.Button();
            this.ScaleDefaultButton = new System.Windows.Forms.Button();
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.EnterParametersLabel = new System.Windows.Forms.Label();
            this.UniversalTextBox = new System.Windows.Forms.TextBox();
            this.ParameterLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CurvesList
            // 
            this.CurvesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurvesList.FormattingEnabled = true;
            this.CurvesList.Location = new System.Drawing.Point(8, 35);
            this.CurvesList.Name = "CurvesList";
            this.CurvesList.Size = new System.Drawing.Size(224, 24);
            this.CurvesList.TabIndex = 0;
            // 
            // BuildFunctionButton
            // 
            this.BuildFunctionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BuildFunctionButton.Location = new System.Drawing.Point(12, 494);
            this.BuildFunctionButton.Name = "BuildFunctionButton";
            this.BuildFunctionButton.Size = new System.Drawing.Size(982, 23);
            this.BuildFunctionButton.TabIndex = 2;
            this.BuildFunctionButton.Text = "Build function";
            this.BuildFunctionButton.UseVisualStyleBackColor = true;
            this.BuildFunctionButton.Click += new System.EventHandler(this.BuildFunctionButton_Click);
            // 
            // AvailableFunctionLabel
            // 
            this.AvailableFunctionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AvailableFunctionLabel.Location = new System.Drawing.Point(4, 9);
            this.AvailableFunctionLabel.Name = "AvailableFunctionLabel";
            this.AvailableFunctionLabel.Size = new System.Drawing.Size(228, 23);
            this.AvailableFunctionLabel.TabIndex = 3;
            this.AvailableFunctionLabel.Text = "Available functions";
            // 
            // ScalingLabel
            // 
            this.ScalingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScalingLabel.Location = new System.Drawing.Point(4, 356);
            this.ScalingLabel.Name = "ScalingLabel";
            this.ScalingLabel.Size = new System.Drawing.Size(224, 23);
            this.ScalingLabel.TabIndex = 4;
            this.ScalingLabel.Text = "Scaling ∈ [0,1; 5,2]";
            // 
            // ScaleLevelLabel
            // 
            this.ScaleLevelLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ScaleLevelLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ScaleLevelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScaleLevelLabel.Location = new System.Drawing.Point(8, 382);
            this.ScaleLevelLabel.Name = "ScaleLevelLabel";
            this.ScaleLevelLabel.Size = new System.Drawing.Size(100, 100);
            this.ScaleLevelLabel.TabIndex = 5;
            this.ScaleLevelLabel.Text = "1";
            this.ScaleLevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScaleUpButton
            // 
            this.ScaleUpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScaleUpButton.Location = new System.Drawing.Point(114, 418);
            this.ScaleUpButton.Name = "ScaleUpButton";
            this.ScaleUpButton.Size = new System.Drawing.Size(118, 30);
            this.ScaleUpButton.TabIndex = 6;
            this.ScaleUpButton.Text = "+0,1";
            this.ScaleUpButton.UseVisualStyleBackColor = true;
            this.ScaleUpButton.Click += new System.EventHandler(this.ScaleButton_Click);
            // 
            // ScaleDownButton
            // 
            this.ScaleDownButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScaleDownButton.Location = new System.Drawing.Point(114, 452);
            this.ScaleDownButton.Name = "ScaleDownButton";
            this.ScaleDownButton.Size = new System.Drawing.Size(118, 30);
            this.ScaleDownButton.TabIndex = 7;
            this.ScaleDownButton.Text = "-0,1";
            this.ScaleDownButton.UseVisualStyleBackColor = true;
            this.ScaleDownButton.Click += new System.EventHandler(this.ScaleButton_Click);
            // 
            // ScaleDefaultButton
            // 
            this.ScaleDefaultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScaleDefaultButton.Location = new System.Drawing.Point(114, 382);
            this.ScaleDefaultButton.Name = "ScaleDefaultButton";
            this.ScaleDefaultButton.Size = new System.Drawing.Size(118, 30);
            this.ScaleDefaultButton.TabIndex = 8;
            this.ScaleDefaultButton.Text = "Default";
            this.ScaleDefaultButton.UseVisualStyleBackColor = true;
            this.ScaleDefaultButton.Click += new System.EventHandler(this.ScaleDefaultButton_Click);
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ImagePictureBox.Location = new System.Drawing.Point(242, 12);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(752, 470);
            this.ImagePictureBox.TabIndex = 9;
            this.ImagePictureBox.TabStop = false;
            // 
            // EnterParametersLabel
            // 
            this.EnterParametersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EnterParametersLabel.Location = new System.Drawing.Point(4, 157);
            this.EnterParametersLabel.Name = "EnterParametersLabel";
            this.EnterParametersLabel.Size = new System.Drawing.Size(224, 23);
            this.EnterParametersLabel.TabIndex = 10;
            this.EnterParametersLabel.Text = "Enter parameters:";
            this.EnterParametersLabel.Visible = false;
            // 
            // UniversalTextBox
            // 
            this.UniversalTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UniversalTextBox.Location = new System.Drawing.Point(8, 326);
            this.UniversalTextBox.Name = "UniversalTextBox";
            this.UniversalTextBox.Size = new System.Drawing.Size(224, 27);
            this.UniversalTextBox.TabIndex = 11;
            this.UniversalTextBox.Visible = false;
            // 
            // ParameterLabel
            // 
            this.ParameterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ParameterLabel.Location = new System.Drawing.Point(5, 180);
            this.ParameterLabel.Name = "ParameterLabel";
            this.ParameterLabel.Size = new System.Drawing.Size(224, 143);
            this.ParameterLabel.TabIndex = 12;
            this.ParameterLabel.Visible = false;
            // 
            // CurvesWF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 529);
            this.Controls.Add(this.ParameterLabel);
            this.Controls.Add(this.UniversalTextBox);
            this.Controls.Add(this.EnterParametersLabel);
            this.Controls.Add(this.ImagePictureBox);
            this.Controls.Add(this.ScaleDefaultButton);
            this.Controls.Add(this.ScaleDownButton);
            this.Controls.Add(this.ScaleUpButton);
            this.Controls.Add(this.ScaleLevelLabel);
            this.Controls.Add(this.ScalingLabel);
            this.Controls.Add(this.AvailableFunctionLabel);
            this.Controls.Add(this.BuildFunctionButton);
            this.Controls.Add(this.CurvesList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1024, 576);
            this.MinimumSize = new System.Drawing.Size(1024, 576);
            this.Name = "CurvesWF";
            this.Text = "CurveVisio";
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CurvesList;
        private System.Windows.Forms.Button BuildFunctionButton;
        private System.Windows.Forms.Label AvailableFunctionLabel;
        private System.Windows.Forms.Label ScalingLabel;
        private System.Windows.Forms.Label ScaleLevelLabel;
        private System.Windows.Forms.Button ScaleUpButton;
        private System.Windows.Forms.Button ScaleDownButton;
        private System.Windows.Forms.Button ScaleDefaultButton;
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.Label EnterParametersLabel;
        private System.Windows.Forms.TextBox UniversalTextBox;
        private System.Windows.Forms.Label ParameterLabel;
    }
}

