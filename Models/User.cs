namespace FakeAPIApp.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public bool IsValid()
        {
            return Id != 0 && Name != null && Email != null && Phone != null;
        }

        public bool IsParcialValid()
        {
            return Name != null && Email != null && Phone != null;
        }
    }
}
