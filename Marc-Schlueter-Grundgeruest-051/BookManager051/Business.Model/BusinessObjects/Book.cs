using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class Book : INotifyPropertyChanged
    {
        private string id;
        private string title;
        private string author;
        private int pageCount;
        private string category;
        private bool isHighlighted;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        public int PageCount
        {
            get { return pageCount; }
            set
            {
                pageCount = value;
                OnPropertyChanged(nameof(PageCount));
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public bool IsHighlighted
        {
            get { return isHighlighted; }
            set
            {
                isHighlighted = value;
                OnPropertyChanged(nameof(IsHighlighted));
            }
        }

        public List<Rating> Ratings { get; private set; }

        public double AverageRating
        {
            get
            {
                return Ratings.Any() ? Ratings.Average(rating => rating.Score) : 0.0;
            }
        }

        public Book()
        {
            Ratings = new List<Rating>();
        }

        public Book(string id, string title, string author, int pageCount, string category)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.pageCount = pageCount;
            this.category = category;
            this.isHighlighted = false;
            Ratings = new List<Rating>();
        }

        public void AddOrUpdateRating(Rating newRating)
        {
            if (newRating == null)
            {
                throw new ArgumentNullException(nameof(newRating));
            }

            Rating existingRating = Ratings.FirstOrDefault(
                rating => rating.Reviewer.Id == newRating.Reviewer.Id);

            if (existingRating != null)
            {
                Ratings.Remove(existingRating);
            }

            Ratings.Add(newRating);
            OnPropertyChanged(nameof(AverageRating));
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}