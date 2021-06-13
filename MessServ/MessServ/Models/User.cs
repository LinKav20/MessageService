using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MessServ.Models
{
    /// <summary>
    /// Class of user.
    /// </summary>
    [DataContract]
    public class User
    {
        // Arrays of syllables to generate a name.
        static private string[] syll1 = { "Ma", "Jo", "Le", "An", "Al", "Sti", "Ne", "Wo", "Flo", "Ri" };
        static private string[] syll2 = { "rin", "hat", "vin", "ken", "ex", "na", "st", "gor", "men", "k" };
        [DataMember]
        // Name of the user.
        public string Name { get; set; }
        [DataMember]
        // Email of the user.
        public string Email { get; set; }
        [DataMember]
        // Lis tof all messages of the user.
        public List<Message> Messages { get; set; }
        /// <summary>
        /// Constructor to create the user.
        /// </summary>
        /// <param name="name">Name of the user.</param>
        /// <param name="email">Email of the user.</param>
        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Messages = new List<Message>();
        }
        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public User()
        {

        }
        /// <summary>
        /// Create n random users.
        /// </summary>
        /// <param name="Users">List where we put all users.</param>
        static public void CreateUsers(ref List<User> Users)
        {
            var rnd = new Random();
            Users = new List<User>();
            // Create random amount of users.
            for (int i = 0; i < rnd.Next(2, 26); i++)
            {
                // Generate random name.
                string name = syll1[rnd.Next(syll1.Length)] + syll2[rnd.Next(syll2.Length)];
                string email = name + "@love.you";
                // Add user.
                Users.Add(new User(name, email));
            }
            // Create random amout of messages for all users.
            for (int i = 0; i < rnd.Next(Users.Count, 3 * Users.Count); i++)
            {
                // Select receiver. 
                int indexToSend = -1;
                // Checking that sender cannot be a reciever for one message.
                while (indexToSend == -1)
                {
                    indexToSend = rnd.Next(Users.Count);
                    if (indexToSend == i % Users.Count)
                        indexToSend = -1;
                }
                // Add message for user.
                Users[i % Users.Count].Messages.Add(Message.CreateMessage(Users[i % Users.Count].Email, Users[indexToSend].Email));
            }
        }
    }
}
