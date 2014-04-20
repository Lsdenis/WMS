using System;
using System.Windows;
using System.Windows.Markup;
using WMS.BusinessLogic.DataModel;

namespace WMS.CustomControls.Controls.BaseWindow
{
    public class BaseWindow : Window
    {
        protected WarehouseEntities Context = new WarehouseEntities();

        protected override void OnClosed(EventArgs e)
        {
            Context.SaveChanges();
            Context.Dispose();
            base.OnClosed(e);
        }
    }
}
