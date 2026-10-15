namespace HotelBookingSystem
{
    public class HotelService
    {
        private Room[] rooms;
        private Guest[] guests;
        private Booking[] bookings;

        private int guestCount = 0;
        private int bookingCount = 0;

        public HotelService()
        {
            rooms = new Room[6];
            guests = new Guest[20];
            bookings = new Booking[20];

            rooms[0] = new Room(101, "Standard");
            rooms[1] = new Room(102, "Standard");
            rooms[2] = new Room(103, "Standard");
            rooms[3] = new Room(201, "Premium");
            rooms[4] = new Room(202, "Premium");
            rooms[5] = new Room(203, "Premium");
        }

        // Display rooms
        public void DisplayRooms()
        {
            Console.WriteLine("\n--- Room List ---");

            foreach (Room room in rooms)
            {
                room.Display();
            }
        }

        // Search room
        public void SearchRoom()
        {
            Console.Write("Enter room type to search: ");
            string type = Console.ReadLine();

            bool found = false;

            foreach (Room room in rooms)
            {
                if (room.RoomType.ToLower() == type.ToLower())
                {
                    room.Display();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No room found.");
            }
        }

        // Sort rooms
        public void SortRooms()
        {
            for (int i = 0; i < rooms.Length - 1; i++)
            {
                for (int j = i + 1; j < rooms.Length; j++)
                {
                    if (rooms[i].NightlyRate > rooms[j].NightlyRate)
                    {
                        Room temp = rooms[i];
                        rooms[i] = rooms[j];
                        rooms[j] = temp;
                    }
                }
            }

            Console.WriteLine("\nRooms sorted by price:");
            DisplayRooms();
        }

        // Register guest
        public Guest RegisterGuest()
        {
            Console.WriteLine("\n--- Guest Registration ---");

            string name;

            while (true)
            {
                Console.Write("Enter name: ");
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name))
                    break;

                Console.WriteLine("Name cannot be empty.");
            }

            string phone;

            while (true)
            {
                Console.Write("Enter phone: ");
                phone = Console.ReadLine();

                if (phone.Length == 10)
                    break;

                Console.WriteLine("Phone must contain 10 digits.");
            }

            string email;

            while (true)
            {
                Console.Write("Enter email: ");
                email = Console.ReadLine();

                if (email.Contains("@"))
                    break;

                Console.WriteLine("Invalid email.");
            }

            guestCount++;

            Guest guest = new Guest(
                guestCount,
                name,
                phone,
                email);

            guests[guestCount - 1] = guest;

            Console.WriteLine($"Guest registered successfully. Guest ID: {guest.GuestId}");

            return guest;
        }

        // Find room using out
        public bool TryGetRoom(out Room room)
        {
            room = null;

            Console.Write("Enter room number: ");

            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return false;
            }

            foreach (Room currentRoom in rooms)
            {
                if (currentRoom.RoomNumber == roomNumber)
                {
                    room = currentRoom;
                    return true;
                }
            }

            Console.WriteLine("Room not found.");
            return false;
        }

        // Overloaded booking method 1
        public Booking CreateBooking(
            Guest guest,
            Room room,
            int nights,
            ref int counter)
        {
            if (!room.IsAvailable)
            {
                Console.WriteLine("Room is already booked.");
                return null;
            }

            if (nights <= 0)
            {
                Console.WriteLine("Number of nights must be positive.");
                return null;
            }

            counter++;

            Booking booking;

            if (room.RoomType == "Standard")
            {
                booking = new StandardRoomBooking(
                    counter,
                    guest,
                    room,
                    nights);
            }
            else
            {
                booking = new PremiumRoomBooking(
                    counter,
                    guest,
                    room,
                    nights);
            }

            room.IsAvailable = false;
            bookings[bookingCount] = booking;
            bookingCount++;

            Console.WriteLine(
                $"Booking confirmed. Booking ID: {booking.BookingId}");

            return booking;
        }

        // Overloaded booking method 2
        public Booking CreateBooking(
            Guest guest,
            int roomNumber,
            StayPeriod stay,
            ref int counter)
        {
            foreach (Room room in rooms)
            {
                if (room.RoomNumber == roomNumber)
                {
                    return CreateBooking(
                        guest,
                        room,
                        stay.Nights,
                        ref counter);
                }
            }

            Console.WriteLine("Room not found.");
            return null;
        }

        // Find booking
        public Booking FindBooking(int bookingId)
        {
            foreach (Booking booking in bookings)
            {
                if (booking != null &&
                    booking.BookingId == bookingId)
                {
                    return booking;
                }
            }

            return null;
        }

        // Cancellation
        public void CancelBooking()
        {
            Console.Write("Enter booking ID: ");

            if (!int.TryParse(Console.ReadLine(), out int bookingId))
            {
                Console.WriteLine("Invalid booking ID.");
                return;
            }

            Booking booking = FindBooking(bookingId);

            if (booking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            if (booking.IsCheckedOut)
            {
                Console.WriteLine("Booking already checked out.");
                return;
            }

            if (booking.IsCancelled)
            {
                Console.WriteLine("Booking already cancelled.");
                return;
            }

            booking.IsCancelled = true;
            booking.Room.IsAvailable = true;

            Console.WriteLine("Booking cancelled successfully.");
        }

        // Calculate summary
        public BookingSummary CalculateSummary(Booking booking)
        {
            decimal roomCost = booking.Room.NightlyRate * booking.Nights;

            decimal serviceCharge = booking.GetServiceCharge();

            decimal subtotal = roomCost + serviceCharge;

            decimal discount = 0m;

            if (booking.Nights >= 5)
            {
                discount = subtotal * 0.10m;
            }

            decimal afterDiscount = subtotal - discount;

            decimal tax = afterDiscount * 0.05m;

            decimal finalAmount = afterDiscount + tax;

            // Named tuple
            (decimal subtotal, decimal fees, decimal finalAmount) amount =
                (subtotal, tax, finalAmount);

            Console.WriteLine($"\nSubtotal: ₹{amount.subtotal}");
            Console.WriteLine($"Fees/Tax: ₹{amount.fees}");
            Console.WriteLine($"Final Amount: ₹{amount.finalAmount}");

            return new BookingSummary(
                roomCost,
                serviceCharge,
                discount,
                tax,
                finalAmount);
        }

        // Checkout overload 1
        public bool Checkout(
            Booking booking,
            IPaymentProcessor processor)
        {
            BookingSummary summary = CalculateSummary(booking);

            return Checkout(
                booking,
                processor,
                in summary);
        }

        // Checkout overload 2 using in
        public bool Checkout(
            Booking booking,
            IPaymentProcessor processor,
            in BookingSummary summary)
        {
            Console.WriteLine(
                $"\nFinal checkout amount: ₹{summary.FinalAmount}");

            bool paymentSuccessful =
                processor.ProcessPayment(summary.FinalAmount);

            if (!paymentSuccessful)
            {
                Console.WriteLine("Payment failed.");
                return false;
            }

            booking.IsCheckedOut = true;
            booking.Room.IsAvailable = true;

            Console.WriteLine("Checkout completed successfully.");

            return true;
        }

        // Display bookings
        public void DisplayBookings()
        {
            Console.WriteLine("\n--- Bookings ---");

            bool found = false;

            foreach (Booking booking in bookings)
            {
                if (booking != null)
                {
                    booking.Display();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No bookings available.");
            }
        }

        // Revenue by room type - bonus
        public void RevenueReport()
        {
            decimal standardRevenue = 0m;
            decimal premiumRevenue = 0m;

            foreach (Booking booking in bookings)
            {
                if (booking != null && booking.IsCheckedOut)
                {
                    decimal amount = booking.CalculateCharge();

                    if (booking.Room.RoomType == "Standard")
                    {
                        standardRevenue += amount;
                    }
                    else if (booking.Room.RoomType == "Premium")
                    {
                        premiumRevenue += amount;
                    }
                }
            }

            Console.WriteLine("\n--- Revenue Report ---");
            Console.WriteLine($"Standard Revenue: ₹{standardRevenue}");
            Console.WriteLine($"Premium Revenue: ₹{premiumRevenue}");
            Console.WriteLine(
                $"Total Revenue: ₹{standardRevenue + premiumRevenue}");
        }

        public int BookingCount
        {
            get { return bookingCount; }
        }
    }
}