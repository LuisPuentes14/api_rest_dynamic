using api_rest_dynamic.CustomExceptions;
using NpgsqlTypes;
using System.Data;


namespace api_rest_dynamic.Utils
{
    public class Utils
    {

        private static Dictionary<string, string> typeMap = new Dictionary<string, string>
    {
            // postgres / CSharp
        { "integer", "Int32" },
        { "bigint", "Int64" },
        { "text", "String" },
        { "varchar", "String" },
        { "boolean", "Bool" },
        { "numeric", "Decimal" },
        { "float4", "Float" },
        { "float8", "Double" },
        { "timestamp", "DateTime" },
        { "date", "DateOnly" },
        { "time", "TimeOnly" },
        { "interval", "TimeSpan" },
        { "bytea", "Byte[]" },
        { "json", "String" },
        { "jsonb", "String" }
        // Agrega más mapeos según sea necesario
    };
        public static object ConvertElementInNpgsqlDbType(object element, NpgsqlDbType npgsqlDbTypeValue)
        {

            object valorConvertido = null;

            try
            {
                switch (npgsqlDbTypeValue)
                {
                    case NpgsqlDbType.Integer:
                        valorConvertido = int.Parse(element.ToString());
                        break;
                    case NpgsqlDbType.Bigint:
                        valorConvertido = long.Parse(element.ToString());
                        break;
                    case NpgsqlDbType.Real:
                        valorConvertido = float.Parse(element.ToString());
                        break;
                    case NpgsqlDbType.Double:
                        valorConvertido = double.Parse(element.ToString());
                        break;
                    case NpgsqlDbType.Numeric:
                    case NpgsqlDbType.Money:
                        valorConvertido = decimal.Parse(element.ToString());
                        break;
                    case NpgsqlDbType.Char:
                    case NpgsqlDbType.Varchar:
                    case NpgsqlDbType.Text:
                    case NpgsqlDbType.Xml:
                    case NpgsqlDbType.Json:
                    case NpgsqlDbType.Jsonb:
                        valorConvertido = element.ToString();
                        break;
                    case NpgsqlDbType.Boolean:
                        valorConvertido = bool.Parse(element.ToString());
                        break;
                    case NpgsqlDbType.Date:
                        valorConvertido = DateOnly.Parse(element.ToString());
                        break;

                    case NpgsqlDbType.Time:
                        valorConvertido = TimeOnly.Parse(element.ToString());
                        break;
                    case NpgsqlDbType.Timestamp:
                    case NpgsqlDbType.TimestampTz:
                        valorConvertido = DateTime.Parse(element.ToString());
                        break;
                    // Agrega más casos según sea necesario
                    default:
                        throw new Exception($"No se puede manejar el tipo {npgsqlDbTypeValue}.");
                }
            }
            catch (FormatException fe)
            {
                // Manejo de error si la conversión falla
                throw new FormatException(fe.Message);
                // Puedes decidir cómo manejar este error, como asignar un valor predeterminado, lanzar una excepción, etc.
            }

            return valorConvertido;

        }


        public static string GetNpgsqlTypeToCSharpType(string npgsqlTypeName)
        {
            if (typeMap.TryGetValue(npgsqlTypeName, out var csharpTypeName))
            {
                return csharpTypeName;
            }
            else
            {
                throw new ArgumentException($"No C# type mapping found for NpgsqlDbType '{npgsqlTypeName}'");
            }
        }

        public static object ConvertElementInDbType(object element, DbType dbTypeValue)
        {
            object valorConvertido = null;

            try
            {
                switch (dbTypeValue)
                {
                    case DbType.Int32:
                        valorConvertido = element.ToString() == "" ? null : int.Parse(element.ToString());
                        break;
                    case DbType.Int64:
                        valorConvertido = element.ToString() == "" ? null : long.Parse(element.ToString());
                        break;
                    case DbType.Single:
                        valorConvertido = element.ToString() == "" ? null : float.Parse(element.ToString());
                        break;
                    case DbType.Double:
                        valorConvertido = element.ToString() == "" ? null : double.Parse(element.ToString());
                        break;
                    case DbType.Decimal:
                        valorConvertido = element.ToString() == "" ? null : decimal.Parse(element.ToString());
                        break;
                    case DbType.String:
                    case DbType.StringFixedLength:
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.Xml:
                        valorConvertido = element.ToString() == "" ? null : element.ToString();
                        break;
                    case DbType.Boolean:
                        valorConvertido = element.ToString() == "" ? null : bool.Parse(element.ToString());
                        break;
                    case DbType.Date:
                        valorConvertido = element.ToString() == "" ? null : DateOnly.Parse(element.ToString());
                        break;
                    case DbType.Time:
                        valorConvertido = element.ToString() == "" ? null : TimeOnly.Parse(element.ToString());
                        break;
                    case DbType.DateTime:
                    case DbType.DateTime2:
                    case DbType.DateTimeOffset:
                        valorConvertido = element.ToString() == "" ? null : DateTime.Parse(element.ToString());
                        break;
                    // Agrega más casos según sea necesario
                    default:
                        throw new Exception($"No se puede manejar el tipo {dbTypeValue}.");
                }
            }
            catch (FormatException fe)
            {
                // Manejo de error si la conversión falla
                throw new FormatException($"Error al convertir el elemento a {dbTypeValue}: {fe.Message}");
            }

            return valorConvertido;
        }


    }
}
