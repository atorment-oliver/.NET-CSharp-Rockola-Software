namespace WindowsFormsApplication1
{
    partial class ReproductorGrande
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReproductorGrande));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pbarMedia = new DevExpress.XtraEditors.ProgressBarControl();
            this.Reproductor2 = new AxWMPLib.AxWindowsMediaPlayer();
            this.lblCreditos = new System.Windows.Forms.Label();
            this.lblNombreCancion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbarMedia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reproductor2)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pbarMedia
            // 
            this.pbarMedia.Location = new System.Drawing.Point(187, 411);
            this.pbarMedia.Name = "pbarMedia";
            this.pbarMedia.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.pbarMedia.Properties.ShowTitle = true;
            this.pbarMedia.Size = new System.Drawing.Size(304, 31);
            this.pbarMedia.TabIndex = 23;
            // 
            // Reproductor2
            // 
            this.Reproductor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Reproductor2.Enabled = true;
            this.Reproductor2.Location = new System.Drawing.Point(0, 0);
            this.Reproductor2.Name = "Reproductor2";
            this.Reproductor2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Reproductor2.OcxState")));
            this.Reproductor2.Size = new System.Drawing.Size(736, 488);
            this.Reproductor2.TabIndex = 5;
            this.Reproductor2.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.Reproductor2_PlayStateChange);
            this.Reproductor2.ErrorEvent += new System.EventHandler(this.Reproductor2_ErrorEvent);
            this.Reproductor2.MediaError += new AxWMPLib._WMPOCXEvents_MediaErrorEventHandler(this.Reproductor2_MediaError);
            // 
            // lblCreditos
            // 
            this.lblCreditos.AutoSize = true;
            this.lblCreditos.BackColor = System.Drawing.SystemColors.WindowText;
            this.lblCreditos.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCreditos.Location = new System.Drawing.Point(692, 409);
            this.lblCreditos.Name = "lblCreditos";
            this.lblCreditos.Size = new System.Drawing.Size(43, 46);
            this.lblCreditos.TabIndex = 24;
            this.lblCreditos.Text = "0";
            // 
            // lblNombreCancion
            // 
            this.lblNombreCancion.AutoSize = true;
            this.lblNombreCancion.BackColor = System.Drawing.SystemColors.WindowText;
            this.lblNombreCancion.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCancion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblNombreCancion.Location = new System.Drawing.Point(12, 14);
            this.lblNombreCancion.Name = "lblNombreCancion";
            this.lblNombreCancion.Size = new System.Drawing.Size(0, 31);
            this.lblNombreCancion.TabIndex = 25;
            // 
            // ReproductorGrande
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(736, 488);
            this.Controls.Add(this.lblNombreCancion);
            this.Controls.Add(this.lblCreditos);
            this.Controls.Add(this.pbarMedia);
            this.Controls.Add(this.Reproductor2);
            this.Name = "ReproductorGrande";
            this.Text = "ReproductorGrande";
            this.MaximumSizeChanged += new System.EventHandler(this.ReproductorGrande_MaximumSizeChanged);
            this.Load += new System.EventHandler(this.ReproductorGrande_Load);
            this.Resize += new System.EventHandler(this.ReproductorGrande_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbarMedia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Reproductor2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Timer timer1;
        public AxWMPLib.AxWindowsMediaPlayer Reproductor2;
        private System.Windows.Forms.Timer timer2;
        public DevExpress.XtraEditors.ProgressBarControl pbarMedia;
        public System.Windows.Forms.Label lblCreditos;
        public System.Windows.Forms.Label lblNombreCancion;
    }
}