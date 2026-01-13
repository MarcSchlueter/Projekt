using De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels;
using System;
using System.Windows;

namespace De.HsFlensburg.ClientApp051.Ui.Desktop
{
    public partial class AddRatingWindow : Window
    {
        public AddRatingWindow(AddRatingWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.RatingAdded += ViewModel_RatingAdded;
        }

        private void ViewModel_RatingAdded(object sender, EventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}