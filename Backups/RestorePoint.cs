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

        public DateTime Date { get; }
        public List<Storage> ListStorages { get; }
        public IAlgorithm Algorithm { get; }
        public string ZipPath { get; }
        public Guid Id { get; }
        public string NameDirectory { get; }
    }
}