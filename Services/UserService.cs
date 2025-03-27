using Bogus;
using FakeAPIApp.Models;

namespace FakeAPIApp.Services
{
    public class UserService
    {
        private Dictionary<long, User> _users = new Dictionary<long, User>();
        private Faker _faker = new();
        private int idCounter = 1;
        public UserService()
        {
            for (int i = 0; i < 5; i++)
            {
                GenerateUser();
            }
        }

        public List<User> GetUsers()
        {
            return _users.Values.ToList();
        }

        public User GetUser(long id)
        {
            return _users.GetValueOrDefault(id);
        }

        public User CreateUser(User user)
        {
            user.Id = idCounter++;
            if (user.IsValid())
            {
                _users.Add(user.Id, user);
                return user;
            }
            else
                return null;
        }

        public bool UserExists (long id)
        {
            return _users.ContainsKey(id);
        }

        public User Update(long id, User updatedUser)
        {
            if (updatedUser.IsParcialValid())
            {
                updatedUser.Id = id;
                _users[id] = updatedUser;
                return updatedUser;
            }
            else
                return null; 
        }

        public User Patch(long id, User partialUser)
        {
            if (!_users.TryGetValue(id, out var user)) return null;

            if (!string.IsNullOrEmpty(partialUser.Name)) user.Name = partialUser.Name;
            if (!string.IsNullOrEmpty(partialUser.Email)) user.Email = partialUser.Email;
            if (!string.IsNullOrEmpty(partialUser.Phone)) user.Phone = partialUser.Phone;
            if (!string.IsNullOrEmpty(partialUser.Address)) user.Address = partialUser.Address;

            return user;
        }

        public bool Delete(long id)
        {
            return _users.Remove(id);
        }

        private void GenerateUser()
        {
            User user = new User();
            user.Name = _faker.Name.FullName();
            user.Email = _faker.Internet.Email();
            user.Phone = _faker.Phone.PhoneNumber();
            user.Address = _faker.Address.FullAddress();
            CreateUser(user);
        }
    }
}
