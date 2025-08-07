
namespace CondoAPI.Core.Models
{
    public class Agent : BaseEntity
    {
        public int AgentID { get; set; }

        public string AgentName { get; set; } = string.Empty;

        public string? AgentAddress { get; set; } 

        public string? AgentCity { get; set; }

        public string? AgentStoreProvince { get; set; }

        public string? AgentCountry { get; set; }

        public string? AgentPhone { get; set; }

        public string? AgentEmail { get; set; }

        public string? AgentContact { get; set; }

    }
}
