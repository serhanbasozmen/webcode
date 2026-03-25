namespace dotnet_store.Models;

public class OrderCreateModel
{
    public string FullName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public string PostCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? OrderNote { get; set; }
}