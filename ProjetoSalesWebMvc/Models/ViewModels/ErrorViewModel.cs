namespace ProjetoSalesWebMvc.Models.ViewModels;

// Model auxiliar para povoar as telas
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public string Message { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
