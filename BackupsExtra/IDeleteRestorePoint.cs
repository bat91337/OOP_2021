using System;
using Backups;

namespace BackupsExtra
{
    public interface IDeleteRestorePoint
    {
         void DeleteRestorePoint(Backupjob backupJob, DateTime dateTime, int countRestorePoint);
    }
}