using System;
using System.Runtime.Serialization;

namespace MessServ.Models
{
    /// <summary>
    /// Class of messages.
    /// </summary>
    [DataContract]
    public class Message
    {
        // Arrays of words to generate topic and text of the messages.
        static private string[] topic1 = { "1 ", "78 ", "WARNING ", "IMPORTANT ", "Dangerous ", "!1! ", "Love ", "Xe" };
        static private string[] topic2 = { "Non", "Message", "Reed To End", "Bee", "Care", "Beware", "Wave" };
        static private string[] word1 = { "I ", "You ", "They ", "He ", "She ", "We ", "It " };
        static private string[] word2 = { "love ", "watch ", "phone ", "hate ", "die ", "pretend ", "cry ", "keep " };
        static private string[] word3 = { "flower", "district", "frog", "happy", "course", "knife", "cart", "money" };
        static private string[] word4 = { "!", ".", "?", "?!", " ^^", " :3", " :D" };
        [DataMember]
        // The topic of the message.
        public string Subject { get; set; }
        [DataMember]
        // The text of the message.
        public string Text { get; set; }
        [DataMember]
        // Sender user email of the message.
        public string SenderID { get; set; }
        [DataMember]
        // Receiver user email of the messages.
        public string ReceiverID { get; set; }
        /// <summary>
        /// Construstor to create the message.
        /// </summary>
        /// <param name="sub">Subject (topic) of the message.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="sender">Sender user email of the message</param>
        /// <param name="receiver">Receiver user email of the messgae.</param>
        public Message(string sub, string text, string sender, string receiver)
        {
            Subject = sub;
            Text = text;
            SenderID = sender;
            ReceiverID = receiver;
        }
        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public Message()
        {

        }
        /// <summary>
        /// Create random message.
        /// </summary>
        /// <param name="Semail">Sender user email.</param>
        /// <param name="Remail">Reciever user email.</param>
        /// <returns></returns>
        static public Message CreateMessage(string Semail, string Remail)
        {
            var rnd = new Random();
            // Generate random topic (subject).
            string topic = topic1[rnd.Next(topic1.Length)] + topic2[rnd.Next(topic2.Length)];
            // Generate rnadom text.
            string text = word1[rnd.Next(word1.Length)] + word2[rnd.Next(word2.Length)] + word3[rnd.Next(word3.Length)] + word4[rnd.Next(word4.Length)];
            return new Message(topic, text, Semail, Remail);
        }
    }
}
