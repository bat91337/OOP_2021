using System.Collections.Generic;

namespace Backups
{
    public interface IRepository
    {
        List<Storage> CreateStorageZip(List<JobObject> jobObjects, IAlgorithm algorithm, string path, string id, Backupjob backupJob);
    }
}