using System.Data.SqlClient;

namespace ConsoleAppEtudiant
{
    class Program
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tomyn\OneDrive\Documents\03 - M2i Exercices\03 - Exercices AdoCSharp\AdoCSharp\Exercice01Etudiant\Database1.mdf;Integrated Security=True;Connect Timeout=30";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Ajouter un étudiant");
                Console.WriteLine("2. Afficher tous les étudiants");
                Console.WriteLine("3. Supprimer un étudiant");
                Console.WriteLine("4. Quitter");
                Console.Write("Entrez votre choix : ");
                string choix = Console.ReadLine();

                try
                {
                    switch (choix)
                    {
                        case "1":
                            AjouterEtudiant();
                            break;
                        case "2":
                            AfficherEtudiants();
                            break;
                        case "3":
                            SupprimerEtudiant();
                            break;
                        case "4":
                            Console.WriteLine("Fin du programme.");
                            Console.ReadKey();
                            return;
                        default:
                            Console.WriteLine("Choix non valide.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Une erreur est survenue : {ex.Message}");
                }

                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }

        static void AjouterEtudiant()
        {
            Console.Write("Entrez le nom : ");
            string nom = Console.ReadLine();

            Console.Write("Entrez le prénom : ");
            string prenom = Console.ReadLine();

            Console.Write("Entrez le numéro de classe : ");
            int numeroClasse;
            while (!int.TryParse(Console.ReadLine(), out numeroClasse))
            {
                Console.Write("Entrez un numéro de classe valide : ");
            }

            Console.Write("Entrez la date de diplôme (YYYY-MM-DD) : ");
            DateTime dateDiplome;
            while (!DateTime.TryParse(Console.ReadLine(), out dateDiplome))
            {
                Console.Write("Entrez une date valide (YYYY-MM-DD) : ");
            }

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

            Console.WriteLine("Étudiant ajouté avec succès.");
        }

        static void AfficherEtudiants()
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

        static void SupprimerEtudiant()
        {
            Console.Write("Entrez l'ID de l'étudiant à supprimer : ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Entrez un ID valide : ");
            }

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