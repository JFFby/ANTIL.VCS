using System;
using System.Collections.Generic;

namespace CommandHandler
{
    public class FileViewModel
    {
        public FileViewModel(string projectPath)
        {
            ProjectPath = projectPath;
        }

        public string Name { get; set; }

        public string Status { get; set; }

        public int Version { get; set; }

        public DateTime LAstWriteTime { get; set; }

        public int CommitId { get; set; }

        public string ProjectPath { get; set; }

        public string FullPath { get { return ProjectPath + Name; } } 

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
            if (first.Name == second.Name && first.CommitId == second.CommitId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(FileViewModel file)
        {
            return (file.Name + file.CommitId.ToString()).GetHashCode();
        }
    }
}
