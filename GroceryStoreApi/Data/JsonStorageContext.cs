using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroceryStoreApi.Data
{
    public class JsonStorageContext : IStorageContext
    {
        private readonly string _JsonPath;
        private ContextData _ContextData;

        public JsonStorageContext(string jsonPath)
        {
            //TODO: Check valid path here

            _JsonPath = jsonPath;
            _ContextData = new ContextData();
        }

        public ContextData GetData()
        {
            return _ContextData;
        }

        public async Task Read()
        {
            _ContextData = await Task.Run(() => JsonConvert.DeserializeObject<ContextData>(File.ReadAllText(_JsonPath)));
        }

        public void Save()
        {
            File.WriteAllText(_JsonPath, JsonConvert.SerializeObject(_ContextData));
        }
    }
}
