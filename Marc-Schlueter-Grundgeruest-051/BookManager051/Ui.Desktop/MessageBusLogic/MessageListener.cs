using De.HsFlensburg.ClientApp051.Logic.Ui.MessageBusMessages;
using De.HsFlensburg.ClientApp051.Logic.Ui.ViewModels;
using De.HsFlensburg.ClientApp051.Services.MessageBus;
using System;

namespace De.HsFlensburg.ClientApp051.Ui.Desktop.MessageBusLogic
{
    class MessageListener
    {
        public bool BindableProperty => true;

        public MessageListener()
        {
            InitMessenger();
        }

        private void InitMessenger()
        {
            ServiceBus.Instance.Register<OpenNewClientWindowMessage>
                (this, OpenNewClientWindow);

            ServiceBus.Instance.Register<OpenAddRatingWindowMessage>
                (this, OpenAddRatingWindow);
        }

        private void OpenNewClientWindow()
        {
            NewClientWindow myWindow = new NewClientWindow();
            myWindow.ShowDialog();
        }

        private void OpenAddRatingWindow(
            OpenAddRatingWindowMessage message)
        {
            var viewModel = new AddRatingWindowViewModel(
                message.BookManager);
            var window = new AddRatingWindow(viewModel);
            window.ShowDialog();
        }
    }
}