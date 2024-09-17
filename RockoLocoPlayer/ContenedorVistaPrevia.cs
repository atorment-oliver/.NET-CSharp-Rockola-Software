using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockoLocoPlayer
{
    public partial class ContenedorVistaPrevia : Form
    {
        const int velocidad = 300;
        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X1;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_BLEND = 0X80000;
        const int AW_HIDE = 0X10000;
        const int AW_ACTIVATE = 0X20000;
        public bool previewFixed = false;
        public ContenedorVistaPrevia()
        {
            InitializeComponent();
        }
        public bool PreviewFixed
        {
            get
            {
                //logic here 
                return previewFixed;
            }
            set
            {
                //logic here
                previewFixed = value;
            }
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect, // x-coordinate of upper-left corner
           int nTopRect, // y-coordinate of upper-left corner
           int nRightRect, // x-coordinate of lower-right corner
           int nBottomRect, // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );
        Dictionary<string, string> listBox3Dict = new Dictionary<string, string>();
        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);
        private void ContenedorVistaPrevia_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width -10, Height-10, 10, 10));
            int WidthOfMain = Application.OpenForms["MainForm"].Width;
            int HeightofMain = Application.OpenForms["MainForm"].Height;
            int LocationMainX = Application.OpenForms["MainForm"].Location.X;
            int locationMainy = Application.OpenForms["MainForm"].Location.Y;
            //Set the Location
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(WidthOfMain - this.Width-250, HeightofMain/17);
        }
        private void VistaPrevia_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
        }
        public void ShowScrenn()
        {
            AnimateWindow(this.Handle, velocidad, AW_SLIDE | AW_HOR_POSITIVE);
            this.Show();
        }
        public void HideScreen()
        {
            if (!previewFixed)
            {
                AnimateWindow(this.Handle, velocidad, AW_HIDE | AW_HOR_NEGATIVE);
                this.Hide();
            }
        }
        private void ShutOffTimer_Tick(object sender, EventArgs e)
        {
            VistaPrevia.Ctlcontrols.stop();
            ((Timer)sender).Stop();
            ((Timer)sender).Dispose();
            HideScreen();
        }

        private void VistaPrevia_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if ((WMPLib.WMPOpenState)e.newState == WMPLib.WMPOpenState.wmposMediaOpen)
            {
                InitCounterClock();
            }
        }
        public void InitCounterClock()
        {
            ShutOffTimer.Stop();
            ShutOffTimer.Dispose();
            ShutOffTimer = new Timer();
            VistaPrevia.Ctlcontrols.currentPosition = 0;
            ShutOffTimer.Interval = 20000;
            ShutOffTimer.Tick += ShutOffTimer_Tick;
            ShutOffTimer.Start();
        }
    }
}
