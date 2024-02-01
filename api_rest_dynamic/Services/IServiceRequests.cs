namespace api_rest_dynamic.Services
{
    public interface IServiceRequests
    {
        Dictionary<string, dynamic> GetResponse(string nameObject, Dictionary<string, object> parameters);
    }
}
