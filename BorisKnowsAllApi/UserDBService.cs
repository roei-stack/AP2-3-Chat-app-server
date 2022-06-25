using BorisKnowsAllApi.Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace BorisKnowsAllApi
{
    public class UserDBService
    {
        private readonly BorisKnowsAllApiContext _context;
        private static List<User> users = new List<User>();
        private static Dictionary<User, string> firebaseTokens = new Dictionary<User, string>();

        public UserDBService(BorisKnowsAllApiContext context)
        {
            _context = context;
            users.AddRange(_context.User.ToList());
        }

        public User Get(string username)
        {
            return users.Find(x => x.Username == username);
        }

        public void Create(string username, string password, string nickname)
        {
            if (Get(username) != null)
            {
                return;
            }

            User user = new User()
            {
                Username = username,
                Password = password,
                Nickname = nickname
            };
            users.Add(user);
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool IsEmpty()
        {
            return users.Count == 0;
        }

        public void AddFirebaseToken(User user, string token)
        {
            firebaseTokens[user] = token;
        }

        public string GetUserToken(User user)
        {
            return firebaseTokens[user];
        }

        public bool ContainsTokenForUser(User u)
        {
            return firebaseTokens.ContainsKey(u);
        }

        public void AddContact(User user, Contact contact)
        {
            user.AddContact(contact.id, contact.name, contact.server);
            _context.Add(contact);
            _context.SaveChanges();
        }

        public void EditContact(Contact contact)
        {
            try
            {
                _context.Update(contact);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { };
        }

        public void DeleteContact(User user, Contact contact)
        {
            _context.Contact.Remove(contact);
            _context.SaveChanges();
            user.DeleteContact(contact.id);
        }

        public void SendMessage(Contact contact, bool sent, string content)
        {
            contact.SendMessage(sent, content);
            _context.Add(contact.GetLastMessage());
            _context.SaveChanges();
        }

        public void EditMessage(OurMessage message)
        {
            try
            {
                _context.Update(message);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { };
        }

        public void DeleteMessage(Contact contact, OurMessage message)
        {
            _context.OurMessage.Remove(message);
            _context.SaveChanges();
            contact.RemoveMessage(message.id);
        }
    }
}

