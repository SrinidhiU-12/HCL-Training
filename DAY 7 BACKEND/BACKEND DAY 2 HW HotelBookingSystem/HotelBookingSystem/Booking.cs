namespace HotelBookingSystem
{
    public abstract class Booking
    {
        public int BookingId { get; set; }
        public Guest Guest { get; set; }
        public Room Room { get; set; }
        public int Nights { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsCheckedOut { get; set; }

        protected Booking(int bookingId, Guest guest, Room room, int nights)
        {
            this.BookingId = bookingId;
            this.Guest = guest;
            this.Room = room;
            this.Nights = nights;
        }

        public virtual decimal GetServiceCharge()
        {
            return 0m;
        }

        public abstract decimal CalculateCharge();

        public virtual void Display()
        {
            Console.WriteLine(
                $"Booking ID: {BookingId} | Guest: {Guest.Name} | " +
                $"Room: {Room.RoomNumber} | Nights: {Nights}");
        }
    }

    public class StandardRoomBooking : Booking
    {
        public StandardRoomBooking(
            int bookingId,
            Guest guest,
            Room room,
            int nights)
            : base(bookingId, guest, room, nights)
        {
        }

        public override decimal GetServiceCharge()
        {
            return 300m;
        }

        public override decimal CalculateCharge()
        {
            decimal roomCost = Room.NightlyRate * Nights;
            return roomCost + GetServiceCharge();
        }
    }

    public class PremiumRoomBooking : Booking
    {
        public PremiumRoomBooking(
            int bookingId,
            Guest guest,
            Room room,
            int nights)
            : base(bookingId, guest, room, nights)
        {
        }

        public override decimal GetServiceCharge()
        {
            return 700m;
        }

        public override decimal CalculateCharge()
        {
            decimal roomCost = Room.NightlyRate * Nights;
            return roomCost + GetServiceCharge();
        }
    }
}