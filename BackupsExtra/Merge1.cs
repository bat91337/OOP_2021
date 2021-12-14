// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Backups;
//
// namespace BackupsExtra
// {
//     public class Merge1
//     {
//         public void Merge(RestorePoint restorePoint1, RestorePoint restorePoint2, Backupjob backupJob)
//         {
//            RestorePoint restorePoint = SearchRestorePointWithMinimalDate(backupJob);
//             foreach (Storage storage1 in restorePoint1.ListStorages)
//             {
//                 foreach (var storage2 in restorePoint2.ListStorages.Where(storage2 => storage1.JobObjects.SequenceEqual(storage2.JobObjects)))
//                 {
//                     backupJob.RestorePoints.Remove(restorePoint1);
//                 }
//             }
//         }
//
//         public RestorePoint SearchRestorePointWithMinimalDate(Backupjob backupJob)
//         {
//             backupJob.RestorePoints.OrderByDescending(x => x.Date).ToList();
//             RestorePoint restorePoint1 = backupJob.RestorePoints.First();
//             return restorePoint1;
//         }
//     }
// }