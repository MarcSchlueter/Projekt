using System;

namespace BookManagerConsoleApp051.Models
{
    public class UploadRecord
    {
        public UploadRecord(User uploader, Book book, DateTime uploadedAt, string storagePath)
        {
            Uploader = uploader;
            Book = book;
            UploadedAt = uploadedAt;
            StoragePath = storagePath;
        }

        public User Uploader { get; }

        public Book Book { get; }

        public DateTime UploadedAt { get; }

        public string StoragePath { get; }
    }
}