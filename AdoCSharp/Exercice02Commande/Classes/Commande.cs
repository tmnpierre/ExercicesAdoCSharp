using System.Data;
using System.Data.SqlClient;

public class Commande
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime DateCommande { get; set; }
    public decimal Total { get; set; }

    public Commande(int id, int clientId, DateTime dateCommande, decimal total)
    {
        Id = id;
        ClientId = clientId;
        DateCommande = dateCommande;
        Total = total;
    }
}

public class CommandeService : ICommandeService
{
    private readonly ConnexionBaseDeDonnees _connexion;

    public CommandeService(ConnexionBaseDeDonnees connexion)
    {
        _connexion = connexion;
    }

    public void Ajouter(Commande commande)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientId", commande.ClientId),
                new SqlParameter("@DateCommande", commande.DateCommande),
                new SqlParameter("@Total", commande.Total)
            };

            _connexion.ExecuteNonQuery(
                "INSERT INTO Commandes (ClientId, DateCommande, Total) VALUES (@ClientId, @DateCommande, @Total)",
                CommandType.Text,
                parameters
            );
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de l'ajout de la commande : " + ex.Message);
            throw;
        }
    }

    public Commande Obtenir(int id)
    {
        try
        {
            var query = $"SELECT * FROM Commandes WHERE ID = @Id";
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
            return new Commande(0, Convert.ToInt32(row["ClientId"]), Convert.ToDateTime(row["DateCommande"]), Convert.ToDecimal(row["Total"]));

        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de l'obtention de la commande : " + ex.Message);
            throw;
        }
    }

    public void Modifier(Commande commande)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", commande.Id),
                new SqlParameter("@ClientId", commande.ClientId),
                new SqlParameter("@DateCommande", commande.DateCommande),
                new SqlParameter("@Total", commande.Total)
            };

            _connexion.ExecuteNonQuery(
                "UPDATE Commandes SET ClientId = @ClientId, DateCommande = @DateCommande, Total = @Total WHERE ID = @Id",
                CommandType.Text,
                parameters
            );
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de la modification de la commande : " + ex.Message);
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
                "DELETE FROM Commandes WHERE ID = @Id",
                CommandType.Text,
                parameters
            );
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de la suppression de la commande : " + ex.Message);
            throw;
        }
    }

    public DataTable AfficherToutesLesCommandes()
    {
        try
        {
            var query = "SELECT * FROM Commandes";
            return _connexion.ExecuteQuery(query, null);
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Erreur lors de l'affichage des commandes : " + ex.Message);
            throw;
        }
    }
}