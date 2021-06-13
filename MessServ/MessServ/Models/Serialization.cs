using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace MessServ.Models
{
    /// <summary>
    /// Class for serialization
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// Serialize all data.
        /// </summary>
        /// <param name="Users">List of elements to serialize.</param>
        public static void Serialize(List<User> Users)
        {
            // Cleaning up the file before writing data.
            if (File.Exists("Users.json"))
            {
                StreamWriter writer = new StreamWriter("Users.json");
                writer.WriteLine("");
                writer.Close();
            }
            using (StreamWriter streamWriter = new StreamWriter(new FileStream("Users.json", FileMode.OpenOrCreate)))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                // Serialize all users in order by email (a-z).
                jsonSerializer.Serialize(streamWriter, Users.OrderBy(x => x.Email).ToList());
            }
        }
        /// <summary>
        /// Deserialize all data.
        /// </summary>
        /// <param name="Users">List where we put all data.</param>
        public static void Deserialize(out List<User> Users)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(new FileStream("Users.json", FileMode.OpenOrCreate)))
                {
                    string data = streamReader.ReadToEnd();
                    var users = JsonConvert.DeserializeObject<List<User>>(data);
                    Users = users;
                }
            }
            catch
            {
                Users = new List<User>();
            }
        }
    }
}