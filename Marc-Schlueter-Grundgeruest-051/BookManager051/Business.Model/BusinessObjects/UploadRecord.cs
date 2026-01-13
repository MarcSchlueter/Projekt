using System;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class UploadRecord
    {
        public User Uploader { get; private set; }
        public Book Book { get; private set; }
        public DateTime UploadedAt { get; private set; }
        public string StoragePath { get; private set; }

        public UploadRecord()
        {
        }

        public UploadRecord(User uploader, Book book, 
                           DateTime uploadedAt, string storagePath)
        {
            Uploader = uploader;
            Book = book;
            UploadedAt = uploadedAt;
            StoragePath = storagePath;
        }
    }
}