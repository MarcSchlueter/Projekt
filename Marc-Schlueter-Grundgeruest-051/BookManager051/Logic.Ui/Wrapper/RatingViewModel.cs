using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp051.Logic.Ui.Base;
using System;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper
{
    public class RatingViewModel : ViewModelBase<Rating>
    {
        public string ReviewerName
        {
            get { return Model.Reviewer?.DisplayName ?? "Unknown"; }
        }

        public string BookTitle
        {
            get { return Model.Book?.Title ?? "Unknown"; }
        }

        public int Score
        {
            get { return Model.Score; }
        }

        public string Comment
        {
            get { return Model.Comment; }
        }

        public DateTime CreatedAt
        {
            get { return Model.CreatedAt; }
        }

        public override void NewModelAssigned()
        {
        }
    }
}