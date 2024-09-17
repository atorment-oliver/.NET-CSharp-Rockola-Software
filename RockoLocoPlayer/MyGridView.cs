using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Registrator;

namespace RockoLocoPlayer {
    public class MyGridView :GridView {
        public MyGridView() : base() { }
        public MyGridView(GridControl grid) : base(grid) { }

        internal const string MyGridViewName = "MyGridView";
        protected override string ViewName { get { return MyGridViewName; } }

        protected override ScrollInfo CreateScrollInfo() {
            return new MyScrollInfo(this);
        }
        protected override DevExpress.XtraGrid.Controls.FindControl CreateFindPanel(object findPanelProperties)
        {
            return new MyFindControl(this, findPanelProperties);
        }
    }

    public class MyGridViewInfoRegistrator :GridInfoRegistrator {
        public MyGridViewInfoRegistrator() : base() { }

        public override string ViewName { get { return MyGridView.MyGridViewName; } }

        public override BaseView CreateView(GridControl grid) {
            return new MyGridView(grid);
        }
    }
}
