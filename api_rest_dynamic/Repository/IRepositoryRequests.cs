using api_rest_dynamic.Models;

namespace api_rest_dynamic.Repository
{
    public interface IRepositoryRequests
    {
        Task<Dictionary<string, object>> GetResponse(ObjectsReques obj, Dictionary<string, object> parameters);
    }
}
