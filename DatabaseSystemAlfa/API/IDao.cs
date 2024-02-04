namespace DatabaseSystemAlfa.API;

public interface IDao<TBase> where TBase : IBaseClass
{
    TBase GetById(int id);
    
    void Insert(TBase entity);

    void Update(TBase entity);

    void Delete(int id);
}