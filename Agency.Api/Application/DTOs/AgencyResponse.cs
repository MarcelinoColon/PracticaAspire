namespace Agency.Api.Application.DTOs
{
    public record AgencyResponse
    {
        public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ContactEmail { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public List<CommissionRuleDto> CommissionRules { get; set; } = new();
}
}
