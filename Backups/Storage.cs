using System;
using System.Collections.Generic;

namespace Backups
{
    public class Storage
    {
        public Storage()
        {
            JobObjects = new List<JobObject>();
        }

        public List<JobObject> JobObjects { get; }

        // public List<JobObject> AddJobObject
        // {
        //
        // }
    }
}