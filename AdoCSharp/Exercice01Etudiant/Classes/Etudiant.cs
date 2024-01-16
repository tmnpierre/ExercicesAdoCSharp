using System.Data.SqlClient;

namespace ConsoleAppEtudiant
{
    public class Etudiant
    {
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tomyn\OneDrive\Documents\03 - M2i Exercices\03 - Exercices AdoCSharp\AdoCSharp\Exercice01Etudiant\Database1.mdf;Integrated Security=True;Connect Timeout=30";

        public static void AjouterEtudiant(string nom, string prenom, int numeroClasse, DateTime dateDiplome)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Etudiant (Nom, Prenom, NumeroClasse, DateDiplome) VALUES (@Nom, @Prenom, @NumeroClasse, @DateDiplome)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prenom", prenom);
                cmd.Parameters.AddWithValue("@NumeroClasse", numeroClasse);
                cmd.Parameters.AddWithValue("@DateDiplome", dateDiplome);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void AfficherEtudiants()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nom, Prenom, NumeroClasse, DateDiplome FROM Etudiant";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Nom: {reader["Nom"]}, Prénom: {reader["Prenom"]}, Numéro de Classe: {reader["NumeroClasse"]}, Date de Diplôme: {reader["DateDiplome"]}");
                    }
                }
                conn.Close();
            }
        }

        public static void SupprimerEtudiant(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Etudiant WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                conn.Close();

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
