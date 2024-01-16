using Microsoft.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\Exercice01; Integrated Security=True; Database=Exercice01";

Console.Write("Veuillez saisir votre prénom : ");
var prenom = Console.ReadLine();
Console.Write("Veuillez saisir votre nom de famille : ");
var nom = Console.ReadLine();
Console.Write("Veuillez saisir votre numéro de classe : ");
var classe = int.Parse(Console.ReadLine());
Console.Write("Veuillez saisir votre année d'obtention de votre diplôme : ");
var diplome = int.Parse(Console.ReadLine());

using (SqlConnection conn = new SqlConnection(connectionString))
{
    string req = "INSERT INTO Etudiant (Nom, Prenom, NumeroDeClasse, DateDeDiplome) VALUES (@Nom, @Prenom, @NumeroDeClasse, @DateDeDiplome)";

    SqlCommand command = new SqlCommand(req, conn);
    command.Parameters.AddWithValue("@Nom", nom);
    command.Parameters.AddWithValue("@Prenom", prenom);
    command.Parameters.AddWithValue("@NumeroDeClasse", classe);
    command.Parameters.AddWithValue("@DateDeDiplome", new DateTime(diplome, 1, 1));

    try
    {
        conn.Open();
        command.ExecuteNonQuery();
        Console.WriteLine("Enregistrement effectué avec succès.");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}