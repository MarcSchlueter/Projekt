using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.MessageBusMessages
{
    public class OpenAddRatingWindowMessage
    {
        public BookManager BookManager { get; set; }

        public OpenAddRatingWindowMessage(BookManager bookManager)
        {
            BookManager = bookManager;
        }
    }
}