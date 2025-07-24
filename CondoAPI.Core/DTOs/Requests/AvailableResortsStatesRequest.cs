namespace CondoAPI.Core.DTOs.Requests
{
    public class AvailableResortsLocationsRequest
    {
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public int MaxOccupancy { get; set; }
        public string? Country { get; set; } 
        public string? StateProvince { get; set; }
        public string? City { get; set; }
    }
}
