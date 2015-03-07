using System;
using System.Collections.Generic;

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

    public class FileViewModelNameComparer : IEqualityComparer<FileViewModel>
    {
        public bool Equals(FileViewModel first, FileViewModel second)
        {
            if (first.FullName == second.FullName && first.CommitId == second.CommitId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(FileViewModel file)
        {
            return (file.FullName + file.CommitId.ToString()).GetHashCode();
        }
    }
}
