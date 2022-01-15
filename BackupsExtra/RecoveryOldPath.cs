using System.IO.Compression;
using Backups;

namespace BackupsExtra
{
    public class RecoveryOldPath : IRecovery
    {
        public void RecoveryFile(RestorePoint restorePoint, string path)
        {
            foreach (Storage storage in restorePoint.ListStorages)
            {
                foreach (JobObject jobObject in storage.JobObjects)
                {
                    string changePath = $"{restorePoint.ZipPath}/{restorePoint.NameDirectory}";
                    ZipFile.ExtractToDirectory(changePath, jobObject.File.Path);
                }
            }
        }
    }
}