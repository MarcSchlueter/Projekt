using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp051.Logic.Ui.Base;
using System;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper
{
    public class BookViewModel : ViewModelBase<Book>
    {
        public string Id
        {
            get { return Model.Id; }
            set { Model.Id = value; }
        }

        public string Title
        {
            get { return Model.Title; }
            set { Model.Title = value; }
        }

        public string Author
        {
            get { return Model.Author; }
            set { Model.Author = value; }
        }

        public int PageCount
        {
            get { return Model.PageCount; }
            set { Model.PageCount = value; }
        }

        public string Category
        {
            get { return Model.Category; }
            set { Model.Category = value; }
        }

        public bool IsHighlighted
        {
            get { return Model.IsHighlighted; }
            set { Model.IsHighlighted = value; }
        }

        public double AverageRating
        {
            get { return Model.AverageRating; }
        }

        public override void NewModelAssigned()
        {
        }
    }
}