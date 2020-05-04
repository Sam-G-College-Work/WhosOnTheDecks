namespace WhosOnTheDecks.API.Dtos
{
    //Booking for display Dto is to pass data from the databse
    //In a form we can display well 
    public class BookingDisplayDto
    {
        public int BookingId { get; set; }

        public string BookingStatus { get; set; }

        public int DjId { get; set; }

        public int EventId { get; set; }
    }
}