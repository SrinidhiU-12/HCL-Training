namespace HotelBookingSystem
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Guest(int guestId, string name, string phone, string email)
        {
            this.GuestId = guestId;
            this.Name = name;
            this.Phone = phone;
            this.Email = email;
        }

        public void Display()
        {
            Console.WriteLine(
                $"Guest ID: {GuestId} | Name: {Name} | Phone: {Phone} | Email: {Email}");
        }
    }
}