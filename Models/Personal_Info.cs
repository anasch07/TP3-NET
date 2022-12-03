using System.Data.SQLite;
using System.Diagnostics;


namespace WebApplication4.Models;

public class PersonalInfo
{
    private static string dbConfig = "Data Source=W:/Coding/INSAT/CodingGl3/.NET/TP3-NET/tp3Db.db;Integrated Security=True";

    public static List<Person> GetAllPerson()
    {
        SQLiteConnection conn = new SQLiteConnection(dbConfig);
        conn.Open();

        SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM personal_info", conn);
        SQLiteDataReader reader = cmd.ExecuteReader();

        List<Person> res = new List<Person>();
        while (reader.Read())
        {
            Person tmp = new Person((int)reader["id"]);
            tmp.first_name = (string)reader["first_name"];
            tmp.last_name = (string)reader["last_name"];
            tmp.email = (string)reader["email"];
            tmp.date_birth = reader.GetString(4);
            // tmp.dateBirth = reader.GetString(4);
            tmp.image = (string)reader["image"];
            tmp.country = (string)reader["country"];
            res.Add(tmp);
        }

        conn.Close();
        return res;
    }

    public static Person? GetPerson(int id)
    {
        using (SQLiteConnection conn = new SQLiteConnection(dbConfig))
        {
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM personal_info where id=@id", conn);
            cmd.Parameters.Add(new SQLiteParameter("@id", id));

            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Person res = new Person(id);
                res.first_name = reader.GetString(1);
                res.last_name = reader.GetString(2);
                res.email = reader.GetString(3);
                res.date_birth = reader.GetString(4);
                res.image = reader.GetString(5);
                res.country = reader.GetString(6);
                return res;
            }
        }

        return null;

    }

    public static Person GetPersonByFirstNameAndCountry(string firstName, string country)

    {
        using (SQLiteConnection conn = new SQLiteConnection(dbConfig))
        {

            conn.Open();
            SQLiteCommand cmd =
                new SQLiteCommand("SELECT * FROM personal_info where first_name=@first_name and country=@country",
                    conn);
            cmd.Parameters.Add(new SQLiteParameter("@first_name", firstName));
            cmd.Parameters.Add(new SQLiteParameter("@country", country));

            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                Person res = new Person((int)reader["id"]);
                res.first_name = reader.GetString(1);
                res.last_name = reader.GetString(2);
                res.email = reader.GetString(3);
                res.date_birth = reader.GetString(4);
                res.image = reader.GetString(5);
                res.country = reader.GetString(6);
                return res;
            }

        }

        return null;
    }


}