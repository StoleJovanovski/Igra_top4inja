﻿namespace _2D_Grafika.Ureducaci
{
    partial class Slozen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonDodadi = new System.Windows.Forms.Button();
            this.buttonIzbrisi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(119, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(107, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Број на елементи :";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(15, 40);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(211, 186);
            this.listBox1.TabIndex = 2;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // buttonDodadi
            // 
            this.buttonDodadi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDodadi.Location = new System.Drawing.Point(15, 232);
            this.buttonDodadi.Name = "buttonDodadi";
            this.buttonDodadi.Size = new System.Drawing.Size(98, 23);
            this.buttonDodadi.TabIndex = 3;
            this.buttonDodadi.Text = "Додади";
            this.buttonDodadi.UseVisualStyleBackColor = true;
            this.buttonDodadi.Click += new System.EventHandler(this.Dodadi_Click);
            // 
            // buttonIzbrisi
            // 
            this.buttonIzbrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIzbrisi.Location = new System.Drawing.Point(129, 232);
            this.buttonIzbrisi.Name = "buttonIzbrisi";
            this.buttonIzbrisi.Size = new System.Drawing.Size(97, 23);
            this.buttonIzbrisi.TabIndex = 4;
            this.buttonIzbrisi.Text = "Избриши";
            this.buttonIzbrisi.UseVisualStyleBackColor = true;
            this.buttonIzbrisi.Click += new System.EventHandler(this.Izbrisi_Click);
            // 
            // UreduvacSlozen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 266);
            this.Controls.Add(this.buttonIzbrisi);
            this.Controls.Add(this.buttonDodadi);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "Slozen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonDodadi;
        private System.Windows.Forms.Button buttonIzbrisi;
    }
}