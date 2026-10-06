namespace MediCare.Domain.Responses;

public class ModuleListConfigurationResponse
{
    public int ModuleID { get; set; }

    public string ModuleCode { get; set; } = string.Empty;

    public string ModuleName { get; set; } = string.Empty;

    public string? Route { get; set; }

    public int ModuleListConfigurationID { get; set; }

    public string ListTitle { get; set; } = string.Empty;

    public string ApiEndpoint { get; set; } = string.Empty;

    public bool SearchEnabled { get; set; }

    public string? SearchPlaceholder { get; set; }

    public bool AddEnabled { get; set; }

    public bool EditEnabled { get; set; }

    public bool DeleteEnabled { get; set; }

    public int PageSize { get; set; }

    public string ColumnsJson { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}