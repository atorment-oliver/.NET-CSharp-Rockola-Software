using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Scrolling;

namespace RockoLocoPlayer
{
    public class MyLayoutView :LayoutView {
        public MyLayoutView() : base() { }
        public MyLayoutView(GridControl grid) : base(grid) { }

        internal const string MyLayoutViewName = "MyLayoutView";
        protected override string ViewName { get { return MyLayoutViewName; } }

        protected override ScrollInfo CreateScrollInfo() {
            return new MyScrollInfo(this) ;
        }
    }

    public class MyLayoutViewInfoRegistrator :LayoutViewInfoRegistrator {
        public MyLayoutViewInfoRegistrator() : base() { }

        public override string ViewName { get { return MyLayoutView.MyLayoutViewName; } }

        public override BaseView CreateView(GridControl grid) {
            return new MyLayoutView(grid);
        }
    }
}
