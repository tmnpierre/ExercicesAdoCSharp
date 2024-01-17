using System.Data;
using System.Data.SqlClient;

public class Client
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Adresse { get; set; }
    public string CodePostal { get; set; }
    public string Ville { get; set; }
    public string Telephone { get; set; }

    public Client(int id, string nom, string prenom, string adresse, string codePostal, string ville, string telephone)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Adresse = adresse;
        CodePostal = codePostal;
        Ville = ville;
        Telephone = telephone;
    }
}

public class ClientService : IClientService
{
    private ConnexionBaseDeDonnees _connexion;

    public ClientService(ConnexionBaseDeDonnees connexion)
    {
        _connexion = connexion;
    }

    public void Ajouter(Client client)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Nom", client.Nom),
                new SqlParameter("@Prenom", client.Prenom),
                new SqlParameter("@Adresse", client.Adresse),
                new SqlParameter("@CodePostal", client.CodePostal),
                new SqlParameter("@Ville", client.Ville),
                new SqlParameter("@Telephone", client.Telephone),
            };

            _connexion.ExecuteNonQuery(
                "INSERT INTO Clients (Nom, Prenom, Adresse, CodePostal, Ville, Telephone) VALUES (@Nom, @Prenom, @Adresse, @CodePostal, @Ville, @Telephone)",
                CommandType.Text,
                parameters
            );
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de l'ajout du client : " + ex.Message);
            throw;
        }
    }

    public Client Obtenir(int id)
    {
        try
        {
            var query = $"SELECT * FROM Clients WHERE ID = @Id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            DataTable dataTable = _connexion.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataTable.Rows[0];
            return MapClient(row);
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de l'obtention du client : " + ex.Message);
            throw;
        }
    }

    public void Modifier(Client client)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", client.Id),
                new SqlParameter("@Nom", client.Nom),
                new SqlParameter("@Prenom", client.Prenom),
                new SqlParameter("@Adresse", client.Adresse),
                new SqlParameter("@CodePostal", client.CodePostal),
                new SqlParameter("@Ville", client.Ville),
                new SqlParameter("@Telephone", client.Telephone),
            };

            _connexion.ExecuteNonQuery(
                "UPDATE Clients SET Nom = @Nom, Prenom = @Prenom, Adresse = @Adresse, CodePostal = @CodePostal, Ville = @Ville, Telephone = @Telephone WHERE ID = @Id",
                CommandType.Text,
                parameters
            );
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de la modification du client : " + ex.Message);
            throw;
        }
    }

    public void Supprimer(int id)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            _connexion.ExecuteNonQuery(
                "DELETE FROM Clients WHERE ID = @Id",
                CommandType.Text,
                parameters
            );
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de la suppression du client : " + ex.Message);
            throw;
        }
    }

    private Client MapClient(DataRow row)
    {
        return new Client(0, row["Nom"].ToString(), row["Prenom"].ToString(), row["Adresse"].ToString(),
                         row["CodePostal"].ToString(), row["Ville"].ToString(), row["Telephone"].ToString());
    }

    public DataTable AfficherTousLesClients()
    {
        try
        {
            var query = "SELECT * FROM Clients";
            return _connexion.ExecuteQuery(query, null);
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de l'affichage des clients : " + ex.Message);
            throw;
        }
    }
}
