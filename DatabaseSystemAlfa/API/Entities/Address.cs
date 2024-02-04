namespace DatabaseSystemAlfa.API.Entities;

public class Address : IBaseClass
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
}