namespace HotelBookingSystem
{
    class Program
    {
        static void Main()
        {
            HotelService hotel = new HotelService();

            Guest currentGuest = null;

            int bookingCounter = 0;

            while (true)
            {
                Console.WriteLine("\n==============================");
                Console.WriteLine(" HOTEL ROOM BOOKING SYSTEM");
                Console.WriteLine("==============================");

                Console.WriteLine("1. List Rooms");
                Console.WriteLine("2. Search Room Type");
                Console.WriteLine("3. Sort Rooms");
                Console.WriteLine("4. Register Guest");
                Console.WriteLine("5. Create Booking");
                Console.WriteLine("6. Cancel Booking");
                Console.WriteLine("7. Checkout");
                Console.WriteLine("8. Display Bookings");
                Console.WriteLine("9. Revenue Report");
                Console.WriteLine("10. Exit");

                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        hotel.DisplayRooms();
                        break;

                    case 2:
                        hotel.SearchRoom();
                        break;

                    case 3:
                        hotel.SortRooms();
                        break;

                    case 4:
                        currentGuest = hotel.RegisterGuest();
                        break;

                    case 5:
                        if (currentGuest == null)
                        {
                            Console.WriteLine(
                                "Please register a guest first.");
                            break;
                        }

                        if (!hotel.TryGetRoom(out Room selectedRoom))
                        {
                            break;
                        }

                        Console.Write("Enter number of nights: ");

                        if (!int.TryParse(
                            Console.ReadLine(),
                            out int nights))
                        {
                            Console.WriteLine("Invalid number of nights.");
                            break;
                        }

                        StayPeriod stay = new StayPeriod(nights);

                        Booking booking = hotel.CreateBooking(
                            currentGuest,
                            selectedRoom.RoomNumber,
                            stay,
                            ref bookingCounter);

                        if (booking != null)
                        {
                            Console.WriteLine(
                                $"Room charge: ₹{booking.CalculateCharge()}");
                        }

                        break;

                    case 6:
                        hotel.CancelBooking();
                        break;

                    case 7:
                        Console.Write("Enter booking ID: ");

                        if (!int.TryParse(
                            Console.ReadLine(),
                            out int bookingId))
                        {
                            Console.WriteLine("Invalid booking ID.");
                            break;
                        }

                        Booking selectedBooking =
                            hotel.FindBooking(bookingId);

                        if (selectedBooking == null)
                        {
                            Console.WriteLine("Booking not found.");
                            break;
                        }

                        if (selectedBooking.IsCancelled)
                        {
                            Console.WriteLine("Booking was cancelled.");
                            break;
                        }

                        if (selectedBooking.IsCheckedOut)
                        {
                            Console.WriteLine("Already checked out.");
                            break;
                        }

                        Console.WriteLine("\nSelect Payment Method:");
                        Console.WriteLine("1. Cash");
                        Console.WriteLine("2. Card/Digital");

                        Console.Write("Enter choice: ");

                        if (!int.TryParse(
                            Console.ReadLine(),
                            out int paymentChoice))
                        {
                            Console.WriteLine("Invalid payment choice.");
                            break;
                        }

                        IPaymentProcessor processor;

                        if (paymentChoice == 1)
                        {
                            processor = new CashPayment();
                        }
                        else if (paymentChoice == 2)
                        {
                            processor = new CardPayment();
                        }
                        else
                        {
                            Console.WriteLine("Invalid payment method.");
                            break;
                        }

                        // Pattern matching
                        if (processor is CardPayment)
                        {
                            Console.WriteLine(
                                "Card/Digital processor selected.");
                        }
                        else if (processor is CashPayment)
                        {
                            Console.WriteLine(
                                "Cash processor selected.");
                        }

                        hotel.Checkout(
                            selectedBooking,
                            processor);

                        break;

                    case 8:
                        hotel.DisplayBookings();
                        break;

                    case 9:
                        hotel.RevenueReport();
                        break;

                    case 10:
                        Console.WriteLine("Thank you.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}