namespace HotelBookingSystem
{
    public struct StayPeriod
    {
        public int Nights { get; }

        public StayPeriod(int nights)
        {
            Nights = nights;
        }
    }
}