namespace MapEditor
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.CaseType = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Put = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.PosY = new System.Windows.Forms.ComboBox();
            this.PosX = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CaseType
            // 
            this.CaseType.FormattingEnabled = true;
            this.CaseType.Items.AddRange(new object[] {
            "Terre",
            "Montagne",
            "Eau"});
            this.CaseType.Location = new System.Drawing.Point(12, 41);
            this.CaseType.Name = "CaseType";
            this.CaseType.Size = new System.Drawing.Size(154, 21);
            this.CaseType.TabIndex = 1;
            this.CaseType.SelectedIndexChanged += new System.EventHandler(this.CaseType_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(153, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Case Type";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 68);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(153, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "X Position";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(13, 121);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(153, 20);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "Y Position";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Put
            // 
            this.Put.Location = new System.Drawing.Point(13, 174);
            this.Put.Name = "Put";
            this.Put.Size = new System.Drawing.Size(153, 57);
            this.Put.TabIndex = 7;
            this.Put.Text = "Put";
            this.Put.UseVisualStyleBackColor = true;
            this.Put.Click += new System.EventHandler(this.Put_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(13, 374);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(153, 73);
            this.Save.TabIndex = 8;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // PosY
            // 
            this.PosY.FormattingEnabled = true;
            this.PosY.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.PosY.Location = new System.Drawing.Point(13, 147);
            this.PosY.Name = "PosY";
            this.PosY.Size = new System.Drawing.Size(154, 21);
            this.PosY.TabIndex = 6;
            this.PosY.SelectedIndexChanged += new System.EventHandler(this.PosY_SelectedIndexChanged);
            // 
            // PosX
            // 
            this.PosX.FormattingEnabled = true;
            this.PosX.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.PosX.Location = new System.Drawing.Point(12, 94);
            this.PosX.Name = "PosX";
            this.PosX.Size = new System.Drawing.Size(154, 21);
            this.PosX.TabIndex = 4;
            this.PosX.SelectedIndexChanged += new System.EventHandler(this.PosX_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 825);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Put);
            this.Controls.Add(this.PosY);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.PosX);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CaseType);
            this.Name = "Form1";
            this.Text = "Map Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CaseType;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button Put;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.ComboBox PosY;
        private System.Windows.Forms.ComboBox PosX;
    }
}

