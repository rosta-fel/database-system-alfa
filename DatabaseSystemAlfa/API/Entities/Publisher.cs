namespace DatabaseSystemAlfa.API.Entities;


public class Publisher : IBaseClass
{
    public int Id { get; set; }
    public string PublisherName { get; set; }
    public int AdressID { get; set; }
}