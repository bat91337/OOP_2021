using System;
using System.Collections.Generic;
namespace Backups
{
    public class Backupjob
    {
        public Backupjob()
        {
            JobObjects = new List<JobObject>();
            RestorePoints = new List<RestorePoint>();
        }

        public List<JobObject> JobObjects { get; }
        public List<RestorePoint> RestorePoints { get; }
        public List<JobObject> AddJobObject(string path, string name)
        {
            var jobObject = new JobObject(path, name);
            JobObjects.Add(jobObject);
            return JobObjects;
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            JobObjects.Remove(jobObject);
        }
    }
}