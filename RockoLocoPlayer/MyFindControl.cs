using System;
using DevExpress.XtraGrid.Controls;
using System.Drawing;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.Utils;

namespace RockoLocoPlayer
{
    public class MyFindControl : FindControl
    {
        public MyFindControl(ColumnView view, object properties)
            : base(view, properties)
        {
            CustomizeControl();
        }


        private void CustomizeControl()
        {
            CustomizeButtons();
            CustomizeEditor();
            CustomizeLayoutControl();
        }
        private Control FindControl(string controlName)
        {
            return layoutControl1.GetControlByName(controlName);
        }
        private void CustomizeButtons()
        {
            FindControl("btClear").MinimumSize = new Size(100, 40);
            FindControl("btClear").Font = new Font(AppearanceObject.DefaultFont, FontStyle.Bold);
            FindControl("btFind").MinimumSize = new Size(300, 60);
            FindControl("btFind").Font = new Font(AppearanceObject.DefaultFont, FontStyle.Bold);
            FindControl("btFind").Visible = false;
        }
        private void CustomizeEditor()
        {
            ButtonEdit be = FindControl("teFind") as ButtonEdit;
            //new EditorButton(ButtonPredefines.Ellipsis)
            //be.Properties.Buttons.Add();
            be.ButtonClick += be_ButtonClick;
            be.AutoSizeInLayoutControl = false;
            be.Size = new Size(400, 100);
            be.Properties.NullValuePrompt = "";
            be.Properties.NullValuePromptShowForEmptyValue = true;
            be.Font = new Font(FontFamily.GenericSansSerif, 16.0F, FontStyle.Regular);
        }

        void be_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Ellipsis)
                using (Form form = new Form())
                {
                    form.ShowDialog();
                }
        }
        private void CustomizeLayoutControl()
        {
            layoutControl1.AllowCustomizationMenu = true;
        }
    }
}