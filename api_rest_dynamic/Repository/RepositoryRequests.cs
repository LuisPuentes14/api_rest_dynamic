using api_rest_dynamic.CustomExceptions;
using api_rest_dynamic.Models;
using api_rest_dynamic.Singleton.Context;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using api_rest_dynamic.Utils;

namespace api_rest_dynamic.Repository
{
    public class RepositoryRequests : IRepositoryRequests
    {
        public async Task<Dictionary<string, dynamic>> GetResponse(ObjectsReques obj, Dictionary<string, object> inputParameters)
        {
            using (var connection = new NpgsqlConnection(ContexPostgres.GetContextString()))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(obj.name, connection))
                {

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // obtiene los parametros de entrada que le corresponden a sp 
                    List<string> listInParameters = obj.inputParameters.Split(", ").ToList();

                    // agrega los parametros de entrada a sp con su valor 
                    foreach (var parameter in inputParameters)
                    {
                        // busca el parametros de entrada con lo que esta llegando en el request
                        string inParameter = listInParameters.Where(x => x.Contains(parameter.Key)).FirstOrDefault();

                        // si no lo encuentra genera una excepcion 
                        if (inParameter is null)
                            throw new NotFoundException($"parametro '{parameter.Key}' no encontrado en el sp '{obj.name}' ");

                        //obtiene el tipo de datos de NpgsqlDbType
                        Enum.TryParse(inParameter.Trim().Split(" ")[2], true, out NpgsqlDbType DbTypeValue);

                        // se agrega el parametro con su valor 
                        command.Parameters.AddWithValue(parameter.Key, Utils.Utils.ConvertElementInNpgsqlDbType(parameter.Value, DbTypeValue));
                    }

                    // obtiene los parametros de salida del sp
                    var listOutParameters = obj.outParameters.Split(", ");

                    // asigna los parametros de salida
                    foreach (var parameter in listOutParameters)
                    {
                        Enum.TryParse(parameter.Trim().Split(" ")[2], true, out NpgsqlDbType npgsqlDbTypeValue);
                        command.Parameters.Add(new NpgsqlParameter(parameter.Trim().Split(" ")[1], npgsqlDbTypeValue) { Direction = ParameterDirection.Output });
                    }

                    // ejecuta
                   await command.ExecuteNonQueryAsync();

                    Dictionary<string, object> requestResult = new();

                    // agrega al diccionario el resultado
                    foreach (var parameter in listOutParameters)
                    {                    
                        //parsea el tipo de datos de postgres a al lenguaje de CSharp
                        Enum.TryParse(
                            // obtiene el tipo de dato al lenguage 
                           Utils.Utils.GetNpgsqlTypeToCSharpType( parameter.Trim().Split(" ")[2]), 
                            true, out DbType npgsqlDbTypeValue);

                        //obtiene el tipo ya convertidos
                        var value = Utils.Utils.ConvertElementInDbType(command.Parameters[parameter.Trim().Split(" ")[1]].Value,npgsqlDbTypeValue);                        
                       
                        // loa agrega a la lista
                        requestResult.Add(
                            parameter.Trim().Split(" ")[1], value);
                    }

                    // retorna el diccionario
                    return requestResult;

                }
            }
        }
    }
}
