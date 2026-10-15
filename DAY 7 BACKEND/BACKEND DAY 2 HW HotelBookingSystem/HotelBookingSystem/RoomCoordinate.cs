namespace HotelBookingSystem
{
    public struct RoomCoordinate
    {
        public int Floor { get; }
        public int RoomNumber { get; }

        public RoomCoordinate(int floor, int roomNumber)
        {
            Floor = floor;
            RoomNumber = roomNumber;
        }

        public override string ToString()
        {
            return $"Floor {Floor}, Room {RoomNumber}";
        }
    }
}