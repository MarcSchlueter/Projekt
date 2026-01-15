using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.MessageBusMessages
{
    public class OpenUploadBookWindowMessage
    {
        public BookManager BookManager { get; set; }

        public OpenUploadBookWindowMessage(BookManager bookManager)
        {
            BookManager = bookManager;
        }
    }
}