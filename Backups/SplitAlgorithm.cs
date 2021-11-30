using System.Collections.Generic;
namespace Backups
{
    public class SplitAlgorithm : IAlgorithm
    {
        public SplitAlgorithm()
        {
        }

        public List<Storage> CreateStorages(List<JobObject> jobObjects)
        {
            var storages = new List<Storage>();
            foreach (JobObject jobObject in jobObjects)
            {
                var storage = new Storage();
                storage.JobObjects.Add(jobObject);
                storages.Add(storage);
            }

            return storages;
        }
    }
}