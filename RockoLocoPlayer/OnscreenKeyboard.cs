using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace RockoLocoPlayer
{
    public partial class OnscreenKeyboard : Form
    {
        public OnscreenKeyboard()
        {
            InitializeComponent();
            CreateKeyboard();
        }

        private void OnscreenKeyboard_Load(object sender, EventArgs e)
        {
            int WidthOfMain = Application.OpenForms["MainForm"].Width;
            int HeightofMain = Application.OpenForms["MainForm"].Height;
            this.Width = WidthOfMain / 2;
            this.Height = HeightofMain / 3;
        }
        void CreateKeyboard()
        {
            DevExpress.XtraEditors.SimpleButton BtnCap;
            int Lft = 10;
            int Top = 60;
            for (int i = Convert.ToInt32('A'); i <= Convert.ToInt32('Z'); i++)
            {
                BtnCap = new DevExpress.XtraEditors.SimpleButton();
                var _with1 = BtnCap;

                _with1.Name = "BtnC_" + Convert.ToChar(i);
                _with1.Text = "&" + Convert.ToChar(i);

                _with1.Size = new Size(50, 50);
                _with1.Location = new Point(Lft, Top);

                _with1.Tag = Convert.ToChar(i);

                Lft = Lft + 53;


                if (Convert.ToChar(i).ToString() == "I")
                {
                    Top = Top + 53;
                    Lft = 10;

                }
                if (Convert.ToChar(i).ToString() == "R")
                {
                    Top = Top + 53;
                    Lft = 10;

                }
                BtnCap.LookAndFeel.UseDefaultLookAndFeel = false;
                BtnCap.LookAndFeel.SkinName = "McSkin";
                _with1.Visible = true;


                this.Controls.Add(BtnCap);

                BtnCap.Click += BtnCap_click;

            }
        }

        private void BtnCap_click(System.Object sender, System.EventArgs e)
        {
            ((MainForm)Application.OpenForms["MainForm"]).txtBuscar.Text = ((MainForm)Application.OpenForms["MainForm"]).txtBuscar.Text + ((DevExpress.XtraEditors.SimpleButton)sender).Tag;
            //TextBox1.Text = TextBox1.Text + ((Button)sender).Tag;
        }
    }
}
