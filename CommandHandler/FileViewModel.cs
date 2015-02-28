using System;

namespace CommandHandler
{
    public class FileViewModel
    {
        public string FullName { get; set; }

        public string Status { get; set; }

        public int Version { get; set; }

        public DateTime LAstWriteTime { get; set; }

        public int CommitId { get; set; }

        public void Update(FileViewModel newVersion)
        {
            this.LAstWriteTime = newVersion.LAstWriteTime;
            this.Status = newVersion.Status;
            this.Version = newVersion.Version;
            CommitId = newVersion.CommitId;
        }
    }
}
