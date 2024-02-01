using api_rest_dynamic.Models;
using api_rest_dynamic.Singleton.Context;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using midelware.Singleton.Logger;
using Npgsql;
using System.Text;
using System.Xml.Linq;

namespace api_rest_dynamic
{
    public class LoadObjectsDataBase
    {

        public static List<ObjectsReques> objectsReques = new();
        public static void GetObjectsDataBase()
        {
            string queryString = @"SELECT    
                                        p.proname AS procedure_name,
	                                    SUBSTRING(pg_get_function_arguments(p.oid), 0,strpos(pg_get_function_arguments(p.oid),', OUT') ) AS input_parameters,
	                                    SUBSTRING(pg_get_function_arguments(p.oid), strpos(pg_get_function_arguments(p.oid),'OUT'),LENGTH(pg_get_function_arguments(p.oid)) ) AS output_parameters
                                    FROM
                                        pg_proc p
                                        JOIN pg_namespace n ON p.pronamespace = n.oid
                                        LEFT JOIN pg_type t ON p.prorettype = t.oid
                                    WHERE
                                        n.nspname NOT IN ('pg_catalog', 'information_schema')
	                                    AND p.proname LIKE 'request%'
	                                    AND  n.nspname = 'polaris_core'
                                    ORDER BY   
                                        procedure_name;";

            using (var connection = new NpgsqlConnection(ContexPostgres.GetContextString()))
            {
                using (var command = new NpgsqlCommand(queryString, connection))
                {
                    try
                    {
                        connection.Open();

                        AppLogger.GetInstance().Info("Conexion establecida a la base de datos");

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                objectsReques.Add(new ObjectsReques
                                {
                                    name = reader[0].ToString(),
                                    inputParameters = reader[1].ToString(),
                                    outParameters = reader[2].ToString()
                                });
                            }
                        }

                        AppLogger.GetInstance().Info($"Objetos encontrados {objectsReques.Count()}");

                    }
                    catch (Exception ex)
                    {
                        AppLogger.GetInstance().Error($"Error estableciendo conexion a la base de datos: {ex.Message}");
                    }
                }
            }
        }
    }
}
