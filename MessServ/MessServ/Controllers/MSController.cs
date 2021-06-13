using MessServ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApp.Controllers
{
    /// <summary>
    /// Logic of all functions.
    /// </summary>
    [ApiController]
    public class MessageServiceController : ControllerBase
    {
        // List of all users.
        public List<User> Users = new List<User>();

        /// <summary>
        /// Post method to create random amount users.
        /// </summary>
        /// <returns>Message with amount of users.</returns>
        [HttpPost("createusers")]
        public async Task<string> Post()
        {
            // Create random users.
            MessServ.Models.User.CreateUsers(ref Users);
            Serialization.Serialize(Users);
            return $"Created {Users.Count} users!";
        }
        /// <summary>
        /// Post method to create the user.
        /// </summary>
        /// <param name="name">Name of the user.</param>
        /// <returns>Message of the result of creation.</returns>
        [HttpPost("adduser/{name}")]
        public async Task<string> Post([FromRoute] string name)
        {
            Serialization.Deserialize(out Users);
            // Check if the user exist.
            if (Users.Where(x => x.Name == name).ToList().Count > 0)
            {
                return $"User with name {name} is already exist";
            }
            // Add new users.
            Users.Add(new MessServ.Models.User(name, name + "@love.you"));
            // Save all data.
            Serialization.Serialize(Users);
            return $"Created user with name { name }!";
        }
        /// <summary>
        /// Get method to know all users.
        /// </summary>
        /// <returns>List of all users with info about them.</returns>
        [HttpGet("informationofallusers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            Serialization.Deserialize(out Users);
            return Users;
        }
        /// <summary>
        /// Get method to get all information about the user by email.
        /// </summary>
        /// <param name="email">Email of user (Name + "@love.you").</param>
        /// <returns>Information about appropriate user.</returns>
        [HttpGet("user/{email}")]
        public async Task<ActionResult<User>> GetUserInfo([FromRoute] string email)
        {
            Serialization.Deserialize(out Users);
            var fUsers = Users.Where(x => x.Email == email).ToList();
            // Check if the user exists.
            if (fUsers.Count == 0)
            {
                return NotFound();
            }
            return fUsers.First();
        }
        /// <summary>
        /// Get method to get all messages that the user send.
        /// </summary>
        /// <param name="senderID">Email of user (Name + "@love.you").</param>
        /// <returns>Messages that the user send.</returns>
        [HttpGet("messagesSend/{senderID}")]
        public async Task<ActionResult<List<Message>>> GetMessagesBySender([FromRoute] string senderID)
        {
            Serialization.Deserialize(out Users);
            var fUsers = Users.Where(x => x.Email == senderID).ToList();
            // Check if the user exists.
            if (fUsers.Count == 0)
            {
                return NotFound();
            }
            return fUsers.First().Messages;
        }
        /// <summary>
        /// Get method to get all messages that the user recieve.
        /// </summary>
        /// <param name="receiverID">Email of user (Name + "@love.you").</param>
        /// <returns>Messages that the user recieve.</returns>
        [HttpGet("messagesReceive/{receiverID}")]
        public async Task<ActionResult<List<Message>>> GetMessagesByReceiver([FromRoute] string receiverID)
        {
            Serialization.Deserialize(out Users);
            var fUsers = Users.Where(x => x.Email == receiverID).ToList();
            // Check if the user exists.
            if (fUsers.Count == 0)
            {
                return NotFound();
            }
            List<Message> messages = new List<Message>();
            // By all users.
            foreach (var user in Users)
            {
                // By all messages.
                foreach (var mess in user.Messages)
                {
                    if (mess.ReceiverID == receiverID)
                        messages.Add(mess);
                }
            }
            // Check if messages we need exist.
            if (messages.Count == 0)
            {
                return NotFound();
            }
            return messages;
        }
        /// <summary>
        /// Get method to get all messages between to users (by one side).
        /// </summary>
        /// <param name="senderID">Email of sender user (Name + "@love.you").</param>
        /// <param name="receiverID">Email of receiver user (Name + "@love.you").</param>
        /// <returns>Messages between sender and receiver users.</returns>
        [HttpGet("messagesfromto/{senderID}/{receiverID}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessageBetween([FromRoute] string senderID, [FromRoute] string receiverID)
        {
            Serialization.Deserialize(out Users);
            // Check if sender and receiver users are exist.
            if (!(Users.Where(x => x.Email == senderID).ToList().Count > 0 && Users.Where(x => x.Email == receiverID).ToList().Count > 0))
            {
                return NotFound();
            }
            var sender = Users.Where(x => x.Email == senderID).ToList();
            List<Message> messages = new List<Message>();
            // Looking for need messages in sender's messages.
            foreach (var mess in sender.First().Messages)
            {
                if (mess.ReceiverID == receiverID)
                    messages.Add(mess);
            }
            // Check if we have needed messages.
            if (messages.Count == 0)
            {
                return NotFound();
            }
            return messages;
        }
    }
}