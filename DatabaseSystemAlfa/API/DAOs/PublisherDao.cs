using DatabaseSystemAlfa.API.Entities;
using DatabaseSystemAlfa.Services;
using MySql.Data.MySqlClient;

namespace DatabaseSystemAlfa.API.DAOs;

public class PublisherDao(DatabaseSingleton databaseInstance) : IDao<Publisher>
{
    public Publisher GetById(int id)
    {
        const string query = "SELECT * FROM Publisher WHERE Id = @Id";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        using MySqlDataReader reader = command.ExecuteReader();
        if (!reader.HasRows) throw new Exception("No rows found in database.");
        reader.Read();

        return new Publisher
        {
            Id = reader.GetInt32(0),
            PublisherName = reader.GetString(1),
            AdressID = reader.GetInt32(2)
        };
    }

    public void Insert(Publisher entity)
    {
        const string query = "INSERT INTO Publisher (PublisherName, AdressID) VALUES" +
                       "(@PublisherName, @AdressID);";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@PublisherName", entity.PublisherName);
        command.Parameters.AddWithValue("@AdressID", entity.AdressID);

        command.ExecuteNonQuery();
    }

    public void Update(Publisher entity)
    {
        const string query = "UPDATE Publisher SET PublisherName = @PublisherName, " +
                       "AdressID = @AdressID" +
                       " WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@PublisherName", entity.PublisherName);
        command.Parameters.AddWithValue("@AdressID", entity.AdressID);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        const string query = "DELETE FROM Publisher WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}