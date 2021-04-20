using System.Threading.Tasks;

namespace GroceryStoreApi.Data
{
    public interface IStorageContext
    {
        ContextData GetData();

        Task Read();

        void Save();
    }
}
