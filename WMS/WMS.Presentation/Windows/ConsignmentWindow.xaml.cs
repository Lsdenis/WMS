using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for ConsignmentWindow.xaml
	/// </summary>
	public partial class ConsignmentWindow : BaseWindow
	{
	    private Consignment _consignment = new Consignment();

	    public ConsignmentWindow()
		{
			InitializeComponent();
		    ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
            ucSaveAndClose.SaveAndCloseButtonClick = SaveAndCloseButtonClick;
		}

	    private void SaveAndCloseButtonClick(object sender, RoutedEventArgs routedEventArgs)
	    {
	        if (!IsConsignmentValid())
	        {
                MessageBox.Show("Name should not be empty.", "Warning!", MessageBoxButton.OK);
	            return;
	        }

	        GetConsignmentValues();



	        if (IsConsignmentExists())
	        {
	            MessageBox.Show("This consignment exists.", "Warning!", MessageBoxButton.OK);
	            return;
	        }

	        using (var uow = new UnitOfWork())
	        {
	            uow.Consignments.Add(_consignment);
                uow.Commit();
	        }

            Close();
	    }

	    private bool IsConsignmentValid()
	    {
	        return !string.IsNullOrWhiteSpace(txtName.Text);
	    }

	    private void GetConsignmentValues()
	    {
	        _consignment.Name = txtName.Text;
	    }

	    private bool IsConsignmentExists()
	    {
	        using (var uow = new UnitOfWork())
	        {
	            return uow.Consignments.GetListOfConsignments().Any(cg => cg.Name.Equals(txtName.Text));
	        }
	    }
	}
}
