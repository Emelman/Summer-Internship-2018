namespace Tanks.VL.ViewLayer
{
    partial class GameStatsBoard
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
            this.gameObjects = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjects)).BeginInit();
            this.SuspendLayout();
            // 
            // gameObjects
            // 
            this.gameObjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gameObjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ObjectName,
            this.Position,
            this.Direction});
            this.gameObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameObjects.Location = new System.Drawing.Point(0, 0);
            this.gameObjects.Name = "gameObjects";
            this.gameObjects.RowHeadersVisible = false;
            this.gameObjects.Size = new System.Drawing.Size(303, 338);
            this.gameObjects.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "GetId";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // ObjectName
            // 
            this.ObjectName.DataPropertyName = "ObjectName";
            this.ObjectName.HeaderText = "Name";
            this.ObjectName.Name = "ObjectName";
            // 
            // Position
            // 
            this.Position.DataPropertyName = "Position";
            this.Position.HeaderText = "X/Ypos";
            this.Position.Name = "Position";
            // 
            // Direction
            // 
            this.Direction.DataPropertyName = "Direction";
            this.Direction.HeaderText = "Direction";
            this.Direction.Name = "Direction";
            // 
            // GameStatsBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 338);
            this.Controls.Add(this.gameObjects);
            this.Name = "GameStatsBoard";
            this.Text = "GameStatsBoard";
            ((System.ComponentModel.ISupportInitialize)(this.gameObjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gameObjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direction;
    }
}