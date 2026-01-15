using De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels;
using System;
using System.Windows;

namespace De.HsFlensburg.ClientApp051.Ui.Desktop
{
    public partial class UploadBookWindow : Window
    {
        public UploadBookWindow(UploadBookWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.BookUploaded += ViewModel_BookUploaded;
        }

        private void ViewModel_BookUploaded(object sender, EventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}