using De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels;
using De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper;

namespace De.HsFlensburg.ClientApp051.Logic.Ui
{
    public class ViewModelLocator
    {
        public MainWindowViewModel TheMainWindowViewModel { get; set; }

        public ViewModelLocator()
        {
            TheMainWindowViewModel = new MainWindowViewModel();
        }
    }
}