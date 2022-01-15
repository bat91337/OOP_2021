using System;
using Backups;

namespace BackupsExtra
{
    public interface IDeleteRestorePoint
    {
         void DeleteRestorePoint(Backupjob backupJob, PredicateRestorePoint predicateRestorePoint);
    }
}