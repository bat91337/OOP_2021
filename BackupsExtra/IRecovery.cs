using Backups;

namespace BackupsExtra
{
    public interface IRecovery
    {
        void RecoveryFile(RestorePoint restorePoint, string path);
    }
}