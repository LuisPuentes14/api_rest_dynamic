namespace api_rest_dynamic.Services
{
    public interface IServiceRequests
    {
        Task<Dictionary<string, object>> GetResponse(string nameObject, Dictionary<string, object> parameters);
    }
}
