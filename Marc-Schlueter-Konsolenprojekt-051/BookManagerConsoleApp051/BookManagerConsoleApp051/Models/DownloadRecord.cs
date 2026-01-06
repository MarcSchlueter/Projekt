using System;

namespace BookManagerConsoleApp051.Models
{
    public class DownloadRecord
    {
        public DownloadRecord(User downloader, Book book, DateTime downloadedAt, string localPath)
        {
            Downloader = downloader;
            Book = book;
            DownloadedAt = downloadedAt;
            LocalPath = localPath;
        }

        public User Downloader { get; }

        public Book Book { get; }

        public DateTime DownloadedAt { get; }

        public string LocalPath { get; }
    }
}