namespace GraphQl.Bff.Models
{
    public record AgencyResponse(
        int Id,
        string Name,
        string ContactEmail,
        bool IsActive,
        List<CommissionRuleResponse>? CommissionRules
    );
}