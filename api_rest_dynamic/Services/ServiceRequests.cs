using api_rest_dynamic.CustomExceptions;
using api_rest_dynamic.Models;
using api_rest_dynamic.Repository;
using api_rest_dynamic.Singleton.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Npgsql;

namespace api_rest_dynamic.Services
{
    public class ServiceRequests(IRepositoryRequests repositoryRequests) : IServiceRequests
    {
        private readonly IRepositoryRequests _repositoryRequests = repositoryRequests;
        public Dictionary<string, dynamic> GetResponse(string nameObject, Dictionary<string, dynamic> parameters)
        {

            ObjectsReques? objectsReques = LoadObjectsDataBase.objectsReques
                                            .Where(o => o.name == nameObject)
                                            .FirstOrDefault();

            if (objectsReques is null)

                throw new NotFoundException ($"Objeto '{nameObject}' no encontrado");

           return _repositoryRequests.GetResponse(objectsReques, parameters);

        }
    }
}
