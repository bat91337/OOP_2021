using System;
using System.Collections.Generic;

namespace Backups
{
    public class RestorePoint
    {
        public RestorePoint(IAlgorithm algorithm, DateTime dateTime, string zipPath)
        {
            Date = dateTime;
            ListStorages = new List<Storage>();
            Algorithm = algorithm;
            ZipPath = zipPath;
            Id = Guid.NewGuid();
            NameDirectory = $"/RestorePoint{Id}/";
        }

        public RestorePoint()
        {
        }

        public DateTime Date { get; set; }
        public List<Storage> ListStorages { get; set; }
        public IAlgorithm Algorithm { get; set; }
        public string ZipPath { get; set; }
        public string NameDirectory { get; set; }
        public Guid Id { get; set; }
    }
}