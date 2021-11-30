using System;
using System.Collections.Generic;

namespace Backups
{
    public class RestorePoint
    {
        public RestorePoint(IAlgorithm algorithm)
        {
            Date = DateTime.Now;
            ListStorages = new List<Storage>();
            Algorithm = algorithm;
        }

        public DateTime Date { get; }
        public List<Storage> ListStorages { get; }
        public IAlgorithm Algorithm { get; }
    }
}