namespace DatabaseSystemAlfa.API.Entities;

public class Passport : IBaseClass
{
    public int Id { get; set; }
    public int PassportNumber { get; set; }
    public int DocumentTypeID { get; set; }
    public int PersonID { get; set; }
    public int PublisherID { get; set; }
    public DateTime Issued { get; set; }
    public DateTime Expiry { get; set; }
}