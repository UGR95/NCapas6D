namespace ProyectoFinal_U1_2
{
    partial class frmReporteAutores
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
            this.reportVAutores = new Telerik.ReportViewer.WinForms.ReportViewer();
            this.groupBoxFiltros = new System.Windows.Forms.GroupBox();
            this.chkContratoAct = new System.Windows.Forms.CheckBox();
            this.groupBoxFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportVAutores
            // 
            this.reportVAutores.AccessibilityKeyMap = null;
            this.reportVAutores.BackColor = System.Drawing.Color.SeaShell;
            this.reportVAutores.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.reportVAutores.Location = new System.Drawing.Point(0, 147);
            this.reportVAutores.Name = "reportVAutores";
            this.reportVAutores.Size = new System.Drawing.Size(800, 325);
            this.reportVAutores.TabIndex = 0;
            this.reportVAutores.Load += new System.EventHandler(this.reportVAutores_Load);
            // 
            // groupBoxFiltros
            // 
            this.groupBoxFiltros.Controls.Add(this.chkContratoAct);
            this.groupBoxFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFiltros.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFiltros.Name = "groupBoxFiltros";
            this.groupBoxFiltros.Size = new System.Drawing.Size(800, 100);
            this.groupBoxFiltros.TabIndex = 1;
            this.groupBoxFiltros.TabStop = false;
            this.groupBoxFiltros.Text = "Foltros";
            // 
            // chkContratoAct
            // 
            this.chkContratoAct.AutoSize = true;
            this.chkContratoAct.Location = new System.Drawing.Point(12, 31);
            this.chkContratoAct.Name = "chkContratoAct";
            this.chkContratoAct.Size = new System.Drawing.Size(66, 17);
            this.chkContratoAct.TabIndex = 0;
            this.chkContratoAct.Text = "Contrato";
            this.chkContratoAct.UseVisualStyleBackColor = true;
            this.chkContratoAct.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmReporteAutores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(800, 472);
            this.Controls.Add(this.groupBoxFiltros);
            this.Controls.Add(this.reportVAutores);
            this.Name = "frmReporteAutores";
            this.Text = "frmReporteAutores";
            this.Load += new System.EventHandler(this.frmReporteAutores_Load);
            this.groupBoxFiltros.ResumeLayout(false);
            this.groupBoxFiltros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.ReportViewer.WinForms.ReportViewer reportVAutores;
        private System.Windows.Forms.GroupBox groupBoxFiltros;
        private System.Windows.Forms.CheckBox chkContratoAct;
    }
}