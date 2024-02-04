namespace DatabaseSystemAlfa.API.Entities;

public class Person : IBaseClass
{
    public int Id { get; set; }
    public int AdressID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public string Sex { get; set; }
    public int BirthCertificate { get; set; }
}