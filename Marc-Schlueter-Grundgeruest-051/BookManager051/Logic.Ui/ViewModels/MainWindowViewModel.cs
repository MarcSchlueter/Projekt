using De.HsFlensburg.ClientApp051.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper;
using De.HsFlensburg.ClientApp051.Services.MessageBus;
using De.HsFlensburg.ClientApp051.Services.SerializationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels
{
    public class MainWindowViewModel
    {
        string pathForSerialization;
        ModelFileHandler modelFileHandler;
        public ICommand RenameValueInModelCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand OpenNewClientWindowCommand { get; }
        private void OpenNewClientWindowMethod()
        {
            ServiceBus.Instance.Send(new OpenNewClientWindowMessage());
        }
        public ClientCollectionViewModel MyList { get; set; }

        public MainWindowViewModel(ClientCollectionViewModel viewModelCollection)
        {
            RenameValueInModelCommand = new RelayCommand(RenameValueInModel);
            SaveCommand = new RelayCommand(SaveModel);
            LoadCommand = new RelayCommand(LoadModel);
            OpenNewClientWindowCommand = new RelayCommand(OpenNewClientWindowMethod);
            MyList = viewModelCollection;
            modelFileHandler = new ModelFileHandler();
            pathForSerialization = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments) +
                "\\ClientCollectionSerialization\\MyClient.cc";
        }

        private void RenameValueInModel()
        {
            //var collection =
            //    this.myDataGrid.ItemsSource
            //    as ClientCollectionViewModel;
            var first =
                MyList.FirstOrDefault();
            first.Model.Name =
                "Rename in the model";
        }

        private void SaveModel()
        {
            modelFileHandler.WriteModelToFile(
                pathForSerialization, MyList.Model);
        }
        private void LoadModel()
        {
            MyList.Model = modelFileHandler.ReadModelFromFile(
                pathForSerialization);
        }
    }
}
