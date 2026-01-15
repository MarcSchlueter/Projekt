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
            System.Diagnostics.Debug.WriteLine(
                "=== MessageListener: CONSTRUCTOR CALLED ===");
            InitMessenger();
            System.Diagnostics.Debug.WriteLine(
                "=== MessageListener: CONSTRUCTOR FINISHED ===");
        }

        private void InitMessenger()
        {
            System.Diagnostics.Debug.WriteLine(
                "=== MessageListener: InitMessenger START ===");

            ServiceBus.Instance.Register<OpenNewClientWindowMessage>
                (this, OpenNewClientWindow);

            ServiceBus.Instance.Register<OpenAddRatingWindowMessage>
                (this, OpenAddRatingWindow);

            ServiceBus.Instance.Register<OpenUploadBookWindowMessage>
                (this, OpenUploadBookWindow);

            System.Diagnostics.Debug.WriteLine(
                "=== MessageListener: InitMessenger COMPLETE ===");
        }

        private void OpenNewClientWindow()
        {
            NewClientWindow myWindow = new NewClientWindow();
            myWindow.ShowDialog();
        }

        private void OpenAddRatingWindow(
            OpenAddRatingWindowMessage message)
        {
            System.Diagnostics.Debug.WriteLine(
                "=== MessageListener: OpenAddRatingWindow CALLED ===");

            var viewModel = new AddRatingWindowViewModel(
                message.BookManager);
            var window = new AddRatingWindow(viewModel);

            if (window.ShowDialog() == true)
            {
                ServiceBus.Instance.Send(new RatingAddedMessage());
            }

            System.Diagnostics.Debug.WriteLine(
                "=== MessageListener: Window closed ===");
        }

        private void OpenUploadBookWindow(
            OpenUploadBookWindowMessage message)
        {
            System.Diagnostics.Debug.WriteLine(
                "=== MessageListener: OpenUploadBookWindow CALLED ===");

            var viewModel = new UploadBookWindowViewModel(
                message.BookManager);
            var window = new UploadBookWindow(viewModel);

            if (window.ShowDialog() == true)
            {
                ServiceBus.Instance.Send(new BookUploadedMessage());
            }

            System.Diagnostics.Debug.WriteLine(
                "=== MessageListener: Upload window closed ===");
        }
    }
}