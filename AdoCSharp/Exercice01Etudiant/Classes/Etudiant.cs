using System.Data.SqlClient;

namespace ConsoleAppEtudiant
{
    public class Etudiant
    {
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tomyn\OneDrive\Documents\03 - M2i Exercices\03 - Exercices AdoCSharp\AdoCSharp\Exercice01Etudiant\Database1.mdf;Integrated Security=True;Connect Timeout=30";

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int NumeroClasse { get; set; }
        public DateTime DateDiplome { get; set; }

        public Etudiant(string nom, string prenom, int numeroClasse, DateTime dateDiplome)
        {
            Nom = nom;
            Prenom = prenom;
            NumeroClasse = numeroClasse;
            DateDiplome = dateDiplome;
        }

        public static void AjouterEtudiant(string nom, string prenom, int numeroClasse, DateTime dateDiplome)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Etudiant (Nom, Prenom, NumeroClasse, DateDiplome) VALUES (@Nom, @Prenom, @NumeroClasse, @DateDiplome)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nom", nom);
                    cmd.Parameters.AddWithValue("@Prenom", prenom);
                    cmd.Parameters.AddWithValue("@NumeroClasse", numeroClasse);
                    cmd.Parameters.AddWithValue("@DateDiplome", dateDiplome);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void AfficherEtudiants()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nom, Prenom, NumeroClasse, DateDiplome FROM Etudiant";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["Id"]}, Nom: {reader["Nom"]}, Prénom: {reader["Prenom"]}, Numéro de Classe: {reader["NumeroClasse"]}, Date de Diplôme: {reader["DateDiplome"]}");
                        }
                    }
                }
            }
        }

        public static void SupprimerEtudiant(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Etudiant WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("Étudiant supprimé avec succès.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun étudiant trouvé avec cet ID.");
                    }
                }
            }
        }
    }
}
