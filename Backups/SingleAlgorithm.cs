using System.Collections.Generic;
using Ionic.Zip;

namespace Backups
{
    public class SingleAlgorithm : IAlgorithm
    {
        public SingleAlgorithm()
        {
        }

        public List<Storage> CreateStorages(List<JobObject> jobObjects)
        {
            var storages = new List<Storage>();
            var storage = new Storage();
            foreach (JobObject jobObject in jobObjects)
            {
                storage.JobObjects.Add(jobObject);
            }

            storages.Add(storage);
            return storages;
        }
    }
}