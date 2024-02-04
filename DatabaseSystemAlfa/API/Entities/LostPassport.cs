namespace DatabaseSystemAlfa.API.Entities;

public class LostPassport : IBaseClass
{
    public int Id { get; set; }
    public int PassportID { get; set; }
    public DateTime WhenLost { get; set; }
    public string LostDescription { get; set; }
}