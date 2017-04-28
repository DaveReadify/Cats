using System.Threading.Tasks;

namespace Cats.API
{
    public interface IAPIConfiguration
    {
        Task<APISettings> GetSettingsAsync();
    }
}