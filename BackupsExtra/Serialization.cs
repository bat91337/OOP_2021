using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Backups;

namespace BackupsExtra
{
    public class Serialization
    {
        public void CreateRestorePointJson(IAlgorithm algorithm, DateTime dateTime, string path)
        {
            var restorePoint = new RestorePoint(algorithm, dateTime, path);
            var fs = new FileStream(@"../../../RestorePoint/file.json", FileMode.OpenOrCreate);
            JsonSerializer.SerializeAsync<RestorePoint>(fs, restorePoint);
        }

        public async Task RestorePointJson(FileStream fileStream)
        {
            RestorePoint restoredRestorePointExtra = await JsonSerializer.DeserializeAsync<RestorePoint>(fileStream);
        }
    }
}