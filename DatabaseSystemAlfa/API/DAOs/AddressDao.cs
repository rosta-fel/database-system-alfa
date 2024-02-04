using DatabaseSystemAlfa.API.Entities;
using DatabaseSystemAlfa.Services;
using MySql.Data.MySqlClient;

namespace DatabaseSystemAlfa.API.DAOs;

public class AddressDao(DatabaseSingleton databaseInstance) : IDao<Address>
{
    public Address GetById(int id)
    {
        const string query = "SELECT * FROM Address WHERE ID = @ID";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@ID", id);

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) throw new Exception("No rows found in database.");
        reader.Read();

        return new Address
        {
            Id = reader.GetInt32(0),
            Street = reader.GetString(1),
            City = reader.GetString(2),
            PostalCode = reader.GetInt32(3)
        };
    }

    public void Insert(Address entity)
    {
        const string query = "INSERT INTO Address (Street, City, PostalCode) VALUES" +
                             "(@Street, @City, @PostalCode);";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Street", entity.Street);
        command.Parameters.AddWithValue("@City", entity.City);
        command.Parameters.AddWithValue("@PostalCode", entity.PostalCode);

        command.ExecuteNonQuery();
    }

    public void Update(Address entity)
    {
        const string query = "UPDATE Address SET Street = @Street, " +
                             "City = @City, " +
                             "PostalCode = @PostalCode" +
                             " WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@Street", entity.Street);
        command.Parameters.AddWithValue("@City", entity.City);
        command.Parameters.AddWithValue("@PostalCode", entity.PostalCode);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        const string query = "DELETE FROM Address WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}