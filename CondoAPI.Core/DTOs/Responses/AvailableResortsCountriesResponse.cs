namespace CondoAPI.Core.DTOs.Responses
{
    public class AvailableResortsCountriesResponse
    {
        public string Country { get; set; } = string.Empty;
        public int AvailableResorts { get; set; }
    }
}