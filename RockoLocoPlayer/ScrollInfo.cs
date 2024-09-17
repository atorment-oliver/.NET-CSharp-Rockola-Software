using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using System.Windows.Forms;

namespace RockoLocoPlayer {
    public class MyScrollInfo :ScrollInfo {
        public MyScrollInfo(BaseView view) : base(view) { }

        protected override VCrkScrollBar CreateVScroll() {
            return new MyVCrkScrollBar(this);
        }

        protected override HCrkScrollBar CreateHScroll() {
            return new MyHCrkScrollBar(this);
        }

        public override int VScrollSize
        {
            get { return 50; }
        }

    
    }

    public class MyVCrkScrollBar :VCrkScrollBar {
        public MyVCrkScrollBar(ScrollInfo scrollInfo) : base(scrollInfo) { }

        protected override ScrollBarViewInfo CreateScrollBarViewInfo() {
            return new MyScrollBarViewinfo(this);
        }
    }

    public class MyHCrkScrollBar :HCrkScrollBar {
        public MyHCrkScrollBar(ScrollInfo scrollInfo) : base(scrollInfo) { }

        protected override ScrollBarViewInfo CreateScrollBarViewInfo() {
            return new MyScrollBarViewinfo(this);
        }
    }

    public class MyScrollBarViewinfo :ScrollBarViewInfo {
        public MyScrollBarViewinfo(IScrollBar scrollBar) : base(scrollBar) { }

        public override int ButtonWidth {
            get { return SystemInformation.VerticalScrollBarArrowHeight * 2; } 
        }
    }
}