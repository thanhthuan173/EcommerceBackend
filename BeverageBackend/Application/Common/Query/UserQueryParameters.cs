namespace BeverageBackend.Application.Common.Query
{
    public class UserQueryParameters : QueryParameters
    {
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }
    }
}