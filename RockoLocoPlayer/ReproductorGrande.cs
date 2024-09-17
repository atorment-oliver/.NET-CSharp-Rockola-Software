using DevExpress.XtraGrid.Views.Grid;
using RockoLocoPlayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace WindowsFormsApplication1
{
    public partial class ReproductorGrande : Form
    {
        string sDireccionCanciones = System.Configuration.ConfigurationSettings.AppSettings["Direccion"];
        public string sDireccionImagenes = System.Configuration.ConfigurationSettings.AppSettings["DireccionImagenes"] + "\\" + System.Configuration.ConfigurationSettings.AppSettings["Template"];
        string sExtensiones = System.Configuration.ConfigurationSettings.AppSettings["Extensiones"];
        string sExtensionesImagenes = System.Configuration.ConfigurationSettings.AppSettings["ExtensionesImages"];
        string sListaCanciones = System.Configuration.ConfigurationSettings.AppSettings["ListaCanciones"];
        string sListaEspera = System.Configuration.ConfigurationSettings.AppSettings["ListEspera"];
        public int iCantidadCanciones = 0;
        WMPLib.IWMPPlaylist listImages;
        MainForm objFormPrincipal;
        SmartRocko.CoreRocko obj = new SmartRocko.CoreRocko();
        WMPLib.IWMPPlaylist playlist;
        //public SmartRocko.CoreRocko ObjetoRocko
        //{
        //    get
        //    {
        //        //logic here 
        //        return obj;
        //    }
        //    set
        //    {
        //        //logic here
        //        obj = value;
        //    }
        //}
        //public WMPLib.IWMPPlaylist Playlist
        //{
        //    get
        //    {
        //        //logic here 
        //        return playlist;
        //    }
        //    set
        //    {
        //        //logic here
        //        playlist = value;
        //    }
        //}
        public ReproductorGrande()
        {
            InitializeComponent();
            //this.Icon = RockoTouch.Resources.logo_touch__1_;
        }

        private void ReproductorGrande_Load(object sender, EventArgs e)
        {
            int WidthOfMain = Application.OpenForms["MainForm"].Width;
            int HeightofMain = Application.OpenForms["MainForm"].Height;
            int LocationMainX = Application.OpenForms["MainForm"].Location.X;
            int locationMainy = Application.OpenForms["MainForm"].Location.Y;
            //Set the Location
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(LocationMainX + WidthOfMain, locationMainy + 10);
            //this.Location = new Point(Screen.AllScreens[1].WorkingArea.Location.X + Screen.AllScreens[0].WorkingArea.Width, Screen.AllScreens[1].WorkingArea.Height);
            //showOnScreen();
            Screen[] sc;
            sc = Screen.AllScreens;
            //this.Location = new Point(sc[0].WorkingArea.Width, sc[0].WorkingArea.Y);
            // If you intend the form to be maximized, change it to normal then maximized.
            this.WindowState = FormWindowState.Normal;
            this.WindowState = FormWindowState.Maximized;
            playlist = Reproductor2.newPlaylist("RockoLocoList", string.Empty);

            listImages = Reproductor2.newPlaylist("Lista", string.Empty);
            WMPLib.IWMPMedia media;
            string[] vExtensiones = sExtensionesImagenes.Split(';');
            listImages = Reproductor2.newPlaylist("Lista", string.Empty);
            foreach (string Exten in vExtensiones)
            {
                foreach (string file in Directory.GetFiles(@sDireccionImagenes, Exten))
                {
                    media = Reproductor2.newMedia(@file);
                    listImages.appendItem(media);
                }
            }
            Reproductor2.settings.autoStart = true;
            Reproductor2.settings.volume = 100;
            StartPlayWaitingList();
            objFormPrincipal = ((MainForm)Application.OpenForms["MainForm"]);
            bool bMaximized = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["StartMaximized"]);
            if (bMaximized)
            {
                MaximizeWindow();
                Reproductor2.uiMode = "none";
                //Reproductor2.fullScreen = true;
            }
            ResizeBarraAvanceSegundoReproductor();
        }
        private void MaximizeWindow()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            RockoTouch.WinApi.SetWinFullScreen(this.Handle);
            Reproductor2.stretchToFit = true;
            //RockoTouch.Taskbar.Hide();
        }
        void showOnScreen()
        {
            Screen[] screens = Screen.AllScreens;
        }

        private void Reproductor2_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                PlaySong();
            }
            if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (iCantidadCanciones > 0)
                {
                    //objFormPrincipal.VistaPrevia.settings.volume = 0;
                    objFormPrincipal.pbarMedia.Properties.Minimum = 0;
                    objFormPrincipal.pbarMedia.Properties.Step = 0;
                    objFormPrincipal.pbarMedia.Properties.Maximum = (int)Reproductor2.Ctlcontrols.currentItem.duration;
                    pbarMedia.Properties.Minimum = 0;
                    pbarMedia.Properties.Step = 0;
                    pbarMedia.Properties.Maximum = (int)Reproductor2.Ctlcontrols.currentItem.duration;
                    timer2.Start();
                }
            }
            else if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer2.Stop();
                objFormPrincipal.pbarMedia.Properties.Step = 0;
                pbarMedia.Properties.Step = 0;
                objFormPrincipal.lblPercent.Text = "0 %";
                objFormPrincipal.VistaPrevia.settings.volume = 5;
            }
            else if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsStopped)
            {
                if (iCantidadCanciones > 0)
                {
                    timer2.Stop();
                    objFormPrincipal.pbarMedia.Properties.Step = 0;
                    pbarMedia.Properties.Step = 0;
                    objFormPrincipal.lblPercent.Text = "0 %";
                    objFormPrincipal.VistaPrevia.settings.volume = 5;
                }
            }
            else if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsUndefined)
            {
                //if (objFormPrincipal != null)
                //{
                //    if (objFormPrincipal.VistaPrevia.playState == WMPPlayState.wmppsPlaying)
                //        Reproductor2.settings.volume = 0;
                //    else
                //        Reproductor2.settings.volume = 10;
                //}
                if (iCantidadCanciones == 0)
                {
                    Reproductor2.settings.volume = 10;
                    LoadImagesList();
                    Reproductor2.Ctlcontrols.play();
                }
            }
        }
        void PlaySong()
        {
            if (iCantidadCanciones > 0)
            {
                obj.DeleteFromWaitList(@sListaEspera, Reproductor2.currentMedia.sourceURL);
                playlist = Reproductor2.newPlaylist("RockoLocoList", string.Empty);
                timer1.Interval = 100;
                timer1.Start();
                timer1.Enabled = true;
                timer1.Tick += timer1_Tick;
            }
            else
            {
                //listImages = Reproductor2.newPlaylist("Lista", string.Empty);
                //timer1.Interval = 100;
                //timer1.Start();
                //timer1.Enabled = true;
                //timer1.Tick += timer1_Tick;
            }
        }
        public void timer1_Tick(object sender, EventArgs e)
        {
            StartPlayWaitingList();
            timer1.Enabled = false;
        }
        public void StartPlayWaitingList()
        {
            try
            {
                if (Reproductor2.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    
                }
                else
                {
                    DataTable dtResultado = obj.ReadList(@sListaEspera);
                    iCantidadCanciones = 0;
                    if (dtResultado != null)
                    {
                        iCantidadCanciones = dtResultado.Rows.Count;
                        foreach (DataRow row in dtResultado.Rows)
                        {
                            if (dtResultado.Rows.Count > playlist.count)
                            {
                                string s = row["Direccion"].ToString();
                                WMPLib.IWMPMedia temp = Reproductor2.newMedia(s); //Load media from URL. 
                                playlist.appendItem(temp); //Add song to playlist.
                            }
                        }
                        this.Reproductor2.currentPlaylist = playlist;
                        this.Reproductor2.settings.autoStart = true;
                    }
                    else
                    {
                        Reproductor2.newPlaylist("RockoLocoList", string.Empty);
                        this.Reproductor2.currentPlaylist = playlist;
                        lblNombreCancion.Text = string.Empty;
                    }
                }
                LoadWaitingList();
            }
            catch (Exception err)
            {
                LoadImagesList();
            }
        }
        public void LoadNameSongInLabelOnBigPlayer(DataTable dtResultado)
        {
            if (dtResultado != null)
            {
                lblNombreCancion.Text = dtResultado.Rows[0]["NombreCancion"].ToString();
            }
        }
        public void LoadWaitingList()
        {
            DataTable dtCanciones = obj.ReadListWaitingList(@sListaEspera);
            if (dtCanciones != null)
            {
                objFormPrincipal.gListaEspera.DataSource = dtCanciones;
                LoadNameSongInLabelOnBigPlayer(dtCanciones);
                objFormPrincipal.grpListaEspera.Text = "LISTA DE ESPERA - TOTAL: " + dtCanciones.Rows.Count.ToString();
                objFormPrincipal.lblCreditos.Text = dtCanciones.Rows.Count.ToString();
                lblCreditos.Text = dtCanciones.Rows.Count.ToString();
            }
            else
            {
                if(objFormPrincipal != null)
                    objFormPrincipal.gListaEspera.DataSource = null;
                objFormPrincipal.grpListaEspera.Text = "LISTA DE ESPERA - TOTAL: 0";
                objFormPrincipal.lblCreditos.Text = "0";
                lblCreditos.Text = "0";
                objFormPrincipal.lblPercent.Text = "0";
                objFormPrincipal.pbarMedia.EditValue = 0;
                objFormPrincipal.pbarMedia.Update();
                pbarMedia.EditValue = 0;
                pbarMedia.Update();
                LoadImagesList();
            }
        }
        public void LoadImagesList()
        {
            if (iCantidadCanciones == 0 && Reproductor2.playState != WMPLib.WMPPlayState.wmppsPlaying)
            {
                pbarMedia.Visible = false;
                listImages = Reproductor2.newPlaylist("Lista", string.Empty);
                WMPLib.IWMPMedia media;
                string[] vExtensiones = sExtensionesImagenes.Split(';');
                listImages = Reproductor2.newPlaylist("Lista", string.Empty);
                foreach (string Exten in vExtensiones)
                {
                    foreach (string file in Directory.GetFiles(@sDireccionImagenes, Exten))
                    {
                        media = Reproductor2.newMedia(@file);
                        listImages.appendItem(media);
                    }
                }
                Reproductor2.settings.volume = 10;
                if (listImages.count > 0)
                {
                    Reproductor2.currentPlaylist = listImages;
                    Reproductor2.settings.setMode("loop", true);
                    this.Reproductor2.settings.autoStart = true;
                }
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Reproductor2.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (iCantidadCanciones > 0)
                {
                    if (objFormPrincipal.pbarMedia.Properties.Maximum > 0)
                    {
                        pbarMedia.Visible = true;
                        //objFormPrincipal.pbarMedia.Properties.Step = (int)Reproductor2.Ctlcontrols.currentPosition;
                        //objFormPrincipal.lblPercent.Text = (Decimal.Round(Convert.ToDecimal(objFormPrincipal.pbarMedia.Properties.Step) / Convert.ToDecimal(objFormPrincipal.pbarMedia.Properties.Maximum),2) * 100).ToString().Replace(",00","") + " %";
                        decimal dValor = (Decimal.Round(Convert.ToDecimal((int)Reproductor2.Ctlcontrols.currentPosition / Convert.ToDecimal(objFormPrincipal.pbarMedia.Properties.Maximum)), 2) * 100);
                        objFormPrincipal.lblPercent.Text = dValor.ToString().Replace(",00", "").Replace(",0", "") + " %";
                        objFormPrincipal.pbarMedia.EditValue = (int)Reproductor2.Ctlcontrols.currentPosition;
                        pbarMedia.EditValue = (int)Reproductor2.Ctlcontrols.currentPosition;
                        //objFormPrincipal.pbarMedia.Increment(1);
                        //objFormPrincipal.pbarMedia.PerformStep();
                        objFormPrincipal.pbarMedia.Update();
                        pbarMedia.Update();
                    }
                }
                else
                    pbarMedia.Visible = false;
            }
        }

        private void Reproductor2_ErrorEvent(object sender, EventArgs e)
        {
            MessageBox.Show(e.GetHashCode().ToString() + " - " + e.GetType().Name.ToString() + " - " + e.ToString());
        }

        private void Reproductor2_MediaError(object sender, AxWMPLib._WMPOCXEvents_MediaErrorEvent e)
        {
            try
            // If the Player encounters a corrupt or missing file, 
            // show the hexadecimal error code and URL.
            {
                IWMPMedia2 errSource = e.pMediaObject as IWMPMedia2;
                IWMPErrorItem errorItem = errSource.Error;
                MessageBox.Show("Error " + errorItem.errorCode.ToString("X")
                                + " in " + errSource.sourceURL);
            }
            catch (InvalidCastException)
            // In case pMediaObject is not an IWMPMedia item.
            {
                MessageBox.Show("Error.");
            } 
        }
        private void ResizeBarraAvanceSegundoReproductor()
        {
            pbarMedia.Size = new Size(Reproductor2.Width / 3, (Reproductor2.Height) / 12);
            pbarMedia.Location = new Point((this.Width / 2) - (pbarMedia.Width / 2), this.Height - (pbarMedia.Height));
            lblCreditos.Location = new Point(this.Width - 50, this.Height - 50);
            lblNombreCancion.Location = new Point(10, 10);
        }
        private void ReproductorGrande_Resize(object sender, EventArgs e)
        {
            ResizeBarraAvanceSegundoReproductor();
        }

        private void ReproductorGrande_MaximumSizeChanged(object sender, EventArgs e)
        {
            pbarMedia.BringToFront();
        }
    }
}
