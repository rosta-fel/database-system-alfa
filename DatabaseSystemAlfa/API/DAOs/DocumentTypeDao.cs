using DatabaseSystemAlfa.API.Entities;
using DatabaseSystemAlfa.Services;
using MySql.Data.MySqlClient;

namespace DatabaseSystemAlfa.API.DAOs;

public class DocumentTypeDao(DatabaseSingleton databaseInstance) : IDao<DocumentType>
{
    public DocumentType GetById(int id)
    {
        const string query = "SELECT * FROM DocumentType WHERE Id = @Id";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) throw new Exception("No rows found in database.");
        reader.Read();

        return new DocumentType
        {
            Id = reader.GetInt32(0),
            TypeName = reader.GetString(1),
            TypeDescription = reader.GetString(2)
        };
    }

    public void Insert(DocumentType entity)
    {
        const string query = "INSERT INTO DocumentType (TypeName, TypeDescription) VALUES" +
                             "(@TypeName, @TypeDescription);";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@TypeName", entity.TypeName);
        command.Parameters.AddWithValue("@TypeDescription", entity.TypeDescription);

        command.ExecuteNonQuery();
    }

    public void Update(DocumentType entity)
    {
        const string query = "UPDATE DocumentType SET TypeName = @TypeName, " +
                             "TypeDescription = @TypeDescription" +
                             " WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@TypeName", entity.TypeName);
        command.Parameters.AddWithValue("@TypeDescription", entity.TypeDescription);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        const string query = "DELETE FROM DocumentType WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}