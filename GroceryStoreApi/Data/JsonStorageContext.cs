using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroceryStoreApi.Data
{
    public class JsonStorageContext : IStorageContext
    {
        private readonly string _JsonPath;
        private ContextData _ContextData;
        readonly IFileSystem _FileSystem;

        public JsonStorageContext(string jsonPath, IFileSystem fileSystem)
        {
            //TODO: Check valid path here
            if (String.IsNullOrWhiteSpace(jsonPath)) throw new ArgumentException();

            _JsonPath = jsonPath;
            _ContextData = new ContextData();
            _FileSystem = fileSystem;
        }

        public ContextData GetData()
        {
            return _ContextData;
        }

        public async Task Read()
        {
            try
            {
                if (_FileSystem.File.Exists(_JsonPath))
                {
                    var jsonData = _FileSystem.File.ReadAllText(_JsonPath);
                    _ContextData = await Task.Run(() => JsonConvert.DeserializeObject<ContextData>(jsonData));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save()
        {
            File.WriteAllText(_JsonPath, JsonConvert.SerializeObject(_ContextData));
        }
    }
}
