using System.Collections.Generic;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var repository = new Repository();
            var backupJob = new Backupjob();
            var backupManager = new BackupManager();
            var jobObject = new JobObject("../../../Files/FileB");
            List<JobObject> jobObjects1 = backupManager.AddJobObject("../../../Files/FileA");
            jobObjects1.Add(jobObject);
            IAlgorithm split = new SplitAlgorithm();
            backupManager.CreateBackup(split, "../../../BackupFiles/", jobObjects1, repository, backupJob);
            backupManager.RemoveJobObject(jobObject);
            backupManager.CreateBackup(split, "../../../BackupFiles/", jobObjects1, repository, backupJob);
        }
    }
}
