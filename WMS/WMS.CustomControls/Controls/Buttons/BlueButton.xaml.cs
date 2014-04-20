using System;
using System.Windows;
using System.Windows.Controls;

namespace WMS.CustomControls.Controls.Buttons
{
    /// <summary>
    /// Interaction logic for BlueButton.xaml
    /// </summary>
    public partial class BlueButton : UserControl
    {
        public string Text
        {
            set { btnBlueButton.Content = value; }
        }

        public RoutedEventHandler Click
        {
            set { btnBlueButton.Click += value; }

        }

        public BlueButton()
        {
            InitializeComponent();
        }
    }
}
