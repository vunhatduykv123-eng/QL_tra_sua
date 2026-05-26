namespace QuanLyQuanTraSua
{
    partial class UserMenuForm
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
            this.dgvUserMenu = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUserMenu
            // 
            this.dgvUserMenu.AllowUserToAddRows = false;
            this.dgvUserMenu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUserMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserMenu.Location = new System.Drawing.Point(-4, 3);
            this.dgvUserMenu.Name = "dgvUserMenu";
            this.dgvUserMenu.ReadOnly = true;
            this.dgvUserMenu.RowHeadersWidth = 51;
            this.dgvUserMenu.RowTemplate.Height = 24;
            this.dgvUserMenu.Size = new System.Drawing.Size(810, 448);
            this.dgvUserMenu.TabIndex = 0;
            this.dgvUserMenu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserMenu_CellContentClick);
            // 
            // UserMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvUserMenu);
            this.Name = "UserMenuForm";
            this.Text = "UserMenuForm";
            this.Load += new System.EventHandler(this.UserMenuForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUserMenu;
    }
}