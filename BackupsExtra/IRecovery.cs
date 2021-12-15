using Backups;

namespace BackupsExtra
{
    public interface IRecovery
    {
        public void RecoveryFile(RestorePoint restorePoint, string path);
    }
}