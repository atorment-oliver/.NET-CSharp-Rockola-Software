namespace WindowsFormsApplication1
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.gCanciones = new RockoLocoPlayer.MyGridControl();
            this.gridView1 = new RockoLocoPlayer.MyGridView();
            this.Artista = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NombreCancion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Direccion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutView = new RockoLocoPlayer.MyLayoutView();
            this.gListaEspera = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ArtistaEspera = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NombreCancionEspera = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.VistaPrevia = new AxWMPLib.AxWindowsMediaPlayer();
            this.ShutOffTimer = new System.Windows.Forms.Timer(this.components);
            this.btnLeerCanciones = new System.Windows.Forms.Button();
            this.gbCanciones = new System.Windows.Forms.GroupBox();
            this.pnlKeyboard = new DevExpress.XtraEditors.PanelControl();
            this.grpListaEspera = new System.Windows.Forms.GroupBox();
            this.btnPreviewFloating = new DevExpress.XtraEditors.SimpleButton();
            this.AnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.btnTeclado = new DevExpress.XtraEditors.SimpleButton();
            this.pnlOriginal = new System.Windows.Forms.Panel();
            this.pnlAlphabet = new System.Windows.Forms.Panel();
            this.Reproductor = new AxWMPLib.AxWindowsMediaPlayer();
            this.pnlBuscar = new DevExpress.XtraEditors.PanelControl();
            this.txtLimpiar = new DevExpress.XtraEditors.SimpleButton();
            this.txtBuscar = new RockoLocoPlayer.WaterMarkTextBox();
            this.pnlKeyboardAlfhabet = new DevExpress.XtraEditors.PanelControl();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblCreditos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbarMedia = new DevExpress.XtraEditors.ProgressBarControl();
            this.pnlVistaPrevia = new System.Windows.Forms.Panel();
            this.clickEffect1 = new BubbleEffect.ClickEffect(this.components);
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.wmpLogo = new AxWMPLib.AxWindowsMediaPlayer();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnTop20Ranking = new DevExpress.XtraEditors.SimpleButton();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.cbSkins = new DevExpress.XtraEditors.ComboBoxEdit();
            this.imgArtist = new System.Windows.Forms.PictureBox();
            this.tbVolumeControl = new DevExpress.XtraEditors.TrackBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.gCanciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gListaEspera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VistaPrevia)).BeginInit();
            this.gbCanciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKeyboard)).BeginInit();
            this.grpListaEspera.SuspendLayout();
            this.pnlAlphabet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Reproductor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBuscar)).BeginInit();
            this.pnlBuscar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKeyboardAlfhabet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbarMedia.Properties)).BeginInit();
            this.pnlVistaPrevia.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmpLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSkins.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgArtist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumeControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumeControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(713, 537);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 71);
            this.button1.TabIndex = 0;
            this.button1.Text = "QUINTO";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gCanciones
            // 
            this.gCanciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gCanciones.Location = new System.Drawing.Point(6, 19);
            this.gCanciones.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gCanciones.MainView = this.gridView1;
            this.gCanciones.Name = "gCanciones";
            this.gCanciones.Size = new System.Drawing.Size(675, 289);
            this.gCanciones.TabIndex = 1;
            this.gCanciones.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gCanciones.Load += new System.EventHandler(this.gridControl1_Load_2);
            this.gCanciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gCanciones_KeyPress);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FilterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.FilterPanel.Options.UseFont = true;
            this.gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Artista,
            this.NombreCancion,
            this.Direccion});
            this.gridView1.GridControl = this.gCanciones;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Default;
            this.gridView1.OptionsDetail.EnableMasterViewMode = false;
            this.gridView1.OptionsFind.FindNullPrompt = "Inserta el artista o cancion a buscar";
            this.gridView1.OptionsFind.HighlightFindResults = false;
            this.gridView1.OptionsFind.ShowFindButton = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.TopRowChanged += new System.EventHandler(this.gridView1_TopRowChanged);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.Layout += new System.EventHandler(this.gridView1_Layout);
            // 
            // Artista
            // 
            this.Artista.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.Artista.AppearanceCell.Options.UseFont = true;
            this.Artista.AppearanceHeader.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Artista.AppearanceHeader.Options.UseFont = true;
            this.Artista.Caption = "Artista";
            this.Artista.FieldName = "Artista";
            this.Artista.MaxWidth = 300;
            this.Artista.MinWidth = 100;
            this.Artista.Name = "Artista";
            this.Artista.OptionsColumn.AllowEdit = false;
            this.Artista.OptionsFilter.AllowAutoFilter = false;
            this.Artista.OptionsFilter.AllowFilter = false;
            this.Artista.Visible = true;
            this.Artista.VisibleIndex = 0;
            this.Artista.Width = 300;
            // 
            // NombreCancion
            // 
            this.NombreCancion.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.NombreCancion.AppearanceCell.Options.UseFont = true;
            this.NombreCancion.AppearanceHeader.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.NombreCancion.AppearanceHeader.Options.UseFont = true;
            this.NombreCancion.Caption = "Nombre Cancion";
            this.NombreCancion.FieldName = "NombreCancion";
            this.NombreCancion.Name = "NombreCancion";
            this.NombreCancion.OptionsColumn.AllowEdit = false;
            this.NombreCancion.OptionsFilter.AllowAutoFilter = false;
            this.NombreCancion.OptionsFilter.AllowFilter = false;
            this.NombreCancion.Visible = true;
            this.NombreCancion.VisibleIndex = 1;
            this.NombreCancion.Width = 357;
            // 
            // Direccion
            // 
            this.Direccion.Caption = "Direccion";
            this.Direccion.FieldName = "Direccion";
            this.Direccion.Name = "Direccion";
            // 
            // layoutView
            // 
            this.layoutView.Name = "layoutView";
            this.layoutView.TemplateCard = null;
            // 
            // gListaEspera
            // 
            this.gListaEspera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gListaEspera.Location = new System.Drawing.Point(6, 22);
            this.gListaEspera.MainView = this.gridView2;
            this.gListaEspera.Name = "gListaEspera";
            this.gListaEspera.Size = new System.Drawing.Size(675, 189);
            this.gListaEspera.TabIndex = 2;
            this.gListaEspera.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gListaEspera.Load += new System.EventHandler(this.gListaEspera_Load);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ArtistaEspera,
            this.NombreCancionEspera});
            this.gridView2.GridControl = this.gListaEspera;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.AutoSelectAllInEditor = false;
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Default;
            this.gridView2.OptionsMenu.EnableColumnMenu = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridView2.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridView2.OptionsSelection.UseIndicatorForSelection = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView2_RowStyle);
            // 
            // ArtistaEspera
            // 
            this.ArtistaEspera.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ArtistaEspera.AppearanceCell.Options.UseFont = true;
            this.ArtistaEspera.AppearanceHeader.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.ArtistaEspera.AppearanceHeader.Options.UseFont = true;
            this.ArtistaEspera.Caption = "Artista";
            this.ArtistaEspera.FieldName = "Artista";
            this.ArtistaEspera.MaxWidth = 300;
            this.ArtistaEspera.MinWidth = 100;
            this.ArtistaEspera.Name = "ArtistaEspera";
            this.ArtistaEspera.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.ArtistaEspera.OptionsFilter.AllowAutoFilter = false;
            this.ArtistaEspera.OptionsFilter.AllowFilter = false;
            this.ArtistaEspera.Visible = true;
            this.ArtistaEspera.VisibleIndex = 0;
            this.ArtistaEspera.Width = 200;
            // 
            // NombreCancionEspera
            // 
            this.NombreCancionEspera.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.NombreCancionEspera.AppearanceCell.Options.UseFont = true;
            this.NombreCancionEspera.AppearanceHeader.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.NombreCancionEspera.AppearanceHeader.Options.UseFont = true;
            this.NombreCancionEspera.Caption = "Nombre Cancion";
            this.NombreCancionEspera.FieldName = "NombreCancion";
            this.NombreCancionEspera.Name = "NombreCancionEspera";
            this.NombreCancionEspera.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.NombreCancionEspera.OptionsFilter.AllowAutoFilter = false;
            this.NombreCancionEspera.OptionsFilter.AllowFilter = false;
            this.NombreCancionEspera.Visible = true;
            this.NombreCancionEspera.VisibleIndex = 1;
            this.NombreCancionEspera.Width = 514;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VistaPrevia
            // 
            this.VistaPrevia.Enabled = true;
            this.VistaPrevia.Location = new System.Drawing.Point(3, 0);
            this.VistaPrevia.Name = "VistaPrevia";
            this.VistaPrevia.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("VistaPrevia.OcxState")));
            this.VistaPrevia.Size = new System.Drawing.Size(306, 223);
            this.VistaPrevia.TabIndex = 4;
            this.VistaPrevia.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(this.VistaPrevia_OpenStateChange);
            this.VistaPrevia.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.VistaPrevia_PlayStateChange);
            this.VistaPrevia.MouseDownEvent += new AxWMPLib._WMPOCXEvents_MouseDownEventHandler(this.VistaPrevia_MouseDownEvent);
            // 
            // ShutOffTimer
            // 
            this.ShutOffTimer.Tick += new System.EventHandler(this.ShutOffTimer_Tick);
            // 
            // btnLeerCanciones
            // 
            this.btnLeerCanciones.Location = new System.Drawing.Point(826, 567);
            this.btnLeerCanciones.Name = "btnLeerCanciones";
            this.btnLeerCanciones.Size = new System.Drawing.Size(106, 41);
            this.btnLeerCanciones.TabIndex = 6;
            this.btnLeerCanciones.Text = "Leer nuevas canciones";
            this.btnLeerCanciones.UseVisualStyleBackColor = true;
            this.btnLeerCanciones.Visible = false;
            this.btnLeerCanciones.Click += new System.EventHandler(this.btnLeerCanciones_Click);
            // 
            // gbCanciones
            // 
            this.gbCanciones.Controls.Add(this.gCanciones);
            this.gbCanciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCanciones.Location = new System.Drawing.Point(12, 73);
            this.gbCanciones.Name = "gbCanciones";
            this.gbCanciones.Size = new System.Drawing.Size(695, 314);
            this.gbCanciones.TabIndex = 7;
            this.gbCanciones.TabStop = false;
            this.gbCanciones.Text = "LISTA DE CANCIONES";
            // 
            // pnlKeyboard
            // 
            this.pnlKeyboard.Location = new System.Drawing.Point(250, 1500);
            this.pnlKeyboard.Name = "pnlKeyboard";
            this.pnlKeyboard.Size = new System.Drawing.Size(0, 0);
            this.pnlKeyboard.TabIndex = 16;
            // 
            // grpListaEspera
            // 
            this.grpListaEspera.Controls.Add(this.gListaEspera);
            this.grpListaEspera.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpListaEspera.Location = new System.Drawing.Point(12, 390);
            this.grpListaEspera.Name = "grpListaEspera";
            this.grpListaEspera.Size = new System.Drawing.Size(695, 218);
            this.grpListaEspera.TabIndex = 8;
            this.grpListaEspera.TabStop = false;
            this.grpListaEspera.Text = "LISTA DE ESPERA";
            // 
            // btnPreviewFloating
            // 
            this.btnPreviewFloating.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnPreviewFloating.Appearance.Options.UseFont = true;
            this.btnPreviewFloating.Appearance.Options.UseTextOptions = true;
            this.btnPreviewFloating.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnPreviewFloating.Location = new System.Drawing.Point(1071, 590);
            this.btnPreviewFloating.Name = "btnPreviewFloating";
            this.btnPreviewFloating.Size = new System.Drawing.Size(129, 63);
            this.btnPreviewFloating.TabIndex = 10;
            this.btnPreviewFloating.Text = "VISTA PREVIA FLOTANTE";
            this.btnPreviewFloating.Visible = false;
            this.btnPreviewFloating.Click += new System.EventHandler(this.btnPreviewFloating_Click);
            // 
            // AnimationTimer
            // 
            this.AnimationTimer.Tick += new System.EventHandler(this.AnimationTimer_Tick);
            // 
            // btnTeclado
            // 
            this.btnTeclado.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.btnTeclado.Appearance.Options.UseFont = true;
            this.btnTeclado.Appearance.Options.UseTextOptions = true;
            this.btnTeclado.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnTeclado.Location = new System.Drawing.Point(718, 363);
            this.btnTeclado.Name = "btnTeclado";
            this.btnTeclado.Size = new System.Drawing.Size(128, 63);
            this.btnTeclado.TabIndex = 12;
            this.btnTeclado.Text = "TECLADO QWERTY";
            this.btnTeclado.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlOriginal
            // 
            this.pnlOriginal.Location = new System.Drawing.Point(-290, 12);
            this.pnlOriginal.Name = "pnlOriginal";
            this.pnlOriginal.Size = new System.Drawing.Size(200, 100);
            this.pnlOriginal.TabIndex = 13;
            // 
            // pnlAlphabet
            // 
            this.pnlAlphabet.BackColor = System.Drawing.Color.Transparent;
            this.pnlAlphabet.Controls.Add(this.Reproductor);
            this.pnlAlphabet.Location = new System.Drawing.Point(1028, 12);
            this.pnlAlphabet.Name = "pnlAlphabet";
            this.pnlAlphabet.Size = new System.Drawing.Size(76, 537);
            this.pnlAlphabet.TabIndex = 14;
            // 
            // Reproductor
            // 
            this.Reproductor.Enabled = true;
            this.Reproductor.Location = new System.Drawing.Point(71, 233);
            this.Reproductor.Name = "Reproductor";
            this.Reproductor.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Reproductor.OcxState")));
            this.Reproductor.Size = new System.Drawing.Size(32, 34);
            this.Reproductor.TabIndex = 9;
            this.Reproductor.Visible = false;
            // 
            // pnlBuscar
            // 
            this.pnlBuscar.Controls.Add(this.txtLimpiar);
            this.pnlBuscar.Controls.Add(this.txtBuscar);
            this.pnlBuscar.Location = new System.Drawing.Point(12, 12);
            this.pnlBuscar.Name = "pnlBuscar";
            this.pnlBuscar.Size = new System.Drawing.Size(695, 55);
            this.pnlBuscar.TabIndex = 15;
            // 
            // txtLimpiar
            // 
            this.txtLimpiar.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.txtLimpiar.Appearance.Options.UseFont = true;
            this.txtLimpiar.Appearance.Options.UseTextOptions = true;
            this.txtLimpiar.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.txtLimpiar.Location = new System.Drawing.Point(550, 8);
            this.txtLimpiar.Name = "txtLimpiar";
            this.txtLimpiar.Size = new System.Drawing.Size(131, 41);
            this.txtLimpiar.TabIndex = 16;
            this.txtLimpiar.Text = "LIMPIAR";
            this.txtLimpiar.Click += new System.EventHandler(this.txtLimpiar_Click_1);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Tahoma", 22F);
            this.txtBuscar.Location = new System.Drawing.Point(6, 6);
            this.txtBuscar.MaxLength = 300;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.ShortcutsEnabled = false;
            this.txtBuscar.Size = new System.Drawing.Size(538, 43);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtBuscar.WaterMarkText = "Ingrese artista o cancion a buscar";
            this.txtBuscar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtBuscar_MouseClick);
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            this.txtBuscar.MouseHover += new System.EventHandler(this.txtBuscar_MouseHover);
            // 
            // pnlKeyboardAlfhabet
            // 
            this.pnlKeyboardAlfhabet.Location = new System.Drawing.Point(250, 1500);
            this.pnlKeyboardAlfhabet.Name = "pnlKeyboardAlfhabet";
            this.pnlKeyboardAlfhabet.Size = new System.Drawing.Size(0, 0);
            this.pnlKeyboardAlfhabet.TabIndex = 17;
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercent.Location = new System.Drawing.Point(713, 283);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(30, 31);
            this.lblPercent.TabIndex = 19;
            this.lblPercent.Text = "0";
            // 
            // lblCreditos
            // 
            this.lblCreditos.AutoSize = true;
            this.lblCreditos.BackColor = System.Drawing.SystemColors.WindowText;
            this.lblCreditos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCreditos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCreditos.Location = new System.Drawing.Point(843, 241);
            this.lblCreditos.Name = "lblCreditos";
            this.lblCreditos.Size = new System.Drawing.Size(32, 33);
            this.lblCreditos.TabIndex = 20;
            this.lblCreditos.Text = "0";
            this.lblCreditos.Click += new System.EventHandler(this.lblCreditos_Click);
            this.lblCreditos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblCreditos_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.MenuText;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(713, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "CREDITOS:";
            // 
            // pbarMedia
            // 
            this.pbarMedia.Location = new System.Drawing.Point(718, 317);
            this.pbarMedia.Name = "pbarMedia";
            this.pbarMedia.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold);
            this.pbarMedia.Properties.ShowTitle = true;
            this.pbarMedia.Size = new System.Drawing.Size(304, 31);
            this.pbarMedia.TabIndex = 22;
            // 
            // pnlVistaPrevia
            // 
            this.pnlVistaPrevia.Controls.Add(this.VistaPrevia);
            this.pnlVistaPrevia.Location = new System.Drawing.Point(718, 12);
            this.pnlVistaPrevia.Name = "pnlVistaPrevia";
            this.pnlVistaPrevia.Size = new System.Drawing.Size(305, 226);
            this.pnlVistaPrevia.TabIndex = 23;
            // 
            // clickEffect1
            // 
            this.clickEffect1.ClickControl = this.button1;
            this.clickEffect1.Speed = 10;
            // 
            // pnlLogo
            // 
            this.pnlLogo.Controls.Add(this.wmpLogo);
            this.pnlLogo.Location = new System.Drawing.Point(718, 12);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(309, 226);
            this.pnlLogo.TabIndex = 25;
            this.pnlLogo.Visible = false;
            // 
            // wmpLogo
            // 
            this.wmpLogo.Enabled = true;
            this.wmpLogo.Location = new System.Drawing.Point(-5, -25);
            this.wmpLogo.Name = "wmpLogo";
            this.wmpLogo.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpLogo.OcxState")));
            this.wmpLogo.Size = new System.Drawing.Size(320, 257);
            this.wmpLogo.TabIndex = 0;
            this.wmpLogo.MouseDownEvent += new AxWMPLib._WMPOCXEvents_MouseDownEventHandler(this.wmpLogo_MouseDownEvent);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnTop20Ranking
            // 
            this.btnTop20Ranking.Appearance.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.btnTop20Ranking.Appearance.Options.UseFont = true;
            this.btnTop20Ranking.Appearance.Options.UseTextOptions = true;
            this.btnTop20Ranking.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btnTop20Ranking.Location = new System.Drawing.Point(894, 363);
            this.btnTop20Ranking.Name = "btnTop20Ranking";
            this.btnTop20Ranking.Size = new System.Drawing.Size(128, 63);
            this.btnTop20Ranking.TabIndex = 26;
            this.btnTop20Ranking.Text = "TOP 20 RANKING";
            this.btnTop20Ranking.Click += new System.EventHandler(this.btnTop20Ranking_Click);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // cbSkins
            // 
            this.cbSkins.Location = new System.Drawing.Point(721, 502);
            this.cbSkins.Name = "cbSkins";
            this.cbSkins.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSkins.Properties.Items.AddRange(new object[] {
            "sSkin1",
            "sSkin2",
            "sSkin3",
            "sSkin4",
            "sSkin5",
            "sSkin6",
            "sSkin7",
            "sSkin8",
            "sSkin9",
            "sSkin10",
            "sSkin11",
            "sSkin12",
            "sSkin13",
            "sSkin14",
            "sSkin15",
            "sSkin16",
            "sSkin17",
            "sSkin18",
            "sSkin19",
            "sSkin20",
            "sSkin21",
            "sSkin22",
            "sSkin23",
            "sSkin24",
            "sSkin25",
            "sSkin26",
            "sSkin27",
            "sSkin28",
            "sSkin29",
            "sSkin30",
            "sSkin31",
            "sSkin32",
            "sSkin33",
            "sSkin34",
            "sSkin35"});
            this.cbSkins.Size = new System.Drawing.Size(100, 20);
            this.cbSkins.TabIndex = 28;
            this.cbSkins.SelectedIndexChanged += new System.EventHandler(this.cbSkins_SelectedIndexChanged);
            // 
            // imgArtist
            // 
            this.imgArtist.Location = new System.Drawing.Point(718, 442);
            this.imgArtist.Name = "imgArtist";
            this.imgArtist.Size = new System.Drawing.Size(304, 166);
            this.imgArtist.TabIndex = 29;
            this.imgArtist.TabStop = false;
            // 
            // tbVolumeControl
            // 
            this.tbVolumeControl.EditValue = 10;
            this.tbVolumeControl.Location = new System.Drawing.Point(714, 276);
            this.tbVolumeControl.Name = "tbVolumeControl";
            this.tbVolumeControl.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.tbVolumeControl.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tbVolumeControl.Size = new System.Drawing.Size(308, 45);
            this.tbVolumeControl.TabIndex = 30;
            this.tbVolumeControl.Value = 10;
            this.tbVolumeControl.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.tbVolumeControl_EditValueChanging);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1121, 620);
            this.Controls.Add(this.tbVolumeControl);
            this.Controls.Add(this.cbSkins);
            this.Controls.Add(this.pnlLogo);
            this.Controls.Add(this.pnlVistaPrevia);
            this.Controls.Add(this.pbarMedia);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCreditos);
            this.Controls.Add(this.pnlKeyboardAlfhabet);
            this.Controls.Add(this.pnlKeyboard);
            this.Controls.Add(this.pnlBuscar);
            this.Controls.Add(this.pnlAlphabet);
            this.Controls.Add(this.pnlOriginal);
            this.Controls.Add(this.btnTeclado);
            this.Controls.Add(this.btnPreviewFloating);
            this.Controls.Add(this.grpListaEspera);
            this.Controls.Add(this.gbCanciones);
            this.Controls.Add(this.btnLeerCanciones);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.btnTop20Ranking);
            this.Controls.Add(this.imgArtist);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "ROCKOTOUCH";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gCanciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gListaEspera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VistaPrevia)).EndInit();
            this.gbCanciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlKeyboard)).EndInit();
            this.grpListaEspera.ResumeLayout(false);
            this.pnlAlphabet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Reproductor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBuscar)).EndInit();
            this.pnlBuscar.ResumeLayout(false);
            this.pnlBuscar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKeyboardAlfhabet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbarMedia.Properties)).EndInit();
            this.pnlVistaPrevia.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wmpLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSkins.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgArtist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumeControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolumeControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private RockoLocoPlayer.MyGridControl gCanciones;
        private RockoLocoPlayer.MyGridView gridView1;
        private RockoLocoPlayer.MyLayoutView layoutView;
        private DevExpress.XtraGrid.Columns.GridColumn Artista;
        private DevExpress.XtraGrid.Columns.GridColumn NombreCancion;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn Direccion;
        private DevExpress.XtraGrid.Columns.GridColumn ArtistaEspera;
        private DevExpress.XtraGrid.Columns.GridColumn NombreCancionEspera;
        private System.Windows.Forms.Timer timer1;
        public AxWMPLib.AxWindowsMediaPlayer VistaPrevia;
        private System.Windows.Forms.Timer ShutOffTimer;
        private System.Windows.Forms.Button btnLeerCanciones;
        private System.Windows.Forms.GroupBox gbCanciones;
        public System.Windows.Forms.GroupBox grpListaEspera;
        public DevExpress.XtraGrid.GridControl gListaEspera;
        private DevExpress.XtraEditors.SimpleButton btnPreviewFloating;
        private System.Windows.Forms.Timer AnimationTimer;
        private DevExpress.XtraEditors.SimpleButton btnTeclado;
        private System.Windows.Forms.Panel pnlOriginal;
        private System.Windows.Forms.Panel pnlAlphabet;
        private DevExpress.XtraEditors.PanelControl pnlBuscar;
        public RockoLocoPlayer.WaterMarkTextBox txtBuscar;
        private DevExpress.XtraEditors.SimpleButton txtLimpiar;
        private DevExpress.XtraEditors.PanelControl pnlKeyboard;
        private DevExpress.XtraEditors.PanelControl pnlKeyboardAlfhabet;
        private AxWMPLib.AxWindowsMediaPlayer Reproductor;
        public System.Windows.Forms.Label lblPercent;
        public System.Windows.Forms.Label lblCreditos;
        public System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.ProgressBarControl pbarMedia;
        private System.Windows.Forms.Panel pnlVistaPrevia;
        private BubbleEffect.ClickEffect clickEffect1;
        private System.Windows.Forms.Panel pnlLogo;
        private AxWMPLib.AxWindowsMediaPlayer wmpLogo;
        private System.Windows.Forms.Timer timer2;
        private DevExpress.XtraEditors.SimpleButton btnTop20Ranking;
        private DevExpress.XtraEditors.ComboBoxEdit cbSkins;
        private System.Windows.Forms.PictureBox imgArtist;
        private DevExpress.XtraEditors.TrackBarControl tbVolumeControl;
    }
}

