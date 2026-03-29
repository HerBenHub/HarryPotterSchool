namespace HPGUI
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
            this.lvCharacters = new System.Windows.Forms.ListView();
            this.pbCharacterImage = new System.Windows.Forms.PictureBox();
            this.lbKnownSpells = new System.Windows.Forms.ListBox();
            this.lbChildren = new System.Windows.Forms.ListBox();
            this.btnLoadBooks = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCharacterImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lvCharacters
            // 
            this.lvCharacters.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvCharacters.HideSelection = false;
            this.lvCharacters.Location = new System.Drawing.Point(0, 0);
            this.lvCharacters.Name = "lvCharacters";
            this.lvCharacters.Size = new System.Drawing.Size(501, 637);
            this.lvCharacters.TabIndex = 0;
            this.lvCharacters.UseCompatibleStateImageBehavior = false;
            // 
            // pbCharacterImage
            // 
            this.pbCharacterImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCharacterImage.Location = new System.Drawing.Point(547, 12);
            this.pbCharacterImage.Name = "pbCharacterImage";
            this.pbCharacterImage.Size = new System.Drawing.Size(200, 200);
            this.pbCharacterImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCharacterImage.TabIndex = 1;
            this.pbCharacterImage.TabStop = false;
            // 
            // lbKnownSpells
            // 
            this.lbKnownSpells.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbKnownSpells.FormattingEnabled = true;
            this.lbKnownSpells.Location = new System.Drawing.Point(547, 218);
            this.lbKnownSpells.Name = "lbKnownSpells";
            this.lbKnownSpells.Size = new System.Drawing.Size(200, 134);
            this.lbKnownSpells.TabIndex = 2;
            // 
            // lbChildren
            // 
            this.lbChildren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbChildren.FormattingEnabled = true;
            this.lbChildren.Location = new System.Drawing.Point(547, 358);
            this.lbChildren.Name = "lbChildren";
            this.lbChildren.Size = new System.Drawing.Size(200, 134);
            this.lbChildren.TabIndex = 3;
            // 
            // btnLoadBooks
            // 
            this.btnLoadBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadBooks.Location = new System.Drawing.Point(547, 525);
            this.btnLoadBooks.Name = "btnLoadBooks";
            this.btnLoadBooks.Size = new System.Drawing.Size(200, 47);
            this.btnLoadBooks.TabIndex = 4;
            this.btnLoadBooks.Text = "Könyvek";
            this.btnLoadBooks.UseVisualStyleBackColor = true;
            this.btnLoadBooks.Click += new System.EventHandler(this.btnLoadBooks_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(547, 578);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(200, 47);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Frissítés";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 637);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnLoadBooks);
            this.Controls.Add(this.lbChildren);
            this.Controls.Add(this.lbKnownSpells);
            this.Controls.Add(this.pbCharacterImage);
            this.Controls.Add(this.lvCharacters);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCharacterImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvCharacters;
        private System.Windows.Forms.PictureBox pbCharacterImage;
        private System.Windows.Forms.ListBox lbKnownSpells;
        private System.Windows.Forms.ListBox lbChildren;
        private System.Windows.Forms.Button btnLoadBooks;
        private System.Windows.Forms.Button btnRefresh;
    }
}

