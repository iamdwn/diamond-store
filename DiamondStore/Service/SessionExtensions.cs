using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace Service
{
    public static class SessionExtensions
    {
        // Method to set an object as JSON in the session
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // Serialize the object to a JSON string
            var jsonString = JsonConvert.SerializeObject(value);
            // Convert the JSON string to a byte array
            var byteArray = Encoding.UTF8.GetBytes(jsonString);
            // Set the byte array in the session
            session.Set(key, byteArray);
        }

        // Method to get an object from JSON in the session
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            // Try to get the byte array from the session
            byte[] value;
            session.TryGetValue(key, out value);

            // If the byte array is null, return the default value of T
            if (value == null)
            {
                return default(T);
            }

            // Convert the byte array to a JSON string
            var jsonString = Encoding.UTF8.GetString(value);
            // Deserialize the JSON string to an object of type T
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
