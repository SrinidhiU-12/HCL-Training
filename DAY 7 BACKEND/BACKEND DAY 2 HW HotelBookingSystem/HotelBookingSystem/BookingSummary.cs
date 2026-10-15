namespace HotelBookingSystem
{
    public record BookingSummary(
        decimal RoomCost,
        decimal ServiceCharge,
        decimal Discount,
        decimal Tax,
        decimal FinalAmount
    );
}