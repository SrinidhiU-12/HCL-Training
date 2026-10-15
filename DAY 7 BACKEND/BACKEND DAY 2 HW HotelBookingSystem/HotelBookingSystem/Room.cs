namespace HotelBookingSystem
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal NightlyRate { get; set; }
        public bool IsAvailable { get; set; }

        public Room(int roomNumber, string roomType)
        {
            this.RoomNumber = roomNumber;
            this.RoomType = roomType;
            this.NightlyRate = roomType switch
            {
                "Standard" => 2000m,
                "Premium" => 3500m,
                _ => 1500m
            };

            this.IsAvailable = true;
        }

        public void Display()
        {
            string status = IsAvailable ? "Available" : "Booked";

            Console.WriteLine(
                $"Room: {RoomNumber} | Type: {RoomType} | Rate: ₹{NightlyRate} | {status}");
        }
    }
}