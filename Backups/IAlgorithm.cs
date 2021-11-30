using System.Collections.Generic;

namespace Backups
{
    public interface IAlgorithm
    {
         List<Storage> CreateStorages(List<JobObject> jobObjects);
    }
}