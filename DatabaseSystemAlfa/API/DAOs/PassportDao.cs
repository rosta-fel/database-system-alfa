using DatabaseSystemAlfa.API.Entities;
using DatabaseSystemAlfa.Services;
using MySql.Data.MySqlClient;

namespace DatabaseSystemAlfa.API.DAOs;

public class PassportDao(DatabaseSingleton databaseInstance) : IDao<Passport>
{
    public Passport GetById(int id)
    {
        const string query = "SELECT * FROM Passport WHERE Id = @Id";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) throw new Exception("No rows found in database.");
        reader.Read();

        return new Passport
        {
            Id = reader.GetInt32(0),
            PassportNumber = reader.GetInt32(1),
            DocumentTypeID = reader.GetInt32(2),
            PersonID = reader.GetInt32(3),
            PublisherID = reader.GetInt32(4),
            Issued = reader.GetDateTime(5),
            Expiry = reader.GetDateTime(6)
        };
    }

    public void Insert(Passport entity)
    {
        const string query =
            "INSERT INTO Passport (PassportNumber, DocumentTypeID, PersonID, PublisherID, Issued, Expiry) VALUES" +
            "(@PassportNumber, @DocumentTypeID, @PersonID, @PublisherID, @Issued, @Expiry);";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@PassportNumber", entity.PassportNumber);
        command.Parameters.AddWithValue("@DocumentTypeID", entity.DocumentTypeID);
        command.Parameters.AddWithValue("@PersonID", entity.PersonID);
        command.Parameters.AddWithValue("@PublisherID", entity.PublisherID);
        command.Parameters.AddWithValue("@Issued", entity.Issued);
        command.Parameters.AddWithValue("@Expiry", entity.Expiry);

        command.ExecuteNonQuery();
    }

    public void Update(Passport entity)
    {
        var query = "UPDATE Passport SET PassportNumber = @PassportNumber, " +
                    "DocumentTypeID = @DocumentTypeID, " +
                    "PersonID = @PersonID, " +
                    "PublisherID = @PublisherID, " +
                    "Issued = @Issued, " +
                    "Expiry = @Expiry" +
                    " WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@PassportNumber", entity.PassportNumber);
        command.Parameters.AddWithValue("@DocumentTypeID", entity.DocumentTypeID);
        command.Parameters.AddWithValue("@PersonID", entity.PersonID);
        command.Parameters.AddWithValue("@PublisherID", entity.PublisherID);
        command.Parameters.AddWithValue("@Issued", entity.Issued);
        command.Parameters.AddWithValue("@Expiry", entity.Expiry);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        const string query = "DELETE FROM Passport WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}