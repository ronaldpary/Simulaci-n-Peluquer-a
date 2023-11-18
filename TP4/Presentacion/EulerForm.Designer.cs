namespace TP4.Presentacion
{
    partial class EulerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvEuler = new System.Windows.Forms.DataGridView();
            this.col_t = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.t_siguiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y_siguiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEuler)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEuler
            // 
            this.dgvEuler.AllowUserToAddRows = false;
            this.dgvEuler.AllowUserToDeleteRows = false;
            this.dgvEuler.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEuler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEuler.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEuler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEuler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_t,
            this.col_d,
            this.coldF,
            this.t_siguiente,
            this.y_siguiente});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEuler.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEuler.Location = new System.Drawing.Point(0, 0);
            this.dgvEuler.Name = "dgvEuler";
            this.dgvEuler.ReadOnly = true;
            this.dgvEuler.RowHeadersWidth = 62;
            this.dgvEuler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEuler.Size = new System.Drawing.Size(800, 450);
            this.dgvEuler.TabIndex = 5;
            // 
            // col_t
            // 
            this.col_t.HeaderText = "t";
            this.col_t.MinimumWidth = 8;
            this.col_t.Name = "col_t";
            this.col_t.ReadOnly = true;
            // 
            // col_d
            // 
            this.col_d.HeaderText = "D";
            this.col_d.MinimumWidth = 8;
            this.col_d.Name = "col_d";
            this.col_d.ReadOnly = true;
            // 
            // coldF
            // 
            this.coldF.HeaderText = "dD/dt";
            this.coldF.Name = "coldF";
            this.coldF.ReadOnly = true;
            // 
            // t_siguiente
            // 
            this.t_siguiente.HeaderText = "t(i+1)";
            this.t_siguiente.MinimumWidth = 8;
            this.t_siguiente.Name = "t_siguiente";
            this.t_siguiente.ReadOnly = true;
            // 
            // y_siguiente
            // 
            this.y_siguiente.HeaderText = "Y(i+1)";
            this.y_siguiente.MinimumWidth = 8;
            this.y_siguiente.Name = "y_siguiente";
            this.y_siguiente.ReadOnly = true;
            // 
            // EulerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvEuler);
            this.Name = "EulerForm";
            this.Text = "Euler";
            this.Load += new System.EventHandler(this.Euler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEuler)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEuler;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_t;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_d;
        private System.Windows.Forms.DataGridViewTextBoxColumn coldF;
        private System.Windows.Forms.DataGridViewTextBoxColumn t_siguiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn y_siguiente;
    }
}