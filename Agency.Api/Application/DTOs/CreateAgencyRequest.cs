namespace Agency.Api.Application.DTOs
{
    public record CreateAgencyRequest(string Name, string ContactEmail, List<CommissionRuleDto>? CommissionRules);
}
