namespace DatabaseSystemAlfa.API.Entities;

public class DocumentType : IBaseClass
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public string TypeDescription { get; set; }
}