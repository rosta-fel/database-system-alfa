using DatabaseSystemAlfa.API.Entities;
using DatabaseSystemAlfa.Services;
using MySql.Data.MySqlClient;

namespace DatabaseSystemAlfa.API.DAOs;

public class LostPassportDao(DatabaseSingleton databaseInstance) : IDao<LostPassport>
{
    public LostPassport GetById(int id)
    {
        const string query = "SELECT * FROM LostPassport WHERE Id = @Id";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) throw new Exception("No rows found in database.");
        reader.Read();

        return new LostPassport
        {
            Id = reader.GetInt32(0),
            PassportID = reader.GetInt32(1),
            WhenLost = reader.GetDateTime(2),
            LostDescription = reader.GetString(3)
        };
    }

    public void Insert(LostPassport entity)
    {
        const string query = "INSERT INTO LostPassport (PassportID, WhenLost, LostDescription) VALUES" +
                             "(@PassportID, @WhenLost, @LostDescription);";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@PassportID", entity.PassportID);
        command.Parameters.AddWithValue("@WhenLost", entity.WhenLost);
        command.Parameters.AddWithValue("@LostDescription", entity.LostDescription);

        command.ExecuteNonQuery();
    }

    public void Update(LostPassport entity)
    {
        const string query = "UPDATE LostPassport SET PassportID = @PassportID, " +
                             "WhenLost = @WhenLost, " +
                             "LostDescription = @LostDescription" +
                             " WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@PassportID", entity.PassportID);
        command.Parameters.AddWithValue("@WhenLost", entity.WhenLost);
        command.Parameters.AddWithValue("@LostDescription", entity.LostDescription);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        const string query = "DELETE FROM LostPassport WHERE Id = @Id;";

        using var command = new MySqlCommand(query, databaseInstance.Connection);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
    }
}