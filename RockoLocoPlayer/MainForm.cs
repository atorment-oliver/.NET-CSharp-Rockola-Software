using DevExpress.Skins;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using RockoLocoPlayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using WinFormAnimation;
using WMPLib;
using EnCryptDecrypt;
using System.Management;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        string sTemplate = System.Configuration.ConfigurationSettings.AppSettings["Template"];
        bool bComboSkin = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ComboSkin"]);
        int SizeFormPrincipal = 0;
        bool isScrolling = false;
        int iCounter = 3;
        System.Windows.Forms.Timer timer3 = new System.Windows.Forms.Timer() { Interval = 1000 };
        bool PreviewFloatingFixed = false;
        bool TypeKeyboadAlphabetic = true;
        string sDireccionCanciones = System.Configuration.ConfigurationSettings.AppSettings["Direccion"];
        string sExtensiones = System.Configuration.ConfigurationSettings.AppSettings["Extensiones"];
        string sExtensionesImagenes = System.Configuration.ConfigurationSettings.AppSettings["ExtensionesImages"];
        string sListaCanciones = System.Configuration.ConfigurationSettings.AppSettings["ListaCanciones"];
        string sListaEspera = System.Configuration.ConfigurationSettings.AppSettings["ListEspera"];
        bool bTipoListado = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["CancionesConGuion"]);
        bool bMaximized = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["StartMaximized"]);
        bool bResetTopSongs = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ResetTop"]);
        bool bPreview = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["Preview"]);
        bool bMakeToUpper = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ConvertToUpper"]);
        public bool bReadSongsEveryEvent = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ReadSongsEveryEvent"]);
        public bool bShowArtistPicture = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ShowArtistPicture"]);
        public bool bShowBackImages = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ShowBackImages"]);
        public string sDireccionFotosArtistas = System.Configuration.ConfigurationSettings.AppSettings["DireccionFotosArtistas"];
        public int iMinVolumen = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["MinVolumen"]);
        public int iMaxVolumen = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["MaxVolumen"]);
        string sTop20 = "TOP 20 + REPRODUCIDOS";
        string sTodasCanciones = "TODAS LAS CANCIONES";
        string sNuevasAdicionadas = "NUEVAS ADICIONADAS";
        string sHotRanking = "HOT RANKING ULTIMOS ADICIONADOS";
        string sActionCoin = System.Configuration.ConfigurationSettings.AppSettings["ActionCoin"];        
        GridView gGridCanciones;
        const int velocidad = 300;
        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X1;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_BLEND = 0X80000;
        const int AW_HIDE = 0X10000;
        const int AW_ACTIVATE = 0X20000;
        bool EnabledFirstScreen = false;
        bool EnabledFirstScreenPreview = true;
        ReproductorGrande ReproductorSegundaPantalla = new ReproductorGrande();
        //ContenedorVistaPrevia ContenedorVistaPreviaFlotante = new ContenedorVistaPrevia();
        //OnscreenKeyboard KeyboardScrenn = new OnscreenKeyboard();
        //private bool _UseSlideAnimation;
        SmartRocko.CoreRocko obj = new SmartRocko.CoreRocko();
        WMPLib.IWMPPlaylist playlist;
        Dictionary<string, string> listBox3Dict = new Dictionary<string, string>();
        public List<FotosArtistas> lstDireccionFotosArtistas = new List<FotosArtistas>();
        public DataTable dsAllSongs = new DataTable();
        //PANEL ANIMATED
        private int _startLeft = -200;  // start position of the panel
        private int _endLeft = 10;      // end position of the panel
        private int _stepSize = 10;
        public enum TipoListado { Todas = 1, Top20 = 2, HotRanking20 = 3, RecientementeAgregados = 4}
        TipoListado TipoSeleccionado = TipoListado.Todas;
        public static class Util
        {
            public enum Effect { Roll, Slide, Center, Blend }

            public static void Animate(Control ctl, Effect effect, int msec, int angle)
            {
                int flags = effmap[(int)effect];
                if (ctl.Visible) { flags |= 0x10000; angle += 180; }
                else
                {
                    if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                    else if (effect == Effect.Blend) throw new ArgumentException();
                }
                flags |= dirmap[(angle % 360) / 45];
                bool ok = AnimateWindow(ctl.Handle, msec, flags);
                if (!ok) throw new Exception("Animation failed");
                ctl.Visible = !ctl.Visible;
            }

            private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
            private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

            [DllImport("user32.dll")]
            private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);
        }
        public sealed class FotosArtistas
        {
            public string sDireccion { get; set; }
            public string sNombre { get; set; }
            public FotosArtistas(string Direccion, string Nombre)
            {
                sDireccion = Direccion;
                sNombre = Nombre;
            }
        }
        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);
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
        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(System.IntPtr hObject);
        public MainForm()
        {
            InitializeComponent();
            wmpLogo.enableContextMenu = false;
            VistaPrevia.enableContextMenu = false;
            cbSkins.Visible = bComboSkin;
            this.Icon = RockoTouch.Resources.logo_touch__2_;
            if (Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ClickEffect"]))
                clickEffect1.ClickControl = this;
            else
                clickEffect1.ClickControl = lblCreditos;
            GetLogin();
            if (!bReadSongsEveryEvent)
                dsAllSongs = obj.ReadList(sListaCanciones);
        }
        public static void GetLogin()
        {
            try
            {
                string cpuInfo = string.Empty;
                ManagementClass mc = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
                string drive = "C";
                ManagementObject dsk = new ManagementObject(
                    @"win32_logicaldisk.deviceid=""" + drive + @":""");
                dsk.Get();
                string volumeSerial = dsk["VolumeSerialNumber"].ToString();
                string llave = cpuInfo + volumeSerial;
                string sDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string registro = System.IO.File.ReadAllText(@sDirectory + "\\license.lic");
                if (!llave.Equals(CryptorEngine.Decrypt(registro)))
                    throw new Exception();
            }
            catch (Exception error)
            {
                //throw error;
                Application.Exit();
                Environment.Exit(1);
                Form.ActiveForm.Close();
            }
        }
        void CargarFotosArtistas()
        {
            string[] vExtensiones = sExtensionesImagenes.Split(';');
            lstDireccionFotosArtistas = new List<FotosArtistas>();
            FotosArtistas objFotoArtista;
            foreach (string Exten in vExtensiones)
            {
                foreach (string file in System.IO.Directory.GetFiles(sDireccionFotosArtistas, Exten))
                {
                    objFotoArtista = new FotosArtistas(file, file.Replace(@sDireccionFotosArtistas, "").Replace(Exten.Replace("*",""), "").Replace("\\",""));
                    lstDireccionFotosArtistas.Add(objFotoArtista);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InsertCoin();
        }
        void InsertCoin()
        {
            GridView view = (gCanciones.FocusedView as GridView);
            object t = view.GetRowCellValue(view.FocusedRowHandle, "Direccion");
            //MessageBox.Show(t.ToString());
            SmartRocko.CoreRocko.SaveSongInList(sListaEspera, sDireccionCanciones, t.ToString(), sExtensiones, bTipoListado, sListaCanciones);
            //StartPlayWaitingList();
            //LoadWaitingList();
            if(ReproductorSegundaPantalla.iCantidadCanciones == 0)
                ReproductorSegundaPantalla.Reproductor2.Ctlcontrols.stop();
            ReproductorSegundaPantalla.Reproductor2.settings.volume = 100;
            ReproductorSegundaPantalla.Reproductor2.settings.setMode("loop", false);
            ReproductorSegundaPantalla.StartPlayWaitingList();
            ReproductorSegundaPantalla.LoadWaitingList();
            SoundCoin();
            if (TipoSeleccionado == TipoListado.Top20)
                Top20Songs();

        }
        void CreateDialog()
        {
            bool IsOpen = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form f in fc)
            {
                if (f.Name == "ReproductorGrande")
                {
                    IsOpen = true;
                    //f.Focus();
                    break;
                }
            }

            if (IsOpen == false)
            {

                ReproductorSegundaPantalla.Show();
            }
        }
        void CreatePreviewFloating()
        {
            bool IsOpen = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form f in fc)
            {
                if (f.Name == "ContenedorVistaPrevia")
                {
                    IsOpen = true;
                    //f.Focus();
                    break;
                }
            }

            if (IsOpen == false)
            {
                //ContenedorVistaPreviaFlotante.TopMost = true;
                //ContenedorVistaPreviaFlotante.Show();
            }
        }
        void StartPlayWaitingList()
        {
            if (EnabledFirstScreen)
            {
                try
                {
                    if (Reproductor.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    {
                    }
                    else
                    {
                        DataTable dtResultado = new DataTable();
                        if (bReadSongsEveryEvent)
                            dtResultado = obj.ReadList(@sListaEspera);
                        else
                            dtResultado = dsAllSongs;
                        if (dtResultado != null)
                        {
                            foreach (DataRow row in dtResultado.Rows)
                            {
                                if (dtResultado.Rows.Count > playlist.count)
                                {
                                    string s = row["Direccion"].ToString();
                                    WMPLib.IWMPMedia temp = Reproductor.newMedia(s); //Load media from URL. 
                                    playlist.appendItem(temp); //Add song to playlist.
                                }
                            }
                            this.Reproductor.currentPlaylist = playlist;
                            Reproductor.uiMode = "full";
                            this.Reproductor.settings.autoStart = true;
                        }
                        else
                        {
                            Reproductor.newPlaylist("RockoLocoList", string.Empty);
                            this.Reproductor.currentPlaylist = playlist;
                            Reproductor.uiMode = "full";
                        }
                    }
                    LoadWaitingList();
                }
                catch (Exception err)
                {
                }
            }
        }
        private void gridControl1_Load_2(object sender, EventArgs e)
        {
            AllSongs();
        }

        private void gListaEspera_Load(object sender, EventArgs e)
        {
            LoadWaitingList();
        }
        void LoadWaitingList()
        {
            //if(EnabledFirstScreen)
            gListaEspera.DataSource = obj.ReadListWaitingList(sListaEspera);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //pnlAlphabet.Location = new Point(-80, 12);
            Reproductor.uiMode = "none";
            this.Reproductor.settings.autoStart = true;
            playlist = Reproductor.newPlaylist("RockoLocoList", string.Empty);
            AnimateWindow(this.Handle, velocidad, AW_SLIDE | AW_HOR_POSITIVE);
            CreateDialog();
            CreatePreviewFloating();
            StartPlayWaitingList();
            DevExpress.UserSkins.BonusSkins.Register();
            //Metropolis Dark
            //McSkin
            //Whiteprint
            //iMaginary
            //The Asphalt World
            ChangeTemplateControls(sTemplate);
            CreateKeyboardAlphabetic(sTemplate);
            CreateKeyboard();
            gGridCanciones = (GridView)gCanciones.MainView;
            pnlKeyboard.Visible = false;

            txtBuscar.Width = ((this.Width) / 13) * 6;
            txtLimpiar.Location = new Point(txtBuscar.Width + 10, txtLimpiar.Location.Y);
            txtLimpiar.Size = new Size((this.Width) / 8, txtLimpiar.Height);
            pnlBuscar.Size = new Size(((this.Width) / 5) * 3, ((this.Height) / 23) * 2);
            pnlVistaPrevia.Size = new Size(((this.Width) / 11) * 3, ((this.Height) / 23) * 9);
            VistaPrevia.Size = pnlVistaPrevia.Size;
            pnlVistaPrevia.Location = new Point((((this.Width) / 5) * 3) + pnlBuscar.Location.X + 10, pnlBuscar.Location.Y);
            tbVolumeControl.Visible = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["ShowVolumenControl"]);
            tbVolumeControl.Location = new Point(pnlVistaPrevia.Location.X, pnlVistaPrevia.Location.Y + pnlVistaPrevia.Height - 30);
            tbVolumeControl.Width = pnlVistaPrevia.Width;
            tbVolumeControl.Properties.Minimum = iMinVolumen;
            tbVolumeControl.Properties.Maximum = iMaxVolumen;
            tbVolumeControl.Value = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["DefaultVolumen"]);
            pnlAlphabet.Size = new Size(((this.Width) / 10), ((this.Height) / 25) * 28);
            pnlAlphabet.Location = new Point(pnlVistaPrevia.Location.X + pnlVistaPrevia.Width + 10, pnlVistaPrevia.Location.Y);
            pnlLogo.Size = pnlVistaPrevia.Size;
            wmpLogo.Size = new Size(pnlVistaPrevia.Size.Width + 10, pnlVistaPrevia.Size.Height + 25);
            pnlLogo.Location = pnlVistaPrevia.Location;
            gbCanciones.Location = new Point(pnlBuscar.Location.X, pnlBuscar.Location.Y + pnlBuscar.Height + 10);
            gbCanciones.Size = new Size(pnlBuscar.Size.Width, ((this.Height) / 6) * 3);
            gCanciones.Size = new Size(pnlBuscar.Size.Width - 15, (((this.Height) / 6) * 3) - 30);
            grpListaEspera.Location = new Point(pnlBuscar.Location.X, gbCanciones.Location.Y + gbCanciones.Height + 10);
            grpListaEspera.Size = new Size(pnlBuscar.Size.Width, ((this.Height) / 7) * 2);
            gListaEspera.Size = new Size(pnlBuscar.Size.Width - 15, (((this.Height) / 7) * 2) - 30);
            int iPosicion = pnlVistaPrevia.Location.Y + pnlVistaPrevia.Height + 10;
            label2.Location = new Point(pnlVistaPrevia.Location.X, iPosicion + 10);
            lblCreditos.Location = new Point(pnlVistaPrevia.Location.X + label2.Width + 15, iPosicion + 10);
            //lblPercent.Location = new Point(pnlVistaPrevia.Location.X, btnTeclado.Height + iPosicion - 30);
            lblPercent.Visible = false;
            pbarMedia.Size = new Size(pnlVistaPrevia.Width, (pnlVistaPrevia.Height) / 7);
            btnTeclado.Size = new Size((pnlVistaPrevia.Width / 2), (pnlVistaPrevia.Height / 4));
            pbarMedia.Location = new Point(pnlVistaPrevia.Location.X, btnTeclado.Height + iPosicion -10);
            btnTeclado.Location = new Point(pnlVistaPrevia.Location.X, pbarMedia.Location.Y + pbarMedia.Height + 10);
            btnTop20Ranking.Size = new Size((pnlVistaPrevia.Width / 2), (pnlVistaPrevia.Height / 4));
            btnTop20Ranking.Location = new Point(pnlVistaPrevia.Location.X + (pnlVistaPrevia.Width / 2), pbarMedia.Location.Y + pbarMedia.Height + 10);
            cbSkins.Location = new Point(btnTop20Ranking.Location.X + (btnTop20Ranking.Width / 2), btnTop20Ranking.Location.Y + btnTop20Ranking.Height + 10);

            button1.Location = new Point(pnlVistaPrevia.Location.X, btnTop20Ranking.Height + btnTop20Ranking.Location.Y + 10);
            btnLeerCanciones.Location = new Point(pnlVistaPrevia.Location.X, button1.Height + button1.Location.Y + 10);

            imgArtist.Size = new Size(pnlVistaPrevia.Width, pnlVistaPrevia.Height);
            imgArtist.Location = new Point(btnTeclado.Location.X, btnTeclado.Location.Y + btnTeclado.Height + 10);

            System.IntPtr ptrBorder = CreateRoundRectRgn(2, 2, this.txtBuscar.Width - 2, this.txtBuscar.Height - 2, 20, 20);
            try { this.txtBuscar.Region = System.Drawing.Region.FromHrgn(ptrBorder); } finally { DeleteObject(ptrBorder); }
            label2.BackColor = Color.Transparent;
            lblPercent.BackColor = Color.Transparent;
            System.IntPtr ptrBorderVistaPrevia = CreateRoundRectRgn(3, 3, this.pnlVistaPrevia.Width - 3, this.pnlVistaPrevia.Height - 3, 20, 20);
            try { this.pnlVistaPrevia.Region = System.Drawing.Region.FromHrgn(ptrBorderVistaPrevia); } finally { DeleteObject(ptrBorderVistaPrevia); }
            System.IntPtr ptrBorderLogo = CreateRoundRectRgn(3, 3, this.pnlLogo.Width - 3, this.pnlLogo.Height - 3, 20, 20);
            try { this.pnlLogo.Region = System.Drawing.Region.FromHrgn(ptrBorderLogo); } finally { DeleteObject(ptrBorderLogo); }
            System.IntPtr ptrFotoArtista = CreateRoundRectRgn(3, 3, this.imgArtist.Width - 3, this.imgArtist.Height - 3, 20, 20);
            try { this.imgArtist.Region = System.Drawing.Region.FromHrgn(ptrFotoArtista); } finally { DeleteObject(ptrFotoArtista); }
            new TouchScrollHelper((GridView)gCanciones.MainView);
            //System.IntPtr ptrBorder2 = CreateRoundRectRgn(1, 1, this.pnlBuscar.Width - 1, this.pnlBuscar.Height - 1, 5, 5);
            //try { this.pnlBuscar.Region = System.Drawing.Region.FromHrgn(ptrBorder2); } finally { DeleteObject(ptrBorder2); }
            //gCanciones.MainView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            //gCanciones.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            ((GridView)gListaEspera.MainView).OptionsSelection.EnableAppearanceFocusedRow = false;
            ((GridView)gListaEspera.MainView).OptionsSelection.EnableAppearanceFocusedCell = false;
            ((GridView)gListaEspera.MainView).OptionsSelection.UseIndicatorForSelection = false;
            ((GridView)gListaEspera.MainView).OptionsView.ShowIndicator = false;
            if (bMaximized)
            {
                MaximizeWindow();
            }
            if (bShowArtistPicture)
            {
                CargarFotosArtistas();
                imgArtist.SizeMode = PictureBoxSizeMode.StretchImage;
                imgArtist.BackgroundImageLayout = ImageLayout.Stretch;
                imgArtist.BackColor = Color.Black;
            }
            //695; 55
            //string sDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            //flasRocko.Movie = @sDirectory + @"\Rocko.swf";
            //flasRocko.Play();
            Process process = Process.GetCurrentProcess(); //Get an instance of current process
            if (process.PriorityClass != ProcessPriorityClass.RealTime) //If it is high
            {
                process.PriorityClass = ProcessPriorityClass.RealTime; //Change it to Normal
            }
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.All_MouseDown);
            AddClicksAllControls(this.Controls);
        }
        // Generate new image dimensions
        public Size GenerateImageDimensions(int currW, int currH, int destW, int destH)
        {
            // double to hold the final multiplier to use when scaling the image
            double multiplier = 0;
            // string for holding layout
            string layout;
            // determine if it's Portrait or Landscape
            if ((currH > currW))
            {
                layout = "portrait";
            }
            else
            {
                layout = "landscape";
            }

            switch (layout.ToLower())
            {
                case "portrait":
                    if ((destH > destW))
                    {
                        multiplier = (Convert.ToDouble(destW) / Convert.ToDouble(currW));
                    }
                    else
                    {
                        multiplier = (Convert.ToDouble(destH) / Convert.ToDouble(currH));
                    }

                    break;
                case "landscape":
                    if ((destH > destW))
                    {
                        multiplier = (Convert.ToDouble(destW) / Convert.ToDouble(currW));
                    }
                    else
                    {
                        multiplier = (Convert.ToDouble(destH) / Convert.ToDouble(currH));
                    }

                    break;
            }
            // return the new image dimensions
            return new Size(Convert.ToInt32((currW * multiplier)), Convert.ToInt32((currH * multiplier)));
        }

        // Resize the image
        private void SetImage(PictureBox pb)
        {
            try
            {
                // create a temp image
                Image img = pb.Image;
                // calculate the size of the image
                Size imgSize = GenerateImageDimensions(img.Width, img.Height, pb.Width, pb.Height);
                // create a new Bitmap with the proper dimensions
                Bitmap finalImg = new Bitmap(img, imgSize.Width, imgSize.Height);
                // create a new Graphics object from the image
                Graphics gfx = Graphics.FromImage(img);
                // clean up the image (take care of any image loss from resizing)
                gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // empty the PictureBox
                pb.Image = null;
                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                // set the new image
                pb.Image = finalImg;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private void ChangeTemplateControls(string Template)
        {
            switch (Template)
            {
                case "sSkin1": sTemplate = "Caramel"; break;
                case "sSkin2": sTemplate = "Money Twins"; break;
                case "sSkin3": sTemplate = "Lilian"; break;
                case "sSkin4": sTemplate = "The Asphalt World"; break;
                case "sSkin5": sTemplate = "iMaginary"; break;
                case "sSkin6": sTemplate = "Black"; break;
                case "sSkin7": sTemplate = "Blue"; break;
                case "sSkin8": sTemplate = "Coffee"; break;
                case "sSkin9": sTemplate = "Liquid Sky"; break;
                case "sSkin10": sTemplate = "London Liquid Sky"; break;
                case "sSkin11": sTemplate = "Glass Oceans"; break;
                case "sSkin12": sTemplate = "Stardust"; break;
                case "sSkin13": sTemplate = "Xmas 2008 Blue"; break;
                case "sSkin14": sTemplate = "Valentine"; break;
                case "sSkin15": sTemplate = "McSkin"; break;
                case "sSkin16": sTemplate = "Summer 2008"; break;
                case "sSkin17": sTemplate = "Pumpkin"; break;
                case "sSkin18": sTemplate = "Dark Side"; break;
                case "sSkin19": sTemplate = "Springtime"; break;
                case "sSkin20": sTemplate = "Darkroom"; break;
                case "sSkin21": sTemplate = "Foggy"; break;
                case "sSkin22": sTemplate = "High Contrast"; break;
                case "sSkin23": sTemplate = "Seven"; break;
                case "sSkin24": sTemplate = "Seven Classic"; break;
                case "sSkin25": sTemplate = "Sharp"; break;
                case "sSkin26": sTemplate = "Sharp Plus"; break;
                case "sSkin27": sTemplate = "DevExpress Style"; break;
                case "sSkin28": sTemplate = "Office 2007 Blue"; break;
                case "sSkin29": sTemplate = "Office 2007 Black"; break;
                case "sSkin30": sTemplate = "Office 2007 Silver"; break;
                case "sSkin31": sTemplate = "Office 2007 Green"; break;
                case "sSkin32": sTemplate = "Office 2007 Pink"; break;
                case "sSkin33": sTemplate = "Office 2010 Blue"; break;
                case "sSkin34": sTemplate = "Office 2010 Black"; break;
                case "sSkin35": sTemplate = "Office 2010 Silver"; break;
                default: sTemplate = "McSkin"; break;
            }
            //if (sTemplate.Equals(""))
            //    sTemplate = sSkin15;
            gCanciones.LookAndFeel.UseDefaultLookAndFeel = false;
            gCanciones.LookAndFeel.SkinName = sTemplate;
            gListaEspera.LookAndFeel.UseDefaultLookAndFeel = false;
            gListaEspera.LookAndFeel.SkinName = sTemplate;
            btnPreviewFloating.LookAndFeel.UseDefaultLookAndFeel = false;
            btnPreviewFloating.LookAndFeel.SkinName = sTemplate;
            btnTeclado.LookAndFeel.UseDefaultLookAndFeel = false;
            btnTeclado.LookAndFeel.SkinName = sTemplate;
            btnTop20Ranking.LookAndFeel.UseDefaultLookAndFeel = false;
            btnTop20Ranking.LookAndFeel.SkinName = sTemplate;
            CreatAlphabetButtons(sTemplate);
            //ContenedorVistaPreviaFlotante.PreviewFixed = true;
            SizeFormPrincipal = this.Width;
            //new Animator2D(new Path2D(new Float2D(SizeFormPrincipal - pnlAlphabet.Width - 60 + 600, -1), new Float2D(SizeFormPrincipal - pnlAlphabet.Width - 50, -1), 500)).Play(pnlAlphabet, Animator2D.KnownProperties.Location);
            pnlBuscar.LookAndFeel.UseDefaultLookAndFeel = false;
            pnlBuscar.LookAndFeel.SkinName = sTemplate;
            pbarMedia.LookAndFeel.UseDefaultLookAndFeel = false;
            pbarMedia.LookAndFeel.SkinName = sTemplate;
            ReproductorSegundaPantalla.pbarMedia.LookAndFeel.UseDefaultLookAndFeel = false;
            ReproductorSegundaPantalla.pbarMedia.LookAndFeel.SkinName = sTemplate;
            pnlKeyboard.LookAndFeel.UseDefaultLookAndFeel = false;
            pnlKeyboard.LookAndFeel.SkinName = sTemplate;
            pnlKeyboard.Size = new Size((((this.Width - 200) / 15) * 12), (((this.Width - 200) / 15) * 3) + 40);
            pnlKeyboardAlfhabet.LookAndFeel.UseDefaultLookAndFeel = false;
            pnlKeyboardAlfhabet.LookAndFeel.SkinName = sTemplate;
            pnlKeyboardAlfhabet.Size = new Size((((this.Width - 200) / 15) * 12), (((this.Width - 200) / 15) * 3) + 40);
            tbVolumeControl.LookAndFeel.UseDefaultLookAndFeel = false;
            tbVolumeControl.LookAndFeel.SkinName = sTemplate;
            if (!bPreview)
            {
                pnlVistaPrevia.Visible = false;
                pnlLogo.Visible = true;
                string sDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                wmpLogo.URL = @sDirectory + @"\ROCKO.mp4";
                wmpLogo.uiMode = "none";
                wmpLogo.settings.setMode("loop", true);
                wmpLogo.Ctlcontrols.play();
            }
            if (bShowBackImages)
            {
                LoadBackgroundWallpaper(Template);
            }
        }
        private void ChangeTemplateControlsDinamically(string Template)
        {
            switch (Template)
            {
                case "sSkin1": sTemplate = "Caramel"; break;
                case "sSkin2": sTemplate = "Money Twins"; break;
                case "sSkin3": sTemplate = "Lilian"; break;
                case "sSkin4": sTemplate = "The Asphalt World"; break;
                case "sSkin5": sTemplate = "iMaginary"; break;
                case "sSkin6": sTemplate = "Black"; break;
                case "sSkin7": sTemplate = "Blue"; break;
                case "sSkin8": sTemplate = "Coffee"; break;
                case "sSkin9": sTemplate = "Liquid Sky"; break;
                case "sSkin10": sTemplate = "London Liquid Sky"; break;
                case "sSkin11": sTemplate = "Glass Oceans"; break;
                case "sSkin12": sTemplate = "Stardust"; break;
                case "sSkin13": sTemplate = "Xmas 2008 Blue"; break;
                case "sSkin14": sTemplate = "Valentine"; break;
                case "sSkin15": sTemplate = "McSkin"; break;
                case "sSkin16": sTemplate = "Summer 2008"; break;
                case "sSkin17": sTemplate = "Pumpkin"; break;
                case "sSkin18": sTemplate = "Dark Side"; break;
                case "sSkin19": sTemplate = "Springtime"; break;
                case "sSkin20": sTemplate = "Darkroom"; break;
                case "sSkin21": sTemplate = "Foggy"; break;
                case "sSkin22": sTemplate = "High Contrast"; break;
                case "sSkin23": sTemplate = "Seven"; break;
                case "sSkin24": sTemplate = "Seven Classic"; break;
                case "sSkin25": sTemplate = "Sharp"; break;
                case "sSkin26": sTemplate = "Sharp Plus"; break;
                case "sSkin27": sTemplate = "DevExpress Style"; break;
                case "sSkin28": sTemplate = "Office 2007 Blue"; break;
                case "sSkin29": sTemplate = "Office 2007 Black"; break;
                case "sSkin30": sTemplate = "Office 2007 Silver"; break;
                case "sSkin31": sTemplate = "Office 2007 Green"; break;
                case "sSkin32": sTemplate = "Office 2007 Pink"; break;
                case "sSkin33": sTemplate = "Office 2010 Blue"; break;
                case "sSkin34": sTemplate = "Office 2010 Black"; break;
                case "sSkin35": sTemplate = "Office 2010 Silver"; break;
                default: sTemplate = "McSkin"; break;
            }
            foreach (Control control in pnlAlphabet.Controls)
            {
                if (control is DevExpress.XtraEditors.SimpleButton)
                    (control as DevExpress.XtraEditors.SimpleButton).LookAndFeel.SkinName = sTemplate;
            }
            foreach (Control control in pnlKeyboard.Controls)
            {
                if (control is DevExpress.XtraEditors.SimpleButton)
                    (control as DevExpress.XtraEditors.SimpleButton).LookAndFeel.SkinName = sTemplate;
            }
            foreach (Control control in pnlKeyboardAlfhabet.Controls)
            {
                if (control is DevExpress.XtraEditors.SimpleButton)
                    (control as DevExpress.XtraEditors.SimpleButton).LookAndFeel.SkinName = sTemplate;
            }
            ReproductorSegundaPantalla.sDireccionImagenes = System.Configuration.ConfigurationSettings.AppSettings["DireccionImagenes"] + "\\" + Template;
            if (bShowBackImages)
            {
                LoadBackgroundWallpaper(Template);
            }
        }

        private void LoadBackgroundWallpaper(string Template)
        {
            Image iBackImage = new Bitmap(@System.Configuration.ConfigurationSettings.AppSettings["DireccionImagenes"] + "\\Wallpaper\\" + Template + ".jpg");
            this.BackgroundImage = iBackImage;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void MaximizeWindow()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            RockoTouch.WinApi.SetWinFullScreen(this.Handle);
            //RockoTouch.Taskbar.Hide();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (e.Cancel == false)
            {
                AnimateWindow(this.Handle, velocidad, AW_HIDE | AW_HOR_NEGATIVE);
            }
        }
        private void Reproductor_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                obj.DeleteFromWaitList(@sListaEspera, Reproductor.currentMedia.sourceURL);
                playlist = Reproductor.newPlaylist("RockoLocoList", string.Empty);
                timer1.Interval = 100;
                timer1.Start();
                timer1.Enabled = true;
                timer1.Tick += timer1_Tick;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            StartPlayWaitingList();
            timer1.Enabled = false;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            pnlLogo.Visible = true;
            string sDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            wmpLogo.URL = @sDirectory + @"\ROCKO.mp4";
            wmpLogo.uiMode = "none";
            wmpLogo.settings.setMode("loop", true);
            this.wmpLogo.settings.autoStart = true;
            timer2.Enabled = false;
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            iCounter--;
            if (iCounter == 0)
            {
                timer3.Enabled = false;
                timer3.Stop();
                timer3.Dispose();
                StartPlayPreview();
                isScrolling = false;
            }
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (isScrolling)
            {
                iCounter = 3;
                timer3.Stop();
                timer3.Dispose();
                timer3.Interval = 1000;
                timer3.Start();
                timer3.Enabled = true;
                timer3.Tick += new EventHandler(timer3_Tick);
            }
            else
            {
                StartPlayPreview();
            }
            ShowArtistPicture();
        }
        void ShowArtistPicture()
        {
            if(bShowArtistPicture)
            {
                imgArtist.Image = null;
                for (int i = 0; i < lstDireccionFotosArtistas.Count; i++)
                {
                    if (((GridView)gCanciones.MainView).GetFocusedValue() != null)
                    {
                        if (((GridView)gCanciones.MainView).GetFocusedValue().ToString().StartsWith(lstDireccionFotosArtistas[i].sNombre))
                        {
                            imgArtist.Image = Image.FromFile(lstDireccionFotosArtistas[i].sDireccion);
                            SetImage(imgArtist);
                            return;
                        }
                        if (((GridView)gCanciones.MainView).GetFocusedValue().ToString().Contains(lstDireccionFotosArtistas[i].sNombre))
                        {
                            imgArtist.Image = Image.FromFile(lstDireccionFotosArtistas[i].sDireccion);
                            SetImage(imgArtist);
                            return;
                        }
                    }
                }
            }
        }
        void StartPlayPreview()
        {
            if (EnabledFirstScreenPreview)
                ChangePreview();
            else
                ChangePreviewFloating();
        }
        void ChangePreview()
        {
            if (gridView1.GetFocusedDataRow() != null)
            {
                if (bPreview)
                {
                    if (!pnlVistaPrevia.Visible)
                    {
                        if (pnlLogo.Visible)
                            Util.Animate(pnlLogo, Util.Effect.Center, 300, 0);
                        Util.Animate(pnlVistaPrevia, Util.Effect.Center, 300, 0);
                    }
                    VistaPrevia.URL = gridView1.GetFocusedDataRow()["Direccion"].ToString();
                    VistaPrevia.windowlessVideo = true;
                    VistaPrevia.uiMode = "none";
                }
            }
            //if (ReproductorSegundaPantalla.Reproductor2.playState == WMPPlayState.wmppsPlaying)
            ControlVolumePreviewAndMain();
            InitCounterClock();
        }
        void ControlVolumePreviewAndMain()
        {
            if (ReproductorSegundaPantalla.iCantidadCanciones > 0)
                VistaPrevia.settings.volume = 0;
            else
            {
                ReproductorSegundaPantalla.Reproductor2.settings.volume = 0;
                VistaPrevia.settings.volume = 5;
            }
        }
    void ChangePreviewFloating()
        {
            //if(ContenedorVistaPreviaFlotante.VistaPrevia.playState != WMPPlayState.wmppsPlaying && !ContenedorVistaPreviaFlotante.Visible)
            //    ContenedorVistaPreviaFlotante.ShowScrenn();
            //if (gridView1.GetFocusedDataRow() != null)
            //{
            //    ContenedorVistaPreviaFlotante.VistaPrevia.URL = gridView1.GetFocusedDataRow()["Direccion"].ToString();
            //    ContenedorVistaPreviaFlotante.VistaPrevia.uiMode = "none";
            //}
            //if (ReproductorSegundaPantalla.Reproductor2.playState == WMPPlayState.wmppsPlaying)
            //    ContenedorVistaPreviaFlotante.VistaPrevia.settings.volume = 0;
            //else
            //    ContenedorVistaPreviaFlotante.VistaPrevia.settings.volume = 5;
            //ContenedorVistaPreviaFlotante.InitCounterClock();
        }
        private void VistaPrevia_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (EnabledFirstScreenPreview)
            {
                if ((WMPLib.WMPOpenState)e.newState == WMPLib.WMPOpenState.wmposMediaOpen)
                {
                    InitCounterClock();
                }
            }
        }
        void InitCounterClock()
        {
            if (bPreview)
            {
                ControlVolumePreviewAndMain();
                ShutOffTimer.Stop();
                ShutOffTimer.Dispose();
                ShutOffTimer = new System.Windows.Forms.Timer();
                VistaPrevia.Ctlcontrols.currentPosition = 0;
                ShutOffTimer.Interval = 15000;
                ShutOffTimer.Tick += ShutOffTimer_Tick;
                ShutOffTimer.Start();
            }
        }
        private void ShutOffTimer_Tick(object sender, EventArgs e)
        {
            VistaPrevia.Ctlcontrols.stop();
            ((System.Windows.Forms.Timer)sender).Stop();
            ((System.Windows.Forms.Timer)sender).Dispose();
            if (bPreview)
            {
                if (pnlVistaPrevia.Visible)
                {
                    Util.Animate(pnlVistaPrevia, Util.Effect.Center, 300, 0);
                    if (!pnlLogo.Visible)
                    {
                        Util.Animate(pnlLogo, Util.Effect.Center, 300, 0);
                        string sDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                        wmpLogo.URL = @sDirectory + @"\ROCKO.mp4";
                        wmpLogo.uiMode = "none";
                        wmpLogo.settings.setMode("loop", true);
                    }
                }
            }
        }

        private void btnLeerCanciones_Click(object sender, EventArgs e)
        {
            LoadNewSongs();
        }
        void LoadNewSongs()
        {
            if (bReadSongsEveryEvent)
            {
                obj.GenerateListOfSongs(@sDireccionCanciones, @sListaCanciones, sExtensiones, bTipoListado, bResetTopSongs, bMakeToUpper);
                gCanciones.DataSource = obj.ReadList(sListaCanciones);
            }
            else
            {
                obj.GenerateListOfSongs(@sDireccionCanciones, @sListaCanciones, sExtensiones, bTipoListado, bResetTopSongs, bMakeToUpper);
                dsAllSongs = obj.ReadList(sListaCanciones);
                gCanciones.DataSource = dsAllSongs;
            }
        }
        public void SoundCoin()
        {
            using (var soundPlayer = new SoundPlayer(RockoTouch.Resources.MarioCoin))
            {
                soundPlayer.Play();
            }
        }

        private void btnPreviewFloating_Click(object sender, EventArgs e)
        {
            Skin currentSkin = CommonSkins.GetSkin(btnPreviewFloating.LookAndFeel);
            Color ActivedColor = currentSkin.TranslateColor(SystemColors.Control);
            Color DisabledColor = currentSkin.TranslateColor(SystemColors.ControlLightLight);

            //if (!ContenedorVistaPreviaFlotante.PreviewFixed)
            //{
            //    btnPreviewFloating.Text = "VISTA PREVIA FIJA";
            //    //DefaultColor = btnPreviewFloating.Appearance.BackColor;
            //    btnPreviewFloating.AppearancePressed.BackColor = DisabledColor;
            //    ContenedorVistaPreviaFlotante.PreviewFixed = true;
            //}
            //else
            //{
            //    btnPreviewFloating.Text = "VISTA PREVIA FLOTANTE";
            //    btnPreviewFloating.Appearance.BackColor = ActivedColor;
            //    ContenedorVistaPreviaFlotante.PreviewFixed = false;
            //}
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // if just starting, move to start location and make visible
            if (!pnlAlphabet.Visible)
            {
                pnlAlphabet.Left = _startLeft;
                pnlAlphabet.Visible = true;
            }

            // incrementally move
            pnlAlphabet.Left += _stepSize;
            // make sure we didn't over shoot
            if (pnlAlphabet.Left > _endLeft) pnlAlphabet.Left = _endLeft;

            // have we arrived?
            if (pnlAlphabet.Left == _endLeft)
            {
                pnlAlphabet.Enabled = true;
            }
            AnimationTimer.Stop();
            AnimationTimer.Dispose();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(TypeKeyboadAlphabetic)
            {
                TypeKeyboadAlphabetic = false;
                btnTeclado.Text = "TECLADO ALFABETICO";
                HideKeyboardAlphabetic();
                ShowKeyboardQwerty();
                //new Animator2D(new Path2D(new Float2D(SizeFormPrincipal-pnlAlphabet.Width - 60, -1), new Float2D(SizeFormPrincipal - pnlAlphabet.Width + 600 -60, -1), 500)).Play(pnlAlphabet, Animator2D.KnownProperties.Location);
            }
            else {
                TypeKeyboadAlphabetic = true;
                btnTeclado.Text = "TECLADO QWERTY";
                HideKeyboardQwerty();
                ShowKeyboardAlphabetic();
                //new Animator2D(new Path2D(new Float2D(SizeFormPrincipal - pnlAlphabet.Width -60 + 600, -1), new Float2D(SizeFormPrincipal - pnlAlphabet.Width -50, -1), 500)).Play(pnlAlphabet, Animator2D.KnownProperties.Location);
            }

        }
        void CreatAlphabetButtons(string Template)
        {
            DevExpress.XtraEditors.SimpleButton BtnCap;
            int Lft = 5;
            int Top = 1;

            BtnCap = new DevExpress.XtraEditors.SimpleButton();
            var _with1 = BtnCap;
            _with1.Name = "BtnC_TODOS";
            _with1.Text = "TODOS";
            //_with1.Size = new Size(66, 27);
            _with1.Size = new Size((this.Width) / 16, (this.Height) / 29);
            _with1.Location = new Point(Lft, Top);
            _with1.Tag = Convert.ToChar("%");
            //Top = Top + 28;
            Top = Top + ((this.Height) / 30);
            _with1.Visible = true;
            BtnCap.LookAndFeel.UseDefaultLookAndFeel = false;
            BtnCap.LookAndFeel.SkinName = Template;
            BtnCap.Appearance.Font = new Font(BtnCap.Appearance.Font.FontFamily, 10, FontStyle.Bold);
            pnlAlphabet.Controls.Add(BtnCap);
            BtnCap.Click += BtnCap_click;

            for (int i = Convert.ToInt32('A'); i <= Convert.ToInt32('Z'); i++)
            {
                BtnCap = new DevExpress.XtraEditors.SimpleButton();
                _with1 = BtnCap;
                _with1.Name = "BtnC_" + Convert.ToChar(i);
                _with1.Text = "&" + Convert.ToChar(i);
                //_with1.Size = new Size(66, 27);
                _with1.Size = new Size((this.Width) / 16, ((this.Height) / 29));
                _with1.Location = new Point(Lft, Top);
                _with1.Tag = Convert.ToChar(i);
                //Top = Top + 28;
                Top = Top + ((this.Height) / 30);
                _with1.Visible = true;
                BtnCap.LookAndFeel.UseDefaultLookAndFeel = false;
                BtnCap.LookAndFeel.SkinName = Template;
                BtnCap.Appearance.Font = new Font(BtnCap.Appearance.Font.FontFamily, 10, FontStyle.Bold);
                pnlAlphabet.Controls.Add(BtnCap);
                BtnCap.Click += BtnCap_click;
            }
        }
        private void BtnCap_click(System.Object sender, System.EventArgs e)
        {
            //TextBox1.Text = TextBox1.Text + ((Button)sender).Tag;
            
            DevExpress.XtraEditors.SimpleButton button = (DevExpress.XtraEditors.SimpleButton)sender;
            //gGridCanciones.FindFilterText = button.Tag.ToString();
            if (bTipoListado)
            {
                GridColumn columnCustomer = gGridCanciones.Columns["Artista"];
                txtBuscar.Text = string.Empty;
                columnCustomer.FilterInfo = new ColumnFilterInfo("[Artista] LIKE '" + button.Tag.ToString() + "%'");
            }
            else
            {
                GridColumn columnCustomer = gGridCanciones.Columns["NombreCancion"];
                txtBuscar.Text = string.Empty;
                columnCustomer.FilterInfo = new ColumnFilterInfo("[NombreCancion] LIKE '" + button.Tag.ToString() + "%'");
            }
        }

        private void gridView1_Layout(object sender, EventArgs e)
        {
            //RockoLocoPlayer.MyGridControl control = gCanciones;
            //FindControl findControl = null;
            //if (control.Controls.OfType<FindControl>() != null)
            //{
            //    var controls = control.Controls.OfType<FindControl>();
            //    if (controls.Count() > 0)
            //    {
            //        findControl = controls.ElementAt(0);
            //        //findControl.FindButton.Click += FindButton_Click;
            //        findControl.ClearButton.Click += ClearButton_Click;
            //    }
            //}
        }
        private void ClearButton_Click(System.Object sender, System.EventArgs e)
        {
            //TextBox1.Text = TextBox1.Text + ((Button)sender).Tag;
            
            DevExpress.XtraEditors.SimpleButton button = (DevExpress.XtraEditors.SimpleButton)sender;
            //gGridCanciones.FindFilterText = button.Tag.ToString();
            if (bTipoListado)
            {
                GridColumn columnCustomer = gGridCanciones.Columns["Artista"];
                columnCustomer.FilterInfo = new ColumnFilterInfo("[Artista] LIKE '%'");
            }
            else
            {
                GridColumn columnCustomer = gGridCanciones.Columns["NombreCancion"];
                columnCustomer.FilterInfo = new ColumnFilterInfo("[NombreCancion] LIKE '%'");
            }
        }
        

        private string _filterRegexPattern = "[^a-zA-Z0-9]"; // This would be "[^a-z0-9 ]" for this question.
        private int _stringMaxLength = 300;
        void OnlyAlphaNumeric()
        {
            if (!string.IsNullOrEmpty(_filterRegexPattern))
            {
                var text = txtBuscar.Text;
                var newText = Regex.Replace(txtBuscar.Text, _filterRegexPattern, "");

                if (newText.Length > _stringMaxLength)
                {
                    newText = newText.Substring(0, _stringMaxLength);
                }


                if (text.Length != newText.Length)
                {
                    var selectionStart = txtBuscar.SelectionStart - (text.Length - newText.Length);
                    txtBuscar.Text = newText;
                    txtBuscar.SelectionStart = selectionStart;
                }
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            OnlyAlphaNumeric();
            if (bTipoListado)
            {
                GridColumn columnCustomer = gGridCanciones.Columns["Artista"];
                columnCustomer.FilterInfo = new ColumnFilterInfo("[Artista] LIKE '%" + ((TextBox)sender).Text.ToString().ToUpper() + "%' OR [NombreCancion] LIKE '%" + ((TextBox)sender).Text.ToString().ToUpper() + "%' ");
            }
            else {
                GridColumn columnCustomer = gGridCanciones.Columns["NombreCancion"];
                columnCustomer.FilterInfo = new ColumnFilterInfo("[NombreCancion] LIKE '%" + ((TextBox)sender).Text.ToString().ToUpper() + "%' ");
            }
        }

        private void txtLimpiar_Click_1(object sender, EventArgs e)
        {
            if (bTipoListado)
            {
                GridColumn columnCustomer = gGridCanciones.Columns["Artista"];
                columnCustomer.FilterInfo = new ColumnFilterInfo("[Artista] LIKE '%'");
                txtBuscar.Text = string.Empty;
            }
            else {
                GridColumn columnCustomer = gGridCanciones.Columns["NombreCancion"];
                columnCustomer.FilterInfo = new ColumnFilterInfo("[NombreCancion] LIKE '%'");
                txtBuscar.Text = string.Empty;
            }
        }

        //SCREEN KEYBOARD

        void CreateKeyboard()
        {
            DevExpress.XtraEditors.SimpleButton BtnCap;
            int Lft = 10;
            int Top = 10;
            List<string> lstAlphabet = new List<string>(new string[] { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Ñ", "Z", "X", "C", "V", "B", "N", "M" });
            foreach (string i in lstAlphabet)// (int i = Convert.ToInt32('A'); i <= Convert.ToInt32('Z'); i++)
            {
                BtnCap = new DevExpress.XtraEditors.SimpleButton();
                var _with1 = BtnCap;
                _with1.Name = "BtnC_" + Convert.ToChar(i);
                _with1.Text = "&" + Convert.ToChar(i);
                _with1.Size = new Size((this.Width - 200)/15, (this.Width - 200) / 15);
                _with1.Location = new Point(Lft, Top);
                _with1.Tag = Convert.ToChar(i);
                Lft = Lft + (this.Width - 200) / 15;
                if (Convert.ToChar(i).ToString() == "P")
                {
                    ButtonDeleteKeyboard(Lft, Top, pnlKeyboard, "btn_Borrar");
                    ButtonCloseKeyboard(Lft, Top + ((this.Width - 200) / 15) + 3, pnlKeyboard, "btn_Cerrar");
                    Top = Top + ((this.Width - 200) / 15) + 3;
                    Lft = 10;
                }
                if (Convert.ToChar(i).ToString() == "Ñ")
                {
                    Top = Top + ((this.Width - 200) / 15) + 3;
                    Lft = 10;
                }
                BtnCap.LookAndFeel.UseDefaultLookAndFeel = false;
                BtnCap.LookAndFeel.SkinName = sTemplate;
                BtnCap.Appearance.Font = new Font(BtnCap.Appearance.Font.FontFamily, 14, FontStyle.Bold);
                _with1.Visible = true;
                this.pnlKeyboard.Controls.Add(BtnCap);
                BtnCap.Click += BtnCap_click2;
                BtnCap.KeyPress += BtnCap_KeyPress;
            }
        }
        void CreateKeyboardAlphabetic(string Template)
        {
            DevExpress.XtraEditors.SimpleButton BtnCap;
            int Lft = 10;
            int Top = 10;
            for (int i = Convert.ToInt32('A'); i <= Convert.ToInt32('Z'); i++)
            {
                BtnCap = new DevExpress.XtraEditors.SimpleButton();
                var _with1 = BtnCap;
                _with1.Name = "BtnC_" + Convert.ToChar(i);
                _with1.Text = "&" + Convert.ToChar(i);
                _with1.Size = new Size((this.Width - 200) / 15, (this.Width - 200) / 15);
                _with1.Location = new Point(Lft, Top);
                _with1.Tag = Convert.ToChar(i);
                Lft = Lft + (this.Width - 200) / 15;
                if (Convert.ToChar(i).ToString() == "I")
                {
                    ButtonDeleteKeyboard(Lft, Top, pnlKeyboardAlfhabet, "btn_Borrar");
                    ButtonCloseKeyboard(Lft, Top + ((this.Width - 200) / 15) + 3, pnlKeyboardAlfhabet, "btnCerrarAlphabet");
                    Top = Top + ((this.Width - 200) / 15) +3;
                    Lft = 10;
                }
                if (Convert.ToChar(i).ToString() == "R")
                {
                    Top = Top + ((this.Width - 200) / 15) +3;
                    Lft = 10;
                }
                BtnCap.LookAndFeel.UseDefaultLookAndFeel = false;
                BtnCap.LookAndFeel.SkinName = Template;
                BtnCap.Appearance.Font = new Font(BtnCap.Appearance.Font.FontFamily, 14, FontStyle.Bold);
                _with1.Visible = true;
                this.pnlKeyboardAlfhabet.Controls.Add(BtnCap);
                BtnCap.Click += BtnCap_click2;
                BtnCap.KeyPress += BtnCap_KeyPress;
            }
        }
        void ButtonCloseKeyboard(int left, int top, Control ctrl, string pNameControl)
        {
            DevExpress.XtraEditors.SimpleButton BtnCap = new DevExpress.XtraEditors.SimpleButton();
            var _with1 = BtnCap;
            _with1.Name = pNameControl;
            _with1.Text = "CERRAR";
            _with1.Size = new Size(((this.Width - 300) / 15)*2, ((this.Width - 200) / 15)*2);
            _with1.Location = new Point(left, top);
            BtnCap.Appearance.Font = new Font(BtnCap.Appearance.Font.FontFamily, 10, FontStyle.Bold);
            _with1.Tag = "CERRAR";
            BtnCap.Click += BtnCap_clickCerrar;
            ctrl.Controls.Add(BtnCap);
        }
        void ButtonDeleteKeyboard(int left, int top, Control ctrl, string pNameControl)
        {
            DevExpress.XtraEditors.SimpleButton BtnCap = new DevExpress.XtraEditors.SimpleButton();
            var _with1 = BtnCap;
            _with1.Name = pNameControl;
            _with1.Text = "Borrar";
            _with1.Size = new Size(((this.Width - 300) / 15) * 2, (this.Width - 200) / 15);
            _with1.Location = new Point(left, top);
            BtnCap.Appearance.Font = new Font(BtnCap.Appearance.Font.FontFamily, 10, FontStyle.Bold);
            _with1.Tag = "Borrar";
            BtnCap.Click += BtnCap_clickBorrar;
            ctrl.Controls.Add(BtnCap);
        }
        private void BtnCap_click2(System.Object sender, System.EventArgs e)
        {
            //((MainForm)Application.OpenForms["MainForm"]).txtBuscar.Text = ((MainForm)Application.OpenForms["MainForm"]).txtBuscar.Text + ((DevExpress.XtraEditors.SimpleButton)sender).Tag;
            txtBuscar.Text = txtBuscar.Text + ((DevExpress.XtraEditors.SimpleButton)sender).Tag;
            //TextBox1.Text = TextBox1.Text + ((Button)sender).Tag;
        }
        private void BtnCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateKeyAndInsertCoin(e.KeyChar.ToString());
        }
        private void BtnCap_clickBorrar(System.Object sender, System.EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtBuscar.Text))
                txtBuscar.Text = txtBuscar.Text = txtBuscar.Text.Substring(0, txtBuscar.Text.Length - 1);
        }
        private void BtnCap_clickCerrar(System.Object sender, System.EventArgs e)
        {
            if (TypeKeyboadAlphabetic)
                HideKeyboardAlphabetic();
            else
                HideKeyboardQwerty();
        }
        void HideKeyboardAlphabetic()
        {
            pnlKeyboardAlfhabet.Location = new Point(250, this.Height - pnlKeyboardAlfhabet.Height);
            if(pnlKeyboardAlfhabet.Visible)
                Util.Animate(pnlKeyboardAlfhabet, Util.Effect.Slide, 100, 270);
            //new Animator2D(new Path2D(new Float2D(pnlKeyboardAlfhabet.Location.X, 400), new Float2D(pnlKeyboardAlfhabet.Location.X, 1200), 400)).Play(pnlKeyboardAlfhabet, Animator2D.KnownProperties.Location);
        }
        void HideKeyboardQwerty()
        {
            pnlKeyboard.Location = new Point(250, this.Height - pnlKeyboard.Height);
            if (pnlKeyboard.Visible)
                Util.Animate(pnlKeyboard, Util.Effect.Slide, 100, 270);
            //new Animator2D(new Path2D(new Float2D(pnlKeyboard.Location.X, 400), new Float2D(pnlKeyboard.Location.X, 1200), 400)).Play(pnlKeyboard, Animator2D.KnownProperties.Location);
        }
        void ShowKeyboardAlphabetic()
        {
            pnlKeyboardAlfhabet.Location = new Point(250, this.Height - pnlKeyboardAlfhabet.Height);
            if (!pnlKeyboardAlfhabet.Visible)
                Util.Animate(pnlKeyboardAlfhabet, Util.Effect.Slide, 100, 270);
            //new Animator2D(new Path2D(new Float2D(pnlKeyboardAlfhabet.Location.X, 1200), new Float2D(pnlKeyboardAlfhabet.Location.X, 400), 400)).Play(pnlKeyboardAlfhabet, Animator2D.KnownProperties.Location);
        }
        void ShowKeyboardQwerty()
        {
            pnlKeyboard.Location = new Point(250, this.Height - pnlKeyboard.Height);
            if (!pnlKeyboard.Visible)
                Util.Animate(pnlKeyboard, Util.Effect.Slide, 100, 270);
            //new Animator2D(new Path2D(new Float2D(pnlKeyboard.Location.X, 1200), new Float2D(pnlKeyboard.Location.X, 400), 400)).Play(pnlKeyboard, Animator2D.KnownProperties.Location);
        }
        private void txtBuscar_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
        }
        private void txtBuscar_MouseHover(object sender, EventArgs e)
        {
            if (TypeKeyboadAlphabetic)
                ShowKeyboardAlphabetic();
            else
                ShowKeyboardQwerty();
        }

        private void gridView2_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle == 0)
            {
                //e.Appearance.BackColor = Color.Blue;
                //e.Appearance.BackColor2 = Color.White;
                //e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, 10, FontStyle.Bold);
                //e.Appearance.ForeColor = Color.White;
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateKeyAndInsertCoin(e.KeyChar.ToString());
            e.Handled = true;
        }

        private void gCanciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateKeyAndInsertCoin(e.KeyChar.ToString());
        }
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateKeyAndInsertCoin(e.KeyChar.ToString());
        }
        void ValidateKeyAndInsertCoin(string sPointKey)
        {
            if (!sActionCoin.ToUpper().Contains("CLICK"))
            {
                if (sPointKey.Equals(sActionCoin))
                {
                InsertCoin();
            }
        }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
        }
        private void btnTop20Ranking_Click(object sender, EventArgs e)
        {
            if (TipoSeleccionado == TipoListado.Todas)
            {
                ChangeToTop20();
            }
            else
            {
                if (TipoSeleccionado == TipoListado.Top20)
                {
                    ChangeToNewAdded();
                }
                else
                {
                    if (TipoSeleccionado == TipoListado.RecientementeAgregados)
                    {
                        ChangeToAllSongs();
                    }
                }
            }
            this.Refresh();
        }
        void ChangeToAllSongs()
        {
            btnTop20Ranking.Text = sTop20;
            gbCanciones.Text = sTodasCanciones;
            AllSongs();
            TipoSeleccionado = TipoListado.Todas;
        }
        void ChangeToTop20()
        {
            btnTop20Ranking.Text = sNuevasAdicionadas;
            gbCanciones.Text = sTop20;
            Top20Songs();
            TipoSeleccionado = TipoListado.Top20;
        }
        void ChangeToNewAdded()
        {
            btnTop20Ranking.Text = sTodasCanciones;
            gbCanciones.Text = sNuevasAdicionadas;
            NewSongsAdded();
            TipoSeleccionado = TipoListado.RecientementeAgregados;
        }
        void AllSongs()
        {
            if (bReadSongsEveryEvent)
                ReadSongsFromDisk();
            else
            {
                ReadSongsFromMemoryRam();
            }
        }
        void ReadSongsFromDisk()
        {
            obj.GenerateListOfSongs(@sDireccionCanciones, @sListaCanciones, sExtensiones, bTipoListado, bResetTopSongs, bMakeToUpper);
            if (bTipoListado)
                gCanciones.DataSource = obj.ReadList(sListaCanciones);
            else
            {
                gCanciones.DataSource = obj.ReadList(sListaCanciones);
                ((GridView)gCanciones.MainView).Columns[1].Caption = "ARTISTA/NOMBRE CANCION";
                ((GridView)gCanciones.MainView).Columns[0].Visible = false;
                ((GridView)gListaEspera.MainView).Columns[1].Caption = "ARTISTA/NOMBRE CANCION";
                ((GridView)gListaEspera.MainView).Columns[0].Visible = false;
            }
        }
        void ReadSongsFromMemoryRam()
        {
            if (bTipoListado)
                gCanciones.DataSource = dsAllSongs;
            else
            {
                gCanciones.DataSource = dsAllSongs;
                ((GridView)gCanciones.MainView).Columns[1].Caption = "ARTISTA/NOMBRE CANCION";
                ((GridView)gCanciones.MainView).Columns[0].Visible = false;
                ((GridView)gListaEspera.MainView).Columns[1].Caption = "ARTISTA/NOMBRE CANCION";
                ((GridView)gListaEspera.MainView).Columns[0].Visible = false;
            }
        }
        void Top20Songs()
        {
            string sSelectedSong = string.Empty;
            if (TipoSeleccionado == TipoListado.Top20)
            {
                if (((GridView)gCanciones.MainView).GetFocusedRow() != null)
                {
                    sSelectedSong = ((GridView)gCanciones.MainView).GetFocusedValue().ToString();
                }
            }
            //int focusedRow = ((GridView)gCanciones.MainView).FocusedRowHandle;
            gCanciones.BeginUpdate();
            obj.GenerateListOfSongs(@sDireccionCanciones, @sListaCanciones, sExtensiones, bTipoListado, bResetTopSongs, bMakeToUpper);
            if (bTipoListado)
                gCanciones.DataSource = obj.ReadListTop20(sListaCanciones);
            else
            {
                gCanciones.DataSource = obj.ReadListTop20(sListaCanciones);
                ((GridView)gCanciones.MainView).Columns[1].Caption = "ARTISTA/NOMBRE CANCION";
                ((GridView)gCanciones.MainView).Columns[0].Visible = false;
                ((GridView)gListaEspera.MainView).Columns[1].Caption = "ARTISTA/NOMBRE CANCION";
                ((GridView)gListaEspera.MainView).Columns[0].Visible = false;
            }
            gCanciones.EndUpdate();
            //((GridView)gCanciones.MainView).FocusedRowHandle = focusedRow;
            if (sSelectedSong != string.Empty)
            {
                int rowHandle = ((GridView)gCanciones.MainView).LocateByValue("NombreCancion", sSelectedSong);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    ((GridView)gCanciones.MainView).FocusedRowHandle = rowHandle;
            }
        }
        void NewSongsAdded()
        {
            string sSelectedSong = string.Empty;
            if (TipoSeleccionado == TipoListado.RecientementeAgregados)
            {
                if (((GridView)gCanciones.MainView).GetFocusedRow() != null)
                {
                    sSelectedSong = ((GridView)gCanciones.MainView).GetFocusedValue().ToString();
                }
            }
            //int focusedRow = ((GridView)gCanciones.MainView).FocusedRowHandle;
            gCanciones.BeginUpdate();
            obj.GenerateListOfSongs(@sDireccionCanciones, @sListaCanciones, sExtensiones, bTipoListado, bResetTopSongs, bMakeToUpper);
            if (bTipoListado)
                gCanciones.DataSource = obj.ReadListNewAdded20(sListaCanciones);
            else
            {
                gCanciones.DataSource = obj.ReadListNewAdded20(sListaCanciones);
                ((GridView)gCanciones.MainView).Columns[1].Caption = "ARTISTA/NOMBRE CANCION";
                ((GridView)gCanciones.MainView).Columns[0].Visible = false;
                ((GridView)gListaEspera.MainView).Columns[1].Caption = "ARTISTA/NOMBRE CANCION";
                ((GridView)gListaEspera.MainView).Columns[0].Visible = false;
            }
            gCanciones.EndUpdate();
            //((GridView)gCanciones.MainView).FocusedRowHandle = focusedRow;
            if (sSelectedSong != string.Empty)
            {
                int rowHandle = ((GridView)gCanciones.MainView).LocateByValue("NombreCancion", sSelectedSong);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    ((GridView)gCanciones.MainView).FocusedRowHandle = rowHandle;
            }
        }
        private void lblCreditos_Click(object sender, EventArgs e)
        {
            
        }
        void AddClicksAllControls(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c.Controls.Count > 0)
                {
                    AddClicksAllControls(c.Controls);
                }
                c.MouseDown += new System.Windows.Forms.MouseEventHandler(this.All_MouseDown);
            }
        }
        private void All_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                InsertCoin();
        }

        private void wmpLogo_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            if (e.nButton == 2)
            {
                InsertCoin();
            }
            return;
        }

        private void VistaPrevia_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            if (e.nButton == 2)
            {
                InsertCoin();
            }
            return;
        }

        private void lblCreditos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int iCantidadCanciones = RockoTouch.MonitorDrives.CopyDataFromUsbToHardDrive(@sDireccionCanciones, @sExtensiones);
            if (iCantidadCanciones > 0)
                MessageBox.Show(iCantidadCanciones.ToString(), "CANTIDAD CANCIONES IMPORTADAS ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AllSongs();
        }

        private void gridView1_TopRowChanged(object sender, EventArgs e)
        {
            isScrolling = true;
        }

        private void VistaPrevia_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((WMPLib.WMPPlayState)e.newState == WMPLib.WMPPlayState.wmppsStopped)
            {
                if (ReproductorSegundaPantalla.iCantidadCanciones == 0)
                {
                    if (VistaPrevia.playState != WMPLib.WMPPlayState.wmppsPlaying)
                    {
                        ReproductorSegundaPantalla.Reproductor2.settings.volume = 10;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void cbSkins_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var cBox = sender as DevExpress.XtraEditors.ComboBoxEdit;
                ChangeTemplateControls(cBox.SelectedItem.ToString());
                ChangeTemplateControlsDinamically(cBox.SelectedItem.ToString());
                if (ReproductorSegundaPantalla.iCantidadCanciones == 0)
                    ReproductorSegundaPantalla.Reproductor2.Ctlcontrols.stop();
                ReproductorSegundaPantalla.LoadImagesList();
                this.Refresh();
                this.Invalidate();
            }
            catch (Exception err) { }
        }

        private void tbVolumeControl_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            ReproductorSegundaPantalla.Reproductor2.settings.volume = tbVolumeControl.Value * 10;
        }
    }
}
