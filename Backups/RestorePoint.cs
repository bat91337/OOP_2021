using System;
using System.Collections.Generic;

namespace Backups
{
    public class RestorePoint
    {
        public RestorePoint(IAlgorithm algorithm, DateTime dateTime)
        {
            Date = dateTime;
            ListStorages = new List<Storage>();
            Algorithm = algorithm;
        }

        public DateTime Date { get; set; }
        public List<Storage> ListStorages { get; }
        public IAlgorithm Algorithm { get; }
    }
}