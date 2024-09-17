namespace RockoLocoPlayer
{
    partial class ContenedorVistaPrevia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContenedorVistaPrevia));
            this.VistaPrevia = new AxWMPLib.AxWindowsMediaPlayer();
            this.ShutOffTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.VistaPrevia)).BeginInit();
            this.SuspendLayout();
            // 
            // VistaPrevia
            // 
            this.VistaPrevia.Enabled = true;
            this.VistaPrevia.Location = new System.Drawing.Point(-41, 1);
            this.VistaPrevia.Name = "VistaPrevia";
            this.VistaPrevia.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("VistaPrevia.OcxState")));
            this.VistaPrevia.Size = new System.Drawing.Size(367, 237);
            this.VistaPrevia.TabIndex = 5;
            this.VistaPrevia.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(this.VistaPrevia_OpenStateChange);
            this.VistaPrevia.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.VistaPrevia_PlayStateChange);
            // 
            // ShutOffTimer
            // 
            this.ShutOffTimer.Tick += new System.EventHandler(this.ShutOffTimer_Tick);
            // 
            // ContenedorVistaPrevia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 237);
            this.Controls.Add(this.VistaPrevia);
            this.Name = "ContenedorVistaPrevia";
            this.Text = "ContenedorVistaPrevia";
            this.Load += new System.EventHandler(this.ContenedorVistaPrevia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VistaPrevia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxWMPLib.AxWindowsMediaPlayer VistaPrevia;
        private System.Windows.Forms.Timer ShutOffTimer;
    }
}