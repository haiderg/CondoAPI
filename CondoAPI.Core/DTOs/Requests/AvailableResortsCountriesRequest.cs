namespace CondoAPI.Core.DTOs.Requests
{
    public class AvailableResortsCountriesRequest
    {
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public int MaxOccupancy { get; set; }

    }
}
