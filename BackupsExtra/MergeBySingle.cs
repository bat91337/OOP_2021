using System.Collections.Generic;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class MergeBySingle
    {
       public void Merge(Backupjob backupJob)
        {
            var restorePoints = new List<RestorePoint>();
            RestorePoint restorePointNew = backupJob.RestorePoints.Last();
            backupJob.RestorePoints.Remove(restorePointNew);
            if (restorePointNew.Algorithm is SingleAlgorithm)
            {
                foreach (RestorePoint restorePoint in backupJob.RestorePoints)
                {
                    restorePoints.Add(restorePoint);
                }

                DeleteRestorePoints(restorePoints, backupJob);
            }

            backupJob.RestorePoints.Add(restorePointNew);
        }

       public void DeleteRestorePoints(List<RestorePoint> restorePoints, Backupjob backupJob)
       {
           foreach (RestorePoint restorePoint in restorePoints)
           {
               backupJob.RestorePoints.Remove(restorePoint);
           }
       }
    }
}