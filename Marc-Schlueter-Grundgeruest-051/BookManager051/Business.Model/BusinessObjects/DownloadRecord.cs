using System;

namespace De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects
{
    [Serializable]
    public class DownloadRecord
    {
        public User Downloader { get; private set; }
        public Book Book { get; private set; }
        public DateTime DownloadedAt { get; private set; }
        public string LocalPath { get; private set; }

        public DownloadRecord()
        {
        }

        public DownloadRecord(User downloader, Book book,
                             DateTime downloadedAt, string localPath)
        {
            Downloader = downloader;
            Book = book;
            DownloadedAt = downloadedAt;
            LocalPath = localPath;
        }
    }
}