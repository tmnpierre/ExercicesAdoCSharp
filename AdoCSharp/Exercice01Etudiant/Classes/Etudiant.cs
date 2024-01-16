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

        public bool Save()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query;
                if (Id == 0)
                {
                    query = "INSERT INTO Etudiant (Nom, Prenom, NumeroClasse, DateDiplome) VALUES (@Nom, @Prenom, @NumeroClasse, @DateDiplome)";
                }
                else
                {
                    query = "UPDATE Etudiant SET Nom = @Nom, Prenom = @Prenom, NumeroClasse = @NumeroClasse, DateDiplome = @DateDiplome WHERE Id = @Id";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@Nom", Nom);
                    cmd.Parameters.AddWithValue("@Prenom", Prenom);
                    cmd.Parameters.AddWithValue("@NumeroClasse", NumeroClasse);
                    cmd.Parameters.AddWithValue("@DateDiplome", DateDiplome);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public bool Delete()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Etudiant WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public static Etudiant GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nom, Prenom, NumeroClasse, DateDiplome FROM Etudiant WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Etudiant(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDateTime(4))
                            {
                                Id = reader.GetInt32(0)
                            };
                        }
                    }
                }
            }
            return null;
        }

        public static List<Etudiant> GetEtudiants(int? numeroClasse = null)
        {
            List<Etudiant> etudiants = new List<Etudiant>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Nom, Prenom, NumeroClasse, DateDiplome FROM Etudiant";
                if (numeroClasse.HasValue)
                {
                    query += " WHERE NumeroClasse = @NumeroClasse";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (numeroClasse.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@NumeroClasse", numeroClasse.Value);
                    }

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            etudiants.Add(new Etudiant(reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDateTime(4))
                            {
                                Id = reader.GetInt32(0)
                            });
                        }
                    }
                }
            }
            return etudiants;
        }

        public static bool EditEtudiant(int id, Etudiant etudiantModifie)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Etudiant SET Nom = @Nom, Prenom = @Prenom, NumeroClasse = @NumeroClasse, DateDiplome = @DateDiplome WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Nom", etudiantModifie.Nom);
                    cmd.Parameters.AddWithValue("@Prenom", etudiantModifie.Prenom);
                    cmd.Parameters.AddWithValue("@NumeroClasse", etudiantModifie.NumeroClasse);
                    cmd.Parameters.AddWithValue("@DateDiplome", etudiantModifie.DateDiplome);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
