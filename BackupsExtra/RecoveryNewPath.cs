using System.IO.Compression;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class RecoveryNewPath : IRecovery
    {
        public void RecoveryFile(RestorePoint restorePoint, string path)
        {
            foreach (JobObject jobObject in restorePoint.ListStorages.SelectMany(storage => storage.JobObjects))
            {
                ZipFile.ExtractToDirectory(restorePoint.ZipPath, path);
            }
        }
    }
}