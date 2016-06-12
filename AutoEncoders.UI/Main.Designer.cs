namespace AutoEncoders.UI
{
    partial class Main
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
            this.StartTraining = new System.Windows.Forms.Button();
            this.Progress = new System.Windows.Forms.Label();
            this.Accuracy = new System.Windows.Forms.Label();
            this.OpenNetwork = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TestAccuracy = new System.Windows.Forms.Button();
            this.SaveNetwork = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartTraining
            // 
            this.StartTraining.Location = new System.Drawing.Point(326, 181);
            this.StartTraining.Name = "StartTraining";
            this.StartTraining.Size = new System.Drawing.Size(149, 40);
            this.StartTraining.TabIndex = 1;
            this.StartTraining.Text = "Start Training";
            this.StartTraining.UseVisualStyleBackColor = true;
            this.StartTraining.Click += new System.EventHandler(this.OnStartTraining);
            // 
            // Progress
            // 
            this.Progress.AutoSize = true;
            this.Progress.Location = new System.Drawing.Point(367, 234);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(65, 17);
            this.Progress.TabIndex = 2;
            this.Progress.Text = "Progress";
            // 
            // Accuracy
            // 
            this.Accuracy.AutoSize = true;
            this.Accuracy.Location = new System.Drawing.Point(367, 272);
            this.Accuracy.Name = "Accuracy";
            this.Accuracy.Size = new System.Drawing.Size(66, 17);
            this.Accuracy.TabIndex = 3;
            this.Accuracy.Text = "Accuracy";
            // 
            // OpenNetwork
            // 
            this.OpenNetwork.Location = new System.Drawing.Point(12, 12);
            this.OpenNetwork.Name = "OpenNetwork";
            this.OpenNetwork.Size = new System.Drawing.Size(115, 41);
            this.OpenNetwork.TabIndex = 4;
            this.OpenNetwork.Text = "Open Network";
            this.OpenNetwork.UseVisualStyleBackColor = true;
            this.OpenNetwork.Click += new System.EventHandler(this.OnOpenNetwork);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OnOpenFileDialogFileOk);
            // 
            // TestAccuracy
            // 
            this.TestAccuracy.Location = new System.Drawing.Point(12, 69);
            this.TestAccuracy.Name = "TestAccuracy";
            this.TestAccuracy.Size = new System.Drawing.Size(115, 48);
            this.TestAccuracy.TabIndex = 5;
            this.TestAccuracy.Text = "Test Accuracy";
            this.TestAccuracy.UseVisualStyleBackColor = true;
            this.TestAccuracy.Click += new System.EventHandler(this.OnTestAccuracy);
            // 
            // SaveNetwork
            // 
            this.SaveNetwork.Location = new System.Drawing.Point(142, 12);
            this.SaveNetwork.Name = "SaveNetwork";
            this.SaveNetwork.Size = new System.Drawing.Size(120, 41);
            this.SaveNetwork.TabIndex = 6;
            this.SaveNetwork.Text = "Save Network";
            this.SaveNetwork.UseVisualStyleBackColor = true;
            this.SaveNetwork.Click += new System.EventHandler(this.OnSaveNetwork);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 510);
            this.Controls.Add(this.SaveNetwork);
            this.Controls.Add(this.TestAccuracy);
            this.Controls.Add(this.OpenNetwork);
            this.Controls.Add(this.Accuracy);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.StartTraining);
            this.Name = "Main";
            this.Text = "AutoEncoders";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartTraining;
        private System.Windows.Forms.Label Progress;
        private System.Windows.Forms.Label Accuracy;
        private System.Windows.Forms.Button OpenNetwork;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button TestAccuracy;
        private System.Windows.Forms.Button SaveNetwork;
    }
}

