namespace SPApi.Controls.Sockets
{
    public class JsonConvert
    {
        public static object DeserializeObject(string value)
        {
            return System.Text.Json.JsonSerializer.Deserialize(value, typeof(object));
        }

        public static object DeserializeObject(string value, System.Type type)
        {
            return System.Text.Json.JsonSerializer.Deserialize(value, type);
        }

        public static T DeserializeObject<T>(string value)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }

        public static string SerializeObject(object value)
        {
            return System.Text.Json.JsonSerializer.Serialize(value);
        }

        public static string SerializeObject(object value, System.Type type)
        {
            return System.Text.Json.JsonSerializer.Serialize(value, type);
        }
    }
}
