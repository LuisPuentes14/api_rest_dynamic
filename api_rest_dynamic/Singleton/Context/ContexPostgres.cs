using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace api_rest_dynamic.Singleton.Context
{
    public class ContexPostgres
    {
        private static string _connectionString = null;

        private ContexPostgres() { }

        public static string GetContextString()
        {

            if (_connectionString is null)
            {
                _connectionString = "Host=localhost;Port=5432;Database=polariscore;Username=postgres;Password=12345;SearchPath=polaris_core";
                return _connectionString;
            }
            return _connectionString;
        }

    }
}

