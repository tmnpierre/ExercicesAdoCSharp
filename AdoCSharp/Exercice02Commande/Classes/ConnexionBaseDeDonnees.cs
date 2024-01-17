using System.Data;
using System.Data.SqlClient;

public class ConnexionBaseDeDonnees
{
    private string _connectionString;

    public ConnexionBaseDeDonnees(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                DataTable dataTable = new DataTable();
                using (var dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }
                return dataTable;
            }
        }
    }

    public int ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(commandText, connection))
            {
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
        }
    }
}
