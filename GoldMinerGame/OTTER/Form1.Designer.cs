namespace OTTER
{
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
            this.textBoxIme = new System.Windows.Forms.TextBox();
            this.labelIme = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelUpozorenje = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxIme
            // 
            this.textBoxIme.Location = new System.Drawing.Point(200, 54);
            this.textBoxIme.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxIme.Name = "textBoxIme";
            this.textBoxIme.Size = new System.Drawing.Size(259, 22);
            this.textBoxIme.TabIndex = 0;
            // 
            // labelIme
            // 
            this.labelIme.AutoSize = true;
            this.labelIme.Font = new System.Drawing.Font("Snap ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIme.Location = new System.Drawing.Point(53, 50);
            this.labelIme.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIme.Name = "labelIme";
            this.labelIme.Size = new System.Drawing.Size(129, 27);
            this.labelIme.TabIndex = 1;
            this.labelIme.Text = "Tvoje ime:";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Snap ITC", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(623, 46);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(209, 71);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelUpozorenje
            // 
            this.labelUpozorenje.AutoSize = true;
            this.labelUpozorenje.Font = new System.Drawing.Font("Snap ITC", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpozorenje.Location = new System.Drawing.Point(224, 96);
            this.labelUpozorenje.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUpozorenje.Name = "labelUpozorenje";
            this.labelUpozorenje.Size = new System.Drawing.Size(0, 23);
            this.labelUpozorenje.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Snap ITC", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(623, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 71);
            this.button1.TabIndex = 4;
            this.button1.Text = "Izlaz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Snap ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(48, 142);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 47);
            this.button2.TabIndex = 5;
            this.button2.Text = "Upute";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OTTER.Properties.Resources.Start;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(928, 518);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelUpozorenje);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelIme);
            this.Controls.Add(this.textBoxIme);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelIme;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelUpozorenje;
        private System.Windows.Forms.TextBox textBoxIme;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}