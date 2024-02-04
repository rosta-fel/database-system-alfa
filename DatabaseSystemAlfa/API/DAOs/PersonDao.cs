using DatabaseSystemAlfa.API.Entities;
using DatabaseSystemAlfa.Services;
using MySql.Data.MySqlClient;

namespace DatabaseSystemAlfa.API.DAOs;

public class PersonDao(DatabaseSingleton databaseInstance) : IDao<Person>
{
    public Person GetById(int id)
    {
        const string query = "SELECT * FROM Person WHERE Id = @Id";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) throw new Exception("No rows found in database.");
        reader.Read();

        return new Person
        {
            Id = reader.GetInt32(0),
            AdressID = reader.GetInt32(1),
            FirstName = reader.GetString(2),
            LastName = reader.GetString(3),
            Birthday = reader.GetDateTime(4),
            Sex = reader.GetString(5),
            BirthCertificate = reader.GetInt32(6)
        };
    }

    public void Insert(Person entity)
    {
        const string query = "INSERT INTO Person (AdressID, FirstName, LastName, Birthday, Sex, BirthCertificate) VALUES" +
                       "(@AdressID, @FirstName, @LastName, @Birthday, @Sex, @BirthCertificate);";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@AdressID", entity.AdressID);
        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
        command.Parameters.AddWithValue("@LastName", entity.LastName);
        command.Parameters.AddWithValue("@Birthday", entity.Birthday);
        command.Parameters.AddWithValue("@Sex", entity.Sex);
        command.Parameters.AddWithValue("@BirthCertificate", entity.BirthCertificate);

        command.ExecuteNonQuery();
    }
    public void Update(Person entity)
    {
        const string query = "UPDATE Person SET AdressID = @AdressID, " +
                             "FirstName = @FirstName, " +
                             "LastName = @LastName, " +
                             "Birthday = @Birthday, " +
                             "Sex = @Sex, " +
                             "BirthCertificate = @BirthCertificate" +
                             " WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@AdressID", entity.AdressID);
        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
        command.Parameters.AddWithValue("@LastName", entity.LastName);
        command.Parameters.AddWithValue("@Birthday", entity.Birthday);
        command.Parameters.AddWithValue("@Sex", entity.Sex);
        command.Parameters.AddWithValue("@BirthCertificate", entity.BirthCertificate);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        const string query = "DELETE FROM Person WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }}
