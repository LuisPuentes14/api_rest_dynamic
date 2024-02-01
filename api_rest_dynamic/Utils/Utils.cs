using api_rest_dynamic.CustomExceptions;
using NpgsqlTypes;


namespace api_rest_dynamic.Utils
{
    public class Utils
    {
        public static object ConvertElementInNpgsqlDbType(object element, NpgsqlDbType npgsqlDbTypeValue) {

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


    }
}
