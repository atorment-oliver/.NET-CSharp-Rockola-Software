using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;

namespace RockoLocoPlayer {
    public class MyGridControl :GridControl {
        public MyGridControl() : base() { }

        protected override void RegisterAvailableViewsCore(InfoCollection collection) {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new MyGridViewInfoRegistrator());
            collection.Add(new MyLayoutViewInfoRegistrator());
        }
    }
}